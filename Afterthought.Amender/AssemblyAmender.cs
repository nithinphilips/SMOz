//-----------------------------------------------------------------------------
//
// Copyright (c) VC3, Inc. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Cci.MutableCodeModel;
using Microsoft.Cci;
using System.Runtime.Serialization;
using System.IO;
using Afterthought;

namespace Afterthought.Amender
{
	/// <summary>
	/// Amends an assembly using the specified type amendments.
	/// </summary>
	internal class AssemblyAmender : MutatingVisitor
	{
		static System.Reflection.MethodInfo VoidMethod = new Action(() => { }).Method;
		static System.Reflection.MethodInfo NotImplementedMethod = new Action(() => { throw new NotImplementedException(); }).Method;
		static System.Reflection.MethodInfo DefaultGetterMethod = new Func<object>(() => { return new object(); }).Method;
		static System.Reflection.MethodInfo DefaultSetterMethod = new Action<object>((value) => { }).Method;

		Dictionary<string, ITypeAmendment> typeAmendments;
		ITypeDefinition iTypeAmendment;
		ITypeDefinition iAmendmentAttribute;

		internal AssemblyAmender(IMetadataHost host, PdbReader pdbReader, IEnumerable<ITypeAmendment> typeAmendments)
			: base(host, true)
		{
			this.typeAmendments = typeAmendments.ToDictionary(w => w.Type.FullName);
			iTypeAmendment = ResolveType(typeof(ITypeAmendment));
			iAmendmentAttribute = ResolveType(typeof(IAmendmentAttribute));
		}

		internal string TargetRuntimeVersion { get; set; }

		Dictionary<IFieldDefinition, IFieldAmendment> Fields { get; set; }

		Dictionary<IMethodDefinition, IConstructorAmendment> Constructors { get; set; }

		Dictionary<IPropertyDefinition, IPropertyAmendment> Properties { get; set; }

		Dictionary<IMethodDefinition, IMethodAmendment> Methods { get; set; }

		Dictionary<IPropertyDefinition, GenericMethod> PropertyInitializers { get; set; }

		/// <summary>
		/// Performs mutations on namespace-level types.
		/// </summary>
		/// <param name="namespaceTypeDefinition"></param>
		/// <returns></returns>
		public override NamespaceTypeDefinition Mutate(NamespaceTypeDefinition namespaceTypeDefinition)
		{
			return AmendType(namespaceTypeDefinition) ? base.Mutate(namespaceTypeDefinition) : namespaceTypeDefinition;
		}

		/// <summary>
		/// Performs mutations on nested types.
		/// </summary>
		/// <param name="nestedTypeDefinition"></param>
		/// <returns></returns>
		public override NestedTypeDefinition Mutate(NestedTypeDefinition nestedTypeDefinition)
		{
			return AmendType(nestedTypeDefinition) ? base.Mutate(nestedTypeDefinition) : nestedTypeDefinition;
		}

		/// <summary>
		/// Applies amendments to namespace and nested types.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>True if the type is amended, otherwise false</returns>
		bool AmendType(TypeDefinition type)
		{
			// Remove all attributes implementing IAmendmentAttribute
			type.Attributes.RemoveAll(attr => TypeHelper.Type1ImplementsType2(attr.Type.ResolvedType, iAmendmentAttribute));

			// Get the Amendment for the current type definition
			ITypeAmendment typeAmendment;
			typeAmendments.TryGetValue(type.ToString(), out typeAmendment);

			// Exit immediately if the type is not being amended
			if (typeAmendment == null)
			{
				// Indicate that the type implement ITypeAmendment and should be mutated
				if (TypeHelper.Type1ImplementsType2(type, iTypeAmendment))
					return true;

				// Indicate that this type is not being amended and should not be mutated
				else
					return false;
			}

			// Add new fields
			Fields = new Dictionary<IFieldDefinition, IFieldAmendment>();
			foreach (var field in typeAmendment.Fields.Where(f => f.FieldInfo == null))
			{
				type.Fields.Add(new FieldDefinition()
				{
					Name = host.NameTable.GetNameFor(field.Name),
					Type = ResolveType(field.Type),
					InternFactory = host.InternFactory,
				});
			}

			// Constructors
			Constructors = new Dictionary<IMethodDefinition, IConstructorAmendment>();
			foreach (var constructor in typeAmendment.Constructors)
			{
				// Add a new method
				if (constructor.ConstructorInfo == null)
					AddConstructor(type, constructor);

				// Track existing properties being amended
				else
					Constructors.Add(ResolveConstructor(type, constructor.ConstructorInfo), constructor);
			}

			// Properties
			Properties = new Dictionary<IPropertyDefinition, IPropertyAmendment>();
			PropertyInitializers = new Dictionary<IPropertyDefinition, GenericMethod>();
			foreach (var property in typeAmendment.Properties)
			{
				IPropertyDefinition propertyDef;

				// Add a new property
				if (property.PropertyInfo == null)
					propertyDef = AddProperty(type, property);

				// Track existing properties being amended
				else
				{
					propertyDef = ResolveProperty(type, property.PropertyInfo);
					Properties.Add(propertyDef, property);
				}

				// Track property initializers
				if (property.Initializer != null)
					PropertyInitializers.Add(propertyDef, ResolvePropertyDelegate(propertyDef, property.Initializer));
			}

			// Methods
			Methods = new Dictionary<IMethodDefinition, IMethodAmendment>();
			foreach (var method in typeAmendment.Methods)
			{
				// Add a new method
				if (method.MethodInfo == null)
					AddMethod(type, method);

				// Track existing properties being amended
				else
					Methods.Add(ResolveMethod(type, method.MethodInfo), method);
			}

			// Implement interfaces
			foreach (var interfaceType in typeAmendment.Interfaces)
			{
				// Get the corresponding interface definition
				ITypeDefinition interfaceDef = ResolveType(interfaceType);

				// Process all interface properties
				foreach (var property in interfaceType.GetProperties())
				{
					// Get the corresponding property definition
					var propertyDef = ResolveProperty(interfaceDef, property);

					// Determine if the interface property is already implemented by the type
					var existingProperty = type.Properties.Where(p => AreEquivalent(p, property) && p.Visibility == TypeMemberVisibility.Public).FirstOrDefault();

					// Mark the existing property as implementing the interface
					if (existingProperty != null)
					{
						// Notify the type that the interface is being implemented by this property
						if (existingProperty.Getter != null)
							Implement(type, propertyDef.Getter, existingProperty.Getter);

						if (existingProperty.Setter != null)
							Implement(type, propertyDef.Setter, existingProperty.Setter);
					}

					// Determine if the property needs to be implicitly implemented as an auto property
					else if (!Properties.Values.Any(p => p.Implements == property))
						AddProperty(type, Afterthought.Amendment.Property.Implement(typeAmendment.Type, property));
				}

				// Mark the type as implementing the interface
				type.Interfaces.Add(interfaceDef);
			}

			// Indicate that this type is being mutated
			return true;
		}

		/// <summary>
		/// Indicates that a method implements an interface on a type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="implemented"></param>
		/// <param name="implementing"></param>
		void Implement(TypeDefinition type, IMethodReference implemented, IMethodReference implementing)
		{
			type.ExplicitImplementationOverrides.Add(new MethodImplementation()
			{
				ContainingType = type,
				ImplementedMethod = implemented,
				ImplementingMethod = implementing
			});
		}

		/// <summary>
		/// Adds a new constructor to a type definition.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="constructor"></param>
		/// <returns></returns>
		MethodDefinition AddConstructor(TypeDefinition type, IConstructorAmendment constructor)
		{
			return null;
		}

		/// <summary>
		/// Adds a new property to a type definition.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		PropertyDefinition AddProperty(TypeDefinition type, IPropertyAmendment property)
		{
			var propertyType = ResolveType(property.Type);
			bool isInterface = property.Implements != null;

			// Add the property
			var propertyDef = new PropertyDefinition()
			{
				Name = host.NameTable.GetNameFor(property.Name),
				Type = propertyType,
				ContainingTypeDefinition = type,
				CallingConvention = CallingConvention.HasThis,
				Visibility = isInterface ? TypeMemberVisibility.Private : TypeMemberVisibility.Public,
				Accessors = new List<IMethodReference>()
			};
			type.Properties.Add(propertyDef);

			// Optionally create a getter
			if (property.Getter != null || (isInterface && property.Implements.GetGetMethod() != null) || (!isInterface && property.Setter == null))
			{
				// Create the getter method
				var getter = new MethodDefinition
				{
					ContainingTypeDefinition = type,
					Type = propertyType,
					Name = host.NameTable.GetNameFor((isInterface ? property.Implements.DeclaringType.FullName + "." : "") +
							"get_" + (isInterface ? property.Implements.Name : property.Name)),
					IsSpecialName = true,
					IsHiddenBySignature = true,
					IsCil = true,
					IsNewSlot = isInterface,
					IsVirtual = isInterface,
					IsSealed = isInterface,
					Visibility = isInterface ? TypeMemberVisibility.Private : TypeMemberVisibility.Public,
					CallingConvention = CallingConvention.HasThis,
					InternFactory = host.InternFactory,
					Body = new MethodBody()
				};

				// Association the getter with the property definition
				propertyDef.Getter = getter;
				propertyDef.Accessors.Add(getter);
				((MethodBody)getter.Body).MethodDefinition = getter;
				type.Methods.Add(getter);
				if (isInterface)
					Implement(type, ResolveProperty(ResolveType(property.Implements.DeclaringType), property.Implements).Getter, getter);

			}

			// Optionally create a setter
			if (property.Setter != null || (isInterface && property.Implements.GetSetMethod() != null) || (!isInterface && property.Getter == null))
			{
				// Create the setter method
				var setter = new MethodDefinition
					{
						ContainingTypeDefinition = type,
						Type = host.PlatformType.SystemVoid,
						Name = host.NameTable.GetNameFor((isInterface ? property.Implements.DeclaringType.FullName + "." : "") +
								"set_" + (isInterface ? property.Implements.Name : property.Name)),
						IsSpecialName = true,
						IsHiddenBySignature = true,
						IsCil = true,
						IsNewSlot = isInterface,
						IsVirtual = isInterface,
						IsSealed = isInterface,
						Visibility = isInterface ? TypeMemberVisibility.Private : TypeMemberVisibility.Public,
						CallingConvention = CallingConvention.HasThis,
						InternFactory = host.InternFactory,
						Body = new MethodBody(),
						Parameters = new List<IParameterDefinition>() { new ParameterDefinition() 
						{
							Index = 0,
							Name = host.NameTable.value,
							Type = propertyType
						}}
					};

				// Association the setter with the property definition
				propertyDef.Setter = setter;
				propertyDef.Accessors.Add(setter);
				((MethodBody)setter.Body).MethodDefinition = setter;
				type.Methods.Add(setter);
				if (isInterface)
					Implement(type, ResolveProperty(ResolveType(property.Implements.DeclaringType), property.Implements).Setter, setter);
			}

			// Add a backing field for new properties that do not have a getter or setter defined
			if (property.PropertyInfo == null && property.Getter == null && property.Setter == null)
			{
				// Add the backing field
				var backingField = new FieldDefinition()
				{
					Name = host.NameTable.GetNameFor(GetBackingFieldName(property)),
					Type = propertyType,
					InternFactory = host.InternFactory,
				};
				type.Fields.Add(backingField);
			}

			// Track the newly added property
			Properties.Add(propertyDef, property);

			// Return the new property definition
			return propertyDef;
		}

		/// <summary>
		/// Adds a new method to a type definition.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		MethodDefinition AddMethod(TypeDefinition type, IMethodAmendment method)
		{
			bool isInterface = method.Implements != null;

			// Add the method
			var args = method.Implementation.GetParameters().Skip(2).Select(p => p.ParameterType.GetGenericArguments()).FirstOrDefault() ?? new Type[] { };
			var methodDef = new MethodDefinition
			{
				ContainingTypeDefinition = type,
				Type = ResolveType(method.Implementation.ReturnType),
				Name = host.NameTable.GetNameFor(method.Name),
				IsSpecialName = true,
				IsHiddenBySignature = true,
				IsCil = true,
				IsNewSlot = isInterface,
				IsVirtual = isInterface,
				IsSealed = isInterface,
				Visibility = isInterface ? TypeMemberVisibility.Private : TypeMemberVisibility.Public,
				CallingConvention = CallingConvention.HasThis,
				InternFactory = host.InternFactory,
				Body = new MethodBody(),
				Parameters = args.Select((parameterType, index) =>
						new ParameterDefinition()
						{
							Index = (ushort)index,
							Name = host.NameTable.GetNameFor("arg" + index),
							Type = ResolveType(parameterType)
						}
					)
					.ToList<IParameterDefinition>()
			};
			
			((MethodBody)methodDef.Body).MethodDefinition = methodDef;
			type.Methods.Add(methodDef);
			if (isInterface)
				Implement(type, ResolveMethod(ResolveType(method.Implements.DeclaringType), method.Implements), methodDef);

			// Track the newly added method
			Methods.Add(methodDef, method);

			// Return the new method definition
			return methodDef;
		}

		/// <summary>
		/// Changes visibility on static methods of Amendments to make them callable.
		/// </summary>
		/// <param name="methodDef"></param>
		/// <returns></returns>
		public override MethodDefinition Mutate(MethodDefinition methodDef)
		{
			// Automatically make all private static methods have internal scope
			if (TypeHelper.Type1ImplementsType2(GetCurrentType(), iTypeAmendment))
			{
				if (methodDef.IsStatic && methodDef.Visibility == TypeMemberVisibility.Private)
					methodDef.Visibility = TypeMemberVisibility.Assembly;
				return methodDef;
			}

			return base.Mutate(methodDef);
		}

		/// <summary>
		/// Initialize __graphInstance backing field with a new <see cref="GraphInstance"/>.
		/// </summary>
		/// <param name="methodBody"></param>
		/// <returns></returns>
		public override MethodBody Mutate(MethodBody methodBody)
		{
			// Get the method definition
			IMethodDefinition methodDef = methodBody.MethodDefinition;

			// Static methods are not supported
			if (methodDef.IsStatic)
				return methodBody;

			// Get the name of the interface and method
			int index = methodDef.Name.Value.LastIndexOf('.');
			string interfaceName = index < 0 ? "" : methodDef.Name.Value.Substring(0, index + 1);
			string methodName = index < 0 ? methodDef.Name.Value : methodDef.Name.Value.Substring(index + 1);

			// Constructors
			if (methodDef.IsConstructor)
			{
				// Get the constructor amendment
				IConstructorAmendment constructorAmendment;

				// Exit immediately if the constructor is not being amended
				if (!Constructors.TryGetValue(methodDef, out constructorAmendment) && PropertyInitializers.Count == 0)
					return methodBody;

				// Amends the constructor
				AmendConstructor(constructorAmendment, methodBody);
			}

			// Properties
			else if (methodDef.IsHiddenBySignature && methodDef.IsSpecialName && (methodName.StartsWith("get_") || methodName.StartsWith("set_")))
			{
				// Determine which property is being mutated
				IPropertyDefinition propertyDef = GetCurrentType().Properties
					.Where(p => p.Name.Value == interfaceName + methodName.Substring(4))
					.FirstOrDefault();

				// Ignore methods that are generated to implement properties defined on base types
				if (propertyDef == null && interfaceName != "")
					return methodBody;
	
				// Get the property Amendment
				IPropertyAmendment propertyAmendment;

				// Exit immediately if the property is not being amended
				if (!Properties.TryGetValue(propertyDef, out propertyAmendment))
					return methodBody;

				// Amend the property
				AmendProperty(propertyDef, propertyAmendment, methodBody);
			}

			// Methods
			else
			{
				// Get the method Amendment
				IMethodAmendment methodAmendment;

				// Exit immediately if the method is not being amended
				if (!Methods.TryGetValue(methodDef, out methodAmendment))
					return methodBody;

				// Amends the method
				AmendMethod(methodAmendment, methodBody);
			}

			return methodBody;
		}

		/// <summary>
		/// Amends the constructor body with the specified constructor amendment.
		/// </summary>
		/// <param name="constructorAmendment"></param>
		/// <param name="methodBody"></param>
		void AmendConstructor(IConstructorAmendment constructorAmendment, MethodBody methodBody)
		{
			// Create an IL generator to amend the operations
			var il = new ILAmender(host, methodBody);

			// Emit the original method operations
			il.EmitUntilReturn();

			// Emit property initializers if the current constructor calls a base constructor or inherits from System.Object
			if (GetCurrentType().BaseClasses.Any(b => TypeHelper.TypesAreEquivalent(b, host.PlatformType.SystemObject)) ||
				methodBody.Operations.Any(o => o.OperationCode == OperationCode.Call && ((IMethodReference)o.Value).ResolvedMethod.IsConstructor && ((IMethodReference)o.Value).ResolvedMethod.ContainingType == GetCurrentType()))
			{
				foreach (var initializer in PropertyInitializers)
				{
					// Load this pointer onto stack
					il.Emit(OperationCode.Ldarg_0);

					// Load this pointer onto stack again
					il.Emit(OperationCode.Ldarg_0);

					// Load property name onto stack
					il.Emit(OperationCode.Ldstr, initializer.Key.Name.Value);

					// Call LazyInitializer delegate
					il.Emit(OperationCode.Call, initializer.Value.Instance);

					// Set the property
					il.Emit(initializer.Key.Setter.ResolvedMethod.IsVirtual ? OperationCode.Callvirt : OperationCode.Call, initializer.Key.Setter);
				}
			}

			// Update the method body
			il.UpdateMethodBody(6);
		}

		/// <summary>
		/// Amends a property definition using the specified property amendment.
		/// </summary>
		/// <param name="propertyDef"></param>
		/// <param name="propertyAmendment"></param>
		/// <param name="methodBody"></param>
		void AmendProperty(IPropertyDefinition propertyDef, IPropertyAmendment propertyAmendment, MethodBody methodBody)
		{	
			// Create an IL generator to amend the operations
			var il = new ILAmender(host, methodBody);

			// Property Get
			if (methodBody.MethodDefinition.Name.Value.StartsWith("get_") || methodBody.MethodDefinition.Name.Value.Contains(".get_"))
			{
				// Getter for new or existing property
				if (propertyAmendment.Getter != null)
				{
					// Clear the original method body
					il.Operations.Clear();

					// Get the delegate to call 
					var getter = ResolvePropertyDelegate(propertyDef, propertyAmendment.Getter);

					// Load this pointer onto stack
					il.Emit(OperationCode.Ldarg_0);

					// Load property name onto stack
					il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

					// Call Getter delegate
					il.Emit(OperationCode.Call, getter.Instance);

					// Return
					il.Emit(OperationCode.Ret);
				}

				// New property without defined getter
				else if (propertyAmendment.PropertyInfo == null)
				{
					// Get the previously created backing field for this property
					var backingField = GetCurrentType().Fields.Where(f => f.Name.Value == GetBackingFieldName(propertyAmendment)).First();

					// Default getter
					if (propertyAmendment.LazyInitializer == null)
					{
						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load the backing field
						il.Emit(OperationCode.Ldfld, backingField);

						// Return
						il.Emit(OperationCode.Ret);
					}

					// Lazy initialized getter
					else
					{
						// Get the delegate to call before the property getter
						var lazyInitializer = ResolvePropertyDelegate(propertyDef, propertyAmendment.LazyInitializer);

						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load the backing field
						il.Emit(OperationCode.Ldfld, backingField);

						// Skip initialization if the backing field is already initialized
						var skipIfNotNull = new ILGeneratorLabel();
						il.Emit(OperationCode.Brtrue_S, skipIfNotNull);

						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load this pointer onto stack again
						il.Emit(OperationCode.Ldarg_0);

						// Load property name onto stack
						il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

						// Call LazyInitializer delegate
						il.Emit(OperationCode.Call, lazyInitializer.Instance);

						// Update the backing field
						il.Emit(OperationCode.Stfld, backingField);
						
						// End the skipping branch
						il.MarkLabel(skipIfNotNull);

						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load the backing field
						il.Emit(OperationCode.Ldfld, backingField);

						// Return
						il.Emit(OperationCode.Ret);
					}
				}

				// Amend the existing method
				else
				{
					// Before Get
					if (propertyAmendment.BeforeGet != null)
					{
						// Get the delegate to call before the property getter
						var beforeGet = ResolvePropertyDelegate(propertyDef, propertyAmendment.BeforeGet);

						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load property name onto stack
						il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

						// Call BeforeGet delegate
						il.Emit(OperationCode.Call, beforeGet.Instance);
					}

					// Lazy Initializer and/or After Get
					if (propertyAmendment.LazyInitializer != null || propertyAmendment.AfterGet != null)
					{
						// After Get
						if (propertyAmendment.AfterGet != null)
						{
							// Load this pointer onto stack
							il.Emit(OperationCode.Ldarg_0);

							// Load property name onto stack
							il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);
						}

						// Emit the original property getter instructions
						il.EmitUntilReturn();

						// Lazy Initializer
						if (propertyAmendment.LazyInitializer != null)
						{
							// Add local variable to store the current value of the property
							var currentValue = new LocalDefinition() { Name = host.NameTable.GetNameFor("_cv_"), Type = propertyDef.Type };
							methodBody.LocalVariables.Add(currentValue);

							// Store the current value in a local variable
							il.Emit(OperationCode.Stloc, currentValue);

							// Load the current value
							il.Emit(OperationCode.Ldloc, currentValue);
		
							// Skip initialization if the backing field is already initialized
							var skipIfNotNull = new ILGeneratorLabel();
							il.Emit(OperationCode.Brtrue_S, skipIfNotNull);

							// Load this pointer onto stack
							il.Emit(OperationCode.Ldarg_0);

							// Load this pointer onto stack again
							il.Emit(OperationCode.Ldarg_0);

							// Load property name onto stack
							il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

							// Call LazyInitializer delegate
							il.Emit(OperationCode.Call, ResolvePropertyDelegate(propertyDef, propertyAmendment.LazyInitializer).Instance);

							// Store the initialized value
							il.Emit(OperationCode.Stloc, currentValue);

							// Load the initialized value
							il.Emit(OperationCode.Ldloc, currentValue);

							// Call the property setter
							il.Emit(propertyDef.Setter.ResolvedMethod.IsVirtual ? OperationCode.Callvirt : OperationCode.Call, propertyDef.Setter);

							// End the skipping branch
							il.MarkLabel(skipIfNotNull);

							// Load the current value
							il.Emit(OperationCode.Ldloc, currentValue);
						}

						// Call AfterGet delegate
						if (propertyAmendment.AfterGet != null)
							il.Emit(OperationCode.Call, ResolvePropertyDelegate(propertyDef, propertyAmendment.AfterGet).Instance);
					}

					// Emit the rest of the original method if not amending after get
					else
						il.EmitUntilReturn();

				}
			}

			// Property Set
			else
			{
				// Setter for new or existing property
				if (propertyAmendment.Setter != null)
				{
					// Clear the original method body
					il.Operations.Clear();

					// Get the delegate to call 
					var setter = ResolvePropertyDelegate(propertyDef, propertyAmendment.Setter);

					// Load this pointer onto stack
					il.Emit(OperationCode.Ldarg_0);

					// Load property name onto stack
					il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

					// Load value argument onto stack
					il.Emit(OperationCode.Ldarg_1);

					// Call Setter delegate
					il.Emit(OperationCode.Call, setter.Instance);

					// Return
					il.Emit(OperationCode.Ret);
				}

				// New property without setter
				else if (propertyAmendment.PropertyInfo == null)
				{
					// Load this pointer onto stack
					il.Emit(OperationCode.Ldarg_0);

					// Load value argument onto stack
					il.Emit(OperationCode.Ldarg_1);

					// Save to backing field
					il.Emit(OperationCode.Stfld, GetCurrentType().Fields.Where(f => f.Name.Value == GetBackingFieldName(propertyAmendment)).First());

					// Return
					il.Emit(OperationCode.Ret);
				}

				// Amend the existing method
				else if (propertyAmendment.BeforeSet != null || propertyAmendment.AfterSet != null)
				{
					// Get the before and after set delegate implementations
					var beforeSet = propertyAmendment.BeforeSet != null ? ResolvePropertyDelegate(propertyDef, propertyAmendment.BeforeSet) : null;
					var afterSet = propertyAmendment.AfterSet != null ? ResolvePropertyDelegate(propertyDef, propertyAmendment.AfterSet) : null;

					// Determine if the old value is used by the amended delegates
					bool useOriginalValue = beforeSet.ReferencesArgument(2) || afterSet.ReferencesArgument(2);

					// Emit a local variable and store the initial property value by calling the property getter
					if (useOriginalValue)
					{
						// Add local variable to store the old value of the property
						methodBody.LocalVariables.Add(new LocalDefinition() { Name = host.NameTable.GetNameFor("_ov_"), Type = propertyDef.Type });

						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Call the property getter to get the old value
						il.Emit(propertyDef.Getter.ResolvedMethod.IsVirtual ? OperationCode.Callvirt : OperationCode.Call, propertyDef.Getter.ResolvedMethod);

						// Store the old value in a local variable
						il.Emit(OperationCode.Stloc, methodBody.LocalVariables.Last());
					}

					// Before Set
					if (beforeSet != null)
					{
						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load property name onto stack
						il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

						// Optionally load the old value onto stack
						if (useOriginalValue)
							il.Emit(OperationCode.Ldloc, methodBody.LocalVariables.Last());
						else
							il.Emit(OperationCode.Ldarg_1);

						// Load value argument pointer
						il.Emit(OperationCode.Ldarg_1);

						// Call BeforeSet delegate
						il.Emit(OperationCode.Call, beforeSet.Instance);

						// Store the result as the new value for the setter
						il.Emit(OperationCode.Starg, methodBody.MethodDefinition.Parameters.First());
					}

					// Emit the old property setter instructions
					il.EmitUntilReturn();

					// After Set
					if (afterSet != null)
					{
						// Load this pointer onto stack
						il.Emit(OperationCode.Ldarg_0);

						// Load property name onto stack
						il.Emit(OperationCode.Ldstr, propertyDef.Name.Value);

						// Optionally load the old value onto stack
						if (useOriginalValue)
							il.Emit(OperationCode.Ldloc, methodBody.LocalVariables.Last());
						else
							il.Emit(OperationCode.Ldarg_1);

						// Load value argument onto stack
						il.Emit(OperationCode.Ldarg_1);

						// Optionally get the new property value if used
						if (afterSet.ReferencesArgument(4))
						{
							// Load this pointer onto stack
							il.Emit(OperationCode.Ldarg_0);

							// Call property getter
							il.Emit(propertyDef.Getter.ResolvedMethod.IsVirtual ? OperationCode.Callvirt : OperationCode.Call, propertyDef.Getter.ResolvedMethod);
						}
						else
							il.Emit(OperationCode.Ldarg_1);

						// Call AfterSet delegate
						il.Emit(OperationCode.Call, afterSet.Instance);
					}
				}
			}

			// Update the method body
			il.UpdateMethodBody(6);
		}

		/// <summary>
		/// Amends the method body with the specified method amendment.
		/// </summary>
		/// <param name="methodAmendment"></param>
		/// <param name="methodBody"></param>
		void AmendMethod(IMethodAmendment methodAmendment, MethodBody methodBody)
		{
			// Create an IL generator to amend the operations
			var il = new ILAmender(host, methodBody);

			// Before Method
			if (methodAmendment.Before != null)
				CallMethodDelegate(methodBody, methodAmendment.Before, true, il, null);

			// Implementation
			Action implement = () =>
			{
				if (methodAmendment.Implementation != null)
				{
					// Clear the original method body
					il.Operations.Clear();
					CallMethodDelegate(methodBody, methodAmendment.Implementation, false, il, null);
				}

				// Emit the original method operations if the method implementation was not overriden
				if (methodAmendment.MethodInfo != null && methodAmendment.Implementation == null)
					il.EmitUntilReturn();
			};

			// After Method
			if (methodAmendment.After != null)
				CallMethodDelegate(methodBody, methodAmendment.After, false, il, implement);
			else
				implement();

			// Or emit a return for new/overriden methods
			if (methodAmendment.Implementation != null)
				il.Emit(OperationCode.Ret);

			// Update the method body
			il.UpdateMethodBody(6);
		}

		/// <summary>
		/// Calls the specified method delegate.
		/// </summary>
		/// <param name="methodBody"></param>
		/// <param name="method"></param>
		/// <param name="updateArguments"></param>
		/// <param name="il"></param>
		/// <param name="implement"></param>
		void CallMethodDelegate(MethodBody methodBody, System.Reflection.MethodInfo method, bool updateArguments, ILAmender il, Action implement)
		{
			// Get the corresponding before method definition
			var methodDef = methodBody.MethodDefinition;
			
			// Get the method definition to emit a call to
			var targetMethodDef = ResolveMethodDelegate(methodBody.MethodDefinition.ContainingType, method);
			
			// Load this pointer onto stack
			il.Emit(OperationCode.Ldarg_0);

			// Load method name onto stack
			il.Emit(OperationCode.Ldstr, methodBody.MethodDefinition.Name.Value);

			// Conditionally handle parameters
			var parameters = method.GetParameters();
			if (parameters.Length >= 3 && (typeof(Parameter).IsAssignableFrom(parameters[2].ParameterType) || parameters[2].ParameterType == typeof(object[])))
			{
				Type argType = parameters[2].ParameterType;
				ITypeDefinition argTypeDef = targetMethodDef.Parameters.Skip(2).First().Type.ResolvedType;

				// Add local variable to store the return value before delegate
				var args = new LocalDefinition() { Name = host.NameTable.GetNameFor("_args_"), Type = argTypeDef };
				methodBody.LocalVariables.Add(args);

				// Array
				if (argType == typeof(object[]))
				{
					// Load the number of arguments onto the stack
					il.Emit(OperationCode.Ldc_I4, methodDef.Parameters.Count());

					// Create an object array
					il.Emit(OperationCode.Newarr, argTypeDef);

					// Store the new array
					il.Emit(OperationCode.Stloc, args);

					// Populate the array with method arguments
					foreach (var parameter in methodDef.Parameters)
					{
						// Load the argument array
						il.Emit(OperationCode.Ldloc, args);

						// Add argument index to the stack
						il.Emit(OperationCode.Ldc_I4, parameter.Index);

						// Load the argument
						il.Emit(OperationCode.Ldarg, parameter);

						// Box value types
						if (parameter.Type.IsValueType)
							il.Emit(OperationCode.Box, parameter.Type);

						// Store the argument in the array
						il.Emit(OperationCode.Stelem_Ref);
					}

					// Load the argument array
					il.Emit(OperationCode.Ldloc, args);

					// Emit the method implementation here if specified
					if (implement != null)
						implement();

					// Call the target method
					il.Emit(OperationCode.Call, targetMethodDef);

					// Handle method delegates that potentially return updated arguments
					if (updateArguments)
					{
						// Store the updated argument array
						il.Emit(OperationCode.Stloc, args);

						// Load the argument array
						il.Emit(OperationCode.Ldloc, args);
		
						// Skip null return values
						var skipIfNull = new ILGeneratorLabel();
						il.Emit(OperationCode.Brfalse_S, skipIfNull);

						// Update the method arguments with the result of the invocation
						foreach (var parameter in methodDef.Parameters)
						{
							// Load the arguments back on the stack
							il.Emit(OperationCode.Ldloc, args);

							// Load the index of the argument value
							il.Emit(OperationCode.Ldc_I4, parameter.Index);

							// Get the argument value from the array
							il.Emit(OperationCode.Ldelem_Ref);

							// Unbox value types
							if (parameter.Type.IsValueType)
								il.Emit(OperationCode.Unbox_Any, parameter.Type);

							// Update the actual argument
							il.Emit(OperationCode.Starg, parameter);
						}

						// End the skipping branch
						il.MarkLabel(skipIfNull);
					}
				}

				// Parameter<T1,...,TN>
				else
				{
					// Load the method arguments onto the stack
					foreach (var parameter in methodDef.Parameters)
						il.Emit(OperationCode.Ldarg, parameter);

					// Create the parameter argument
					il.Emit(OperationCode.Newobj, argTypeDef.Methods.Where(m => m.IsConstructor).First());

					// Emit the method implementation here if specified
					if (implement != null)
						implement();

					// Call the target method
					il.Emit(OperationCode.Call, targetMethodDef);

					// Handle method delegates that potentially return updated arguments
					if (updateArguments)
					{
						// Store the return value
						il.Emit(OperationCode.Stloc, args);

						// Load the arguments back on the stack
						il.Emit(OperationCode.Ldloc, args);

						// Skip null return values
						var skipIfNull = new ILGeneratorLabel();
						il.Emit(OperationCode.Brfalse_S, skipIfNull);

						// Update the method arguments with the result of the invocation
						foreach (var parameter in methodDef.Parameters)
						{
							// Load the arguments back on the stack
							il.Emit(OperationCode.Ldloc, args);

							// Get the argument value
							il.Emit(OperationCode.Call, GetProperty(argTypeDef, "Param" + (parameter.Index + 1)).Getter);

							// Update the actual argument
							il.Emit(OperationCode.Starg, parameter);
						}

						// End the skipping branch
						il.MarkLabel(skipIfNull);
					}
				}
			}

			// Otherwise, just call the target method
			else
				il.Emit(OperationCode.Call, methodDef);
		}

		/// <summary>
		/// Get the property with the specified name on the current type or base types.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		IPropertyDefinition GetProperty(ITypeDefinition type, string property)
		{
			var propertyDef = type.Properties.Where(p => p.Name.Value == property).FirstOrDefault();
			if (propertyDef == null && type.BaseClasses.Any())
				return GetProperty(type.BaseClasses.Select(t => t.ResolvedType).FirstOrDefault(), property);
			return propertyDef;
		}
	
		/// <summary>
		/// Get the <see cref="INamedTypeDefinition"/> corresponding to the specified <see cref="Type"/>.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		ITypeDefinition ResolveType(Type type)
		{
			// Handle arrays
			if (type.IsArray)
			{
				ITypeDefinition elementTypeDef = ResolveType(type.GetElementType());
				return Vector.GetVector(elementTypeDef, host.InternFactory);
			}

			System.Reflection.Assembly assembly = type.Assembly;

			// See if the assembly is already loaded
			IAssembly assemblyDef = null;
			if (assembly.FullName.StartsWith("mscorlib") && TargetRuntimeVersion.StartsWith("v2"))
				assemblyDef = host.FindAssembly(host.CoreAssemblySymbolicIdentity);
			else
			{
				foreach (var unit in host.LoadedUnits)
				{
					if (unit.Name.Value == assembly.FullName)
					{
						assemblyDef = (IAssembly)unit;
						break;
					}
				}
			}

			// Otherwise, load the assembly
			if (assemblyDef == null)
				assemblyDef = (IAssembly)host.LoadUnitFrom(assembly.Location);

			// Handle generic types
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				var genericType = type.GetGenericTypeDefinition();
				var genericTypeDef = assemblyDef.GetAllTypes()
					.OfType<INamedTypeDefinition>()
					.Where(t => AreEquivalent(t, genericType))
					.FirstOrDefault();

				// Return the generic type
				return GenericTypeInstance.GetGenericTypeInstance(genericTypeDef, type.GetGenericArguments().Select(t => ResolveType(t)), host.InternFactory);
			}

			// Otherwise, just find the type
			else
				return assemblyDef.GetAllTypes()
					.OfType<INamedTypeDefinition>()
					.Where(t => AreEquivalent(t, type))
					.FirstOrDefault();
		}

		/// <summary>
		/// Gets the <see cref="IFieldDefinition"/> that corresponds to the specified <see cref="FieldInfo"/>.
		/// </summary>
		/// <param name="declaringType"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		IFieldDefinition ResolveField(ITypeDefinition declaringType, System.Reflection.FieldInfo field)
		{
			return declaringType.Fields.Where(f => AreEquivalent(f, field)).FirstOrDefault();
		}

		/// <summary>
		/// Gets the <see cref="IMethodDefinition"/> that corresponds to the specified <see cref="ConstructorInfo"/>.
		/// </summary>
		/// <param name="v"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		IMethodDefinition ResolveConstructor(ITypeDefinition declaringType, System.Reflection.ConstructorInfo constructor)
		{
			return declaringType.Methods.Where(m => AreEquivalent(m, constructor)).FirstOrDefault();
		}

		/// <summary>
		/// Gets the <see cref="IPropertyDefinition"/> that corresponds to the specified <see cref="PropertyInfo"/>.
		/// </summary>
		/// <param name="declaringType"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		IPropertyDefinition ResolveProperty(ITypeDefinition declaringType, System.Reflection.PropertyInfo property)
		{
			return declaringType.Properties.Where(p => AreEquivalent(p, property)).FirstOrDefault();
		}
	
		/// <summary>
		/// Gets the <see cref="IMethodDefinition"/> that corresponds to the specified <see cref="MethodInfo"/>.
		/// </summary>
		/// <param name="v"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		IMethodDefinition ResolveMethod(ITypeDefinition declaringType, System.Reflection.MethodInfo method)
		{
			return TypeHelper.GetMethod(declaringType, host.NameTable.GetNameFor(method.Name), method.GetParameters().Select(p => ResolveType(p.ParameterType)).ToArray());
//			return declaringType.Methods.Where(m => AreEquivalent(m, method)).FirstOrDefault();
		}

		/// <summary>
		/// Indicates whether the specified <see cref="INamedTypeDefinition"/> and
		/// <see cref="Type"/> are equivalent, representing the same type.
		/// </summary>
		/// <param name="typeDef"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		internal static bool AreEquivalent(INamedTypeDefinition typeDef, Type type)
		{
			return typeDef.ToString() + (typeDef.MangleName ? "`" + typeDef.GenericParameterCount : "") == type.FullName;
		}

		/// <summary>
		/// Indicates whether the specified <see cref="IFieldDefinition"/> and
		/// <see cref="FieldInfo"/> are equivalent, representing the same property.
		/// </summary>
		/// <param name="fieldDef"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		internal bool AreEquivalent(IFieldDefinition fieldDef, System.Reflection.FieldInfo field)
		{
			return

				// Ensure field names match
				fieldDef.Name.Value == field.Name &&

				// Ensure field types match
				TypeHelper.TypesAreEquivalent(fieldDef.Type, ResolveType(field.FieldType));
		}

		/// <summary>
		/// Indicates whether the specified <see cref="IMethodDefinition"/> and
		/// <see cref="MethodBase"/> are equivalent, representing the same method.
		/// </summary>
		/// <param name="methodDef"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		internal bool AreEquivalent(IMethodDefinition methodDef, System.Reflection.MethodBase method)
		{
			return
				
				// Ensure method names match
				methodDef.Name.Value == method.Name && 

				// Ensure parameter counts match
				methodDef.ParameterCount == method.GetParameters().Length &&

				// Ensure generic type parameter counts match
				methodDef.GenericParameterCount == method.GetGenericArguments().Length &&

				// Ensure parameter types match
				method.GetParameters().Select(p => ResolveType(p.ParameterType)).Cast<ITypeDefinition>().SequenceEqual(methodDef.Parameters.Select(p => p.Type.ResolvedType)) &&

				// Ensure generic parameter types match
				method.GetGenericArguments().Select(t => ResolveType(t)).Cast<ITypeDefinition>().SequenceEqual(methodDef.GenericParameters.Cast<IGenericParameterReference>().Select(p => p.ResolvedType));
		}

		/// <summary>
		/// Indicates whether the specified <see cref="IPropertyDefinition"/> and
		/// <see cref="PropertyInfo"/> are equivalent, representing the same property.
		/// </summary>
		/// <param name="propertyDef"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		internal bool AreEquivalent(IPropertyDefinition propertyDef, System.Reflection.PropertyInfo property)
		{
			return

				// Ensure property names match
				propertyDef.Name.Value == property.Name &&

				// Ensure property types match
				TypeHelper.TypesAreEquivalent(propertyDef.Type, ResolveType(property.PropertyType)) &&

				// Ensure parameter counts match
				propertyDef.Parameters.Count() == property.GetIndexParameters().Length &&

				// Ensure parameter types match
				property.GetIndexParameters().Select(p => ResolveType(p.ParameterType)).Cast<ITypeDefinition>().SequenceEqual(propertyDef.Parameters.Select(p => p.Type.ResolvedType));
		}

		GenericMethod ResolvePropertyDelegate(IPropertyDefinition propertyDef, System.Reflection.MethodInfo method)
		{
			var genericType = ResolveType(method.DeclaringType.GetGenericTypeDefinition());

			// Create a concrete type instance
			var concreteType = GenericTypeInstance.GetGenericTypeInstance(genericType, new ITypeReference[] { propertyDef.ContainingType }, host.InternFactory);

			// Then get the generic Amendment method
			var genericMethod = concreteType.Methods.Where(m => m.Name.Value == method.Name && m.ParameterCount == method.GetParameters().Length).FirstOrDefault();

			// Finally, return a concrete method instance
			return new GenericMethod()
			{
				Operations = genericType.Methods.Where(m => m.Name.Value == method.Name && m.ParameterCount == method.GetParameters().Length).FirstOrDefault().Body.Operations,
				Instance = genericMethod.GenericParameterCount == 0 ? genericMethod : new GenericMethodInstance(genericMethod, new ITypeReference[] { propertyDef.Type }, host.InternFactory)
					
			};
		}

		IMethodDefinition ResolveMethodDelegate(ITypeReference instanceType, System.Reflection.MethodInfo method)
		{
			var genericType = ResolveType(method.DeclaringType.GetGenericTypeDefinition());

			// Create a concrete type instance
			var concreteType = GenericTypeInstance.GetGenericTypeInstance(genericType, new ITypeReference[] { instanceType }, host.InternFactory);

			// Then get the Amendment method
			return concreteType.Methods.Where(m => m.Name.Value == method.Name && m.ParameterCount == method.GetParameters().Length).FirstOrDefault();
		}


		/// <summary>
		/// Gets the name of the private backing field to use for new generated properties.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		string GetBackingFieldName(IPropertyAmendment property)
		{
			// Return backing field name consistent with C# compiler conventions
			return "<" + (property.Implements != null ? property.Implements.DeclaringType.FullName.Replace('.', '_') : "") + property.Name + ">k__BackingField";
		}
	}

	internal class GenericMethod
	{
		public IEnumerable<IOperation> Operations { get; set; }
		public IMethodReference Instance { get; set; }
	}

	internal static class MethodHelper
	{
		/// <summary>
		/// Determines whether the specified <see cref="MethodBody"/> references
		/// and argument at the specified index.
		/// </summary>
		/// <param name="body"></param>
		/// <param name="argument"></param>
		/// <returns></returns>
		internal static bool ReferencesArgument(this GenericMethod method, int argument)
		{
			return method != null && method.Operations.Where(o =>
			{
				switch (o.OperationCode)
				{
					case OperationCode.Ldarg:
					case OperationCode.Ldarg_S:
					case OperationCode.Ldarga:
					case OperationCode.Ldarga_S:
					case OperationCode.Starg:
					case OperationCode.Starg_S:
						return ((IParameterListEntry)o.Value).Index == argument;
					case OperationCode.Ldarg_0:
						return argument == 0;
					case OperationCode.Ldarg_1:
						return argument == 1;
					case OperationCode.Ldarg_2:
						return argument == 2;
					case OperationCode.Ldarg_3:
						return argument == 3;
					default:
						return false;
				}
			})
			.Any();
		}
	}
}
