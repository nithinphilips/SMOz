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
using System.Reflection;

namespace Afterthought
{
	#region Amendment

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending
	/// a specific <see cref="Type"/> af compilation.
	/// </summary>
	public abstract partial class Amendment : ITypeAmendment
	{
		#region Fields

		bool initialized;
		internal List<Type> interfaces = new List<Type>();
		internal List<Field> fields = new List<Field>();
		internal List<Constructor> constructors = new List<Constructor>();
		internal List<Property> properties = new List<Property>();
		internal List<Method> methods = new List<Method>();
		internal List<Event> events = new List<Event>();
		internal List<Attribute> attributes = new List<Attribute>();

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs and initializes a new <see cref="Amendment"/> for a specific <see cref="Type"/>.
		/// </summary>
		internal Amendment()
		{ }

		#endregion

		#region Properties

		public abstract Type Type { get; }

		public abstract Type AmendedType { get; }

		string IMemberAmendment.Name { get { return Type.FullName; } }

		IEnumerable<Type> ITypeAmendment.Interfaces
		{
			get
			{
				return interfaces;
			}
		}

		IEnumerable<IAttributeAmendment> IMemberAmendment.Attributes
		{
			get
			{
				return attributes.Cast<IAttributeAmendment>();
			}
		}

		IEnumerable<IConstructorAmendment> ITypeAmendment.Constructors
		{
			get
			{
				return constructors.Cast<IConstructorAmendment>();
			}
		}

		IEnumerable<IFieldAmendment> ITypeAmendment.Fields
		{
			get
			{
				return fields.Cast<IFieldAmendment>();
			}
		}

		IEnumerable<IPropertyAmendment> ITypeAmendment.Properties
		{
			get
			{
				return properties.Cast<IPropertyAmendment>();
			}
		}

		IEnumerable<IMethodAmendment> ITypeAmendment.Methods
		{
			get
			{
				return methods.Cast<IMethodAmendment>();
			}
		}

		IEnumerable<IEventAmendment> ITypeAmendment.Events
		{
			get
			{
				return events.Cast<IEventAmendment>();
			}
		}

		#endregion

		#region Methods

		internal void Initialize()
		{
			// Exit if the amendment has already been initialized
			if (initialized)
				return;
			initialized = true;

			Type amendmentType = typeof(Amendment<,>).MakeGenericType(Type, AmendedType);

			// Allow the amendment to perform type-level changes
			Amend();

			// Build field amendments
			foreach (var fieldInfo in Type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				Type fieldAmendmentType = amendmentType.GetNestedType("Field`1").MakeGenericType(Type, AmendedType, fieldInfo.FieldType);
				Field field = (Field)fieldAmendmentType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(FieldInfo) }, null).Invoke(new object[] { fieldInfo });
				MethodInfo amend = amendmentType.GetMethods()
					.Where(m => m.Name == "Amend" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.BaseType == typeof(Amendment.Field))
					.FirstOrDefault();
				amend.MakeGenericMethod(fieldInfo.FieldType).Invoke(this, new object[] { field });
				if (field.IsAmended)
					fields.Add(field);
			}

			// Build constructor amendments
			foreach (var constructorInfo in Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Type constructorAmendmentType = amendmentType.GetNestedType("Constructor").MakeGenericType(Type, AmendedType);
				Constructor constructor = (Constructor)constructorAmendmentType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ConstructorInfo) }, null).Invoke(new object[] { constructorInfo });
				MethodInfo amend = amendmentType.GetMethods()
					.Where(m => m.Name == "Amend" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.BaseType == typeof(Amendment.Constructor))
					.FirstOrDefault();
				amend.Invoke(this, new object[] { constructor });
				if (constructor.IsAmended)
					constructors.Add(constructor);
			}

			// Build property amendments
			foreach (var propertyInfo in Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				Type propertyAmendmentType = amendmentType.GetNestedType("Property`1").MakeGenericType(Type, AmendedType, propertyInfo.PropertyType);
				Property property = (Property)propertyAmendmentType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(PropertyInfo) }, null).Invoke(new object[] { propertyInfo });
				MethodInfo amend = amendmentType.GetMethods()
					.Where(m => m.Name == "Amend" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.BaseType == typeof(Amendment.Property))
					.FirstOrDefault();
				amend.MakeGenericMethod(propertyInfo.PropertyType).Invoke(this, new object[] { property });
				if (property.IsAmended)
					properties.Add(property);
			}

			// Build method amendments
			foreach (var methodInfo in Type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)

				// Exclude constructors and properties
				.Where(m => !m.IsConstructor && !(m.IsSpecialName && m.IsHideBySig && (m.Name.StartsWith("get_") || m.Name.StartsWith("set_")))))
			{
				Type methodAmendmentType = amendmentType.GetNestedType("Method").MakeGenericType(Type, AmendedType);
				Method method = (Method)methodAmendmentType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(MethodInfo) }, null).Invoke(new object[] { methodInfo });
				MethodInfo amend = amendmentType.GetMethods()
					.Where(m => m.Name == "Amend" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.BaseType == typeof(Amendment.Method))
					.FirstOrDefault();
				amend.Invoke(this, new object[] { method });
				if (method.IsAmended)
					methods.Add(method);
			}

			// Build event amendments
			foreach (var eventInfo in Type.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				Type eventAmendmentType = amendmentType.GetNestedType("Event`1").MakeGenericType(Type, AmendedType, eventInfo.EventHandlerType);
				Event @event = (Event)eventAmendmentType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(EventInfo) }, null).Invoke(new object[] { eventInfo });
				MethodInfo amend = amendmentType.GetMethods()
					.Where(m => m.Name == "Amend" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.BaseType == typeof(Amendment.Event))
					.FirstOrDefault();
				amend.MakeGenericMethod(eventInfo.EventHandlerType).Invoke(this, new object[] { @event });
				if (@event.IsAmended)
					events.Add(@event);
			}
		}

		/// <summary>
		/// Allows subclasses to amend in changes to the current type.
		/// </summary>
		public virtual void Amend()
		{

		}

		public override string ToString()
		{
			return Type.Name;
		}

		#endregion
	}

	#endregion

	#region Amendment<TType, TAmended>

	public partial class Amendment<TType, TAmended> : Amendment
	{
		#region Properties

		public override Type Type
		{
			get
			{
				return typeof(TType);
			}
		}

		public override Type AmendedType
		{
			get
			{
				return typeof(TAmended);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Allows subclasses to amend fields.
		/// </summary>
		/// <typeparam name="F"></typeparam>
		/// <param name="field"></param>
		public virtual void Amend<TField>(Field<TField> field)
		{
		}

		/// <summary>
		/// Allows subclasses to amend constructors.
		/// </summary>
		/// <param name="constructor"></param>
		public virtual void Amend(Constructor constructor)
		{
		}

		/// <summary>
		/// Allows subclasses to amend properties.
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="property"></param>
		public virtual void Amend<TProperty>(Property<TProperty> property)
		{
		}

		/// <summary>
		/// Allows subclasses to amend methods.
		/// </summary>
		/// <param name="method"></param>
		public virtual void Amend(Method method)
		{
		}

		/// <summary>
		/// Allows subclasses to amend events.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		/// <param name="event"></param>
		public virtual void Amend<TEvent>(Event<TEvent> @event)
		{
		}

		/// <summary>
		/// Allows subclasses to add new attributes to the types being amended.
		/// </summary>
		/// <param name="attribute"></param>
		public void AddAttribute(Attribute attribute)
		{
			attributes.Add(attribute);
		}

		/// <summary>
		/// Allows subclasses to add new fields to the types being amended.
		/// </summary>
		/// <typeparam name="TField"></typeparam>
		/// <param name="field"></param>
		public void AddField<TField>(Field<TField> field)
		{
			fields.Add(field);
		}

		/// <summary>
		/// Allows subclasses to add new constructors to the types being amended.
		/// </summary>
		/// <param name="constructor"></param>
		public void AddConstructor(Constructor constructor)
		{
			if (constructor.ImplementationMethod == null)
				throw new ArgumentException("Constructors being added must have an implementation");

			constructors.Add(constructor);
		}

		/// <summary>
		/// Allows subclasses to add new properties to the types being amended.
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="property"></param>
		public void AddProperty<TProperty>(Property<TProperty> property)
		{
			properties.Add(property);
		}

		/// <summary>
		/// Allows subclasses to add new methods to the types being woven.
		/// </summary>
		/// <param name="method"></param>
		public void AddMethod(Method method)
		{
			if (method.ImplementationMethod == null && method.OverrideMethod == null)
				throw new ArgumentException("Methods being added must have an implementation or override an inherited method");

			methods.Add(method);
		}

		/// <summary>
		/// Allows subclasses to add new events to the types being amended.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		/// <param name="event"></param>
		public void AddEvent<TEvent>(Event<TEvent> @event)
		{
			events.Add(@event);
		}

		/// <summary>
		/// Allows subclasses to implement interfaces for the types being amended.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <param name="members"></param>
		public void ImplementInterface<TInterface>(params InterfaceMember[] members)
		{
			var interfaceType = typeof(TInterface);

			// Ensure the specified type is an interface
			if (interfaceType == null || !interfaceType.IsInterface)
				throw new ArgumentException("Only interface types can be implemented.");

			// Track the interface being implemented
			interfaces.Add(interfaceType);

			// Add members that implement the interface
			if (members != null)
			{
				foreach (var member in members)
				{
					// Properties
					if (member is Property)
					{
						var property = (Property)member;

						// Determine the property being implemented
						property.Implements = interfaceType.GetProperty(property.Name);

						// Verify that the property actually implements the specified interface
						if (property.Implements == null || property.Implements.PropertyType != property.Type)
							throw new ArgumentException("The specified property, " + property.Name + ", is not valid for interface " + interfaceType.FullName + ".");

						// Add the new property
						this.properties.Add(property);
					}

					// Methods
					else if (member is Method)
					{
						var method = (Method)member;

						// Verify that the method has an implementation or raises an event
						if (method.ImplementationMethod == null && method.RaisesEvent == null)
							throw new ArgumentException("The method must have an implementation in order to implement an interface.");

						// Get the method arguments
						var args = method.ImplementationMethod != null ? 
							method.ImplementationMethod.GetParameters().Skip(1).Select(p => p.ParameterType).ToArray() :
							method.RaisesEvent.Type.GetMethod("Invoke").GetParameters()
								.SkipWhile((p, i) => (i == 0 && p.ParameterType == typeof(object)) || (i == 1 && p.ParameterType == typeof(EventArgs)))
								.Select(p => p.ParameterType).ToArray();

						// Determine the method being implemented
						method.Implements = interfaceType.GetMethods()
							.FirstOrDefault(m => m.Name == method.Name && m.GetParameters().Length == args.Length &&
								m.GetParameters().All(p => args[p.Position] == p.ParameterType));

						// Verify that the method actually implements the specified interface
						if (method.Implements == null)
							throw new ArgumentException("The specified method, " + method.Name + ", is not valid for interface " + interfaceType.FullName + ".");

						// Add the new method
						this.methods.Add(method);
					}

					// Events
					if (member is Event)
					{
						var @event = (Event)member;

						// Determine the event being implemented
						@event.Implements = interfaceType.GetEvent(@event.Name);

						// Verify that the event actually implements the specified interface
						if (@event.Implements == null || @event.Implements.EventHandlerType != @event.Type)
							throw new ArgumentException("The specified event, " + @event.Name + ", is not valid for interface " + interfaceType.FullName + ".");

						// Add the new event
						this.events.Add(@event);
					}
				}
			}
		}

		#endregion
	}

	#endregion
}
