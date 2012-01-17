//-----------------------------------------------------------------------------
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.Cci.UtilityDataStructures;
using System.Diagnostics.Contracts;

//^ using Microsoft.Contracts;
namespace Microsoft.Cci {

  /// <summary>
  /// A visitor base class that traverses the object model in depth first, left to right order.
  /// </summary>
  public class BaseMetadataTraverser : IMetadataVisitor {

    /// <summary>
    /// 
    /// </summary>
    public BaseMetadataTraverser() {
    }

    //^ [SpecPublic]
    /// <summary>
    /// 
    /// </summary>
    protected System.Collections.Stack path = new System.Collections.Stack();

    /// <summary>
    /// 
    /// </summary>
    protected bool stopTraversal;

    #region IMetadataVisitor Members

    /// <summary>
    /// Visits the specified aliases for types.
    /// </summary>
    /// <param name="aliasesForTypes">The aliases for types.</param>
    public virtual void Visit(IEnumerable<IAliasForType> aliasesForTypes)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IAliasForType aliasForType in aliasesForTypes) {
        this.Visit(aliasForType);
        if (this.stopTraversal) return;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified alias for type.
    /// </summary>
    /// <param name="aliasForType">Type of the alias for.</param>
    public virtual void Visit(IAliasForType aliasForType)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(aliasForType);
      this.Visit(aliasForType.AliasedType);
      this.Visit(aliasForType.Attributes);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      aliasForType.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the alias for type, as a reference.
    /// </summary>
    /// <param name="aliasForType"></param>
    public virtual void VisitReference(IAliasForType aliasForType) {
      aliasForType.DispatchAsReference(this);
    }

    /// <summary>
    /// Performs some computation with the given array type reference.
    /// </summary>
    /// <param name="arrayTypeReference"></param>
    public virtual void Visit(IArrayTypeReference arrayTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(arrayTypeReference);
      this.Visit(arrayTypeReference.ElementType);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given assembly.
    /// </summary>
    /// <param name="assembly"></param>
    public virtual void Visit(IAssembly assembly)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      this.Visit((IModule)assembly);
      this.Visit(assembly.AssemblyAttributes);
      this.Visit(assembly.ExportedTypes);
      this.Visit(assembly.Files);
      this.Visit(assembly.MemberModules);
      this.Visit(assembly.Resources);
      this.Visit(assembly.SecurityAttributes);
    }

    /// <summary>
    /// Visits the specified assembly references.
    /// </summary>
    /// <param name="assemblyReferences">The assembly references.</param>
    public virtual void Visit(IEnumerable<IAssemblyReference> assemblyReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IAssemblyReference assemblyReference in assemblyReferences) {
        this.Visit((IUnitReference)assemblyReference);
        if (this.stopTraversal) return;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given assembly reference.
    /// </summary>
    /// <param name="assemblyReference"></param>
    public virtual void Visit(IAssemblyReference assemblyReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified custom attributes.
    /// </summary>
    /// <param name="customAttributes">The custom attributes.</param>
    public virtual void Visit(IEnumerable<ICustomAttribute> customAttributes)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ICustomAttribute customAttribute in customAttributes) {
        this.Visit(customAttribute);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given custom attribute.
    /// </summary>
    /// <param name="customAttribute"></param>
    public virtual void Visit(ICustomAttribute customAttribute)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(customAttribute);
      this.Visit(customAttribute.Arguments);
      this.Visit(customAttribute.Constructor);
      this.Visit(customAttribute.NamedArguments);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified custom modifiers.
    /// </summary>
    /// <param name="customModifiers">The custom modifiers.</param>
    public virtual void Visit(IEnumerable<ICustomModifier> customModifiers)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ICustomModifier customModifier in customModifiers) {
        this.Visit(customModifier);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given custom modifier.
    /// </summary>
    /// <param name="customModifier"></param>
    public virtual void Visit(ICustomModifier customModifier)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(customModifier);
      this.Visit(customModifier.Modifier);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified events.
    /// </summary>
    /// <param name="events">The events.</param>
    public virtual void Visit(IEnumerable<IEventDefinition> events)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IEventDefinition eventDef in events) {
        this.Visit((ITypeDefinitionMember)eventDef);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given event definition.
    /// </summary>
    /// <param name="eventDefinition"></param>
    public virtual void Visit(IEventDefinition eventDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(eventDefinition);
      this.Visit(eventDefinition.Accessors);
      this.Visit(eventDefinition.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified fields.
    /// </summary>
    /// <param name="fields">The fields.</param>
    public virtual void Visit(IEnumerable<IFieldDefinition> fields)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IFieldDefinition field in fields) {
        this.Visit((ITypeDefinitionMember)field);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given field definition.
    /// </summary>
    /// <param name="fieldDefinition"></param>
    public virtual void Visit(IFieldDefinition fieldDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(fieldDefinition);
      if (fieldDefinition.IsCompileTimeConstant)
        this.Visit((IMetadataExpression)fieldDefinition.CompileTimeValue);
      if (fieldDefinition.IsModified)
        this.Visit(fieldDefinition.CustomModifiers);
      if (fieldDefinition.IsMarshalledExplicitly)
        this.Visit(fieldDefinition.MarshallingInformation);
      this.Visit(fieldDefinition.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given field reference.
    /// </summary>
    /// <param name="fieldReference"></param>
    public virtual void Visit(IFieldReference fieldReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      this.Visit((ITypeMemberReference)fieldReference);
      //^ int oldCount = this.path.Count;
      this.path.Push(fieldReference);
      if (fieldReference.IsModified)
        this.Visit(fieldReference.CustomModifiers);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified file references.
    /// </summary>
    /// <param name="fileReferences">The file references.</param>
    public virtual void Visit(IEnumerable<IFileReference> fileReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IFileReference fileReference in fileReferences) {
        this.Visit(fileReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given file reference.
    /// </summary>
    /// <param name="fileReference"></param>
    public virtual void Visit(IFileReference fileReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given function pointer type reference.
    /// </summary>
    /// <param name="functionPointerTypeReference"></param>
    public virtual void Visit(IFunctionPointerTypeReference functionPointerTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(functionPointerTypeReference);
      this.Visit(functionPointerTypeReference.Type);
      this.Visit(functionPointerTypeReference.Parameters);
      this.Visit(functionPointerTypeReference.ExtraArgumentTypes);
      if (functionPointerTypeReference.ReturnValueIsModified)
        this.Visit(functionPointerTypeReference.ReturnValueCustomModifiers);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given generic method instance reference.
    /// </summary>
    /// <param name="genericMethodInstanceReference"></param>
    public virtual void Visit(IGenericMethodInstanceReference genericMethodInstanceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified generic parameters.
    /// </summary>
    /// <param name="genericParameters">The generic parameters.</param>
    public virtual void Visit(IEnumerable<IGenericMethodParameter> genericParameters)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IGenericMethodParameter genericParameter in genericParameters) {
        this.Visit((IGenericParameter)genericParameter);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter.
    /// </summary>
    /// <param name="genericMethodParameter"></param>
    public virtual void Visit(IGenericMethodParameter genericMethodParameter)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter reference.
    /// </summary>
    /// <param name="genericMethodParameterReference"></param>
    public virtual void Visit(IGenericMethodParameterReference genericMethodParameterReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified generic parameter.
    /// </summary>
    /// <param name="genericParameter">The generic parameter.</param>
    public virtual void Visit(IGenericParameter genericParameter) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(genericParameter);
      this.Visit(genericParameter.Attributes);
      this.Visit(genericParameter.Constraints);
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      genericParameter.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given generic type instance reference.
    /// </summary>
    /// <param name="genericTypeInstanceReference"></param>
    public virtual void Visit(IGenericTypeInstanceReference genericTypeInstanceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(genericTypeInstanceReference);
      this.Visit(genericTypeInstanceReference.GenericType);
      this.Visit(genericTypeInstanceReference.GenericArguments);
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified generic parameters.
    /// </summary>
    /// <param name="genericParameters">The generic parameters.</param>
    public virtual void Visit(IEnumerable<IGenericTypeParameter> genericParameters)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IGenericTypeParameter genericParameter in genericParameters) {
        this.Visit((IGenericParameter)genericParameter);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given generic parameter.
    /// </summary>
    /// <param name="genericTypeParameter"></param>
    public virtual void Visit(IGenericTypeParameter genericTypeParameter)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given generic type parameter reference.
    /// </summary>
    /// <param name="genericTypeParameterReference"></param>
    public virtual void Visit(IGenericTypeParameterReference genericTypeParameterReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given global field definition.
    /// </summary>
    /// <param name="globalFieldDefinition"></param>
    public virtual void Visit(IGlobalFieldDefinition globalFieldDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      this.Visit((IFieldDefinition)globalFieldDefinition);
    }

    /// <summary>
    /// Performs some computation with the given global method definition.
    /// </summary>
    /// <param name="globalMethodDefinition"></param>
    public virtual void Visit(IGlobalMethodDefinition globalMethodDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      this.Visit((IMethodDefinition)globalMethodDefinition);
    }

    /// <summary>
    /// Visits the specified local definitions.
    /// </summary>
    /// <param name="localDefinitions">The local definitions.</param>
    public virtual void Visit(IEnumerable<ILocalDefinition> localDefinitions) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ILocalDefinition localDefinition in localDefinitions) {
        this.Visit(localDefinition);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified local definition.
    /// </summary>
    /// <param name="localDefinition">The local definition.</param>
    public virtual void Visit(ILocalDefinition localDefinition) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(localDefinition);
      if (localDefinition.IsModified)
        this.Visit(localDefinition.CustomModifiers);
      this.Visit(localDefinition.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified local definition as a reference.
    /// </summary>
    /// <param name="localDefinition">The local definition.</param>
    public virtual void VisitReference(ILocalDefinition localDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given managed pointer type reference.
    /// </summary>
    /// <param name="managedPointerTypeReference"></param>
    public virtual void Visit(IManagedPointerTypeReference managedPointerTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given marshalling information.
    /// </summary>
    /// <param name="marshallingInformation"></param>
    public virtual void Visit(IMarshallingInformation marshallingInformation)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(marshallingInformation);
      if (marshallingInformation.UnmanagedType == System.Runtime.InteropServices.UnmanagedType.CustomMarshaler)
        this.Visit(marshallingInformation.CustomMarshaller);
      if (marshallingInformation.UnmanagedType == System.Runtime.InteropServices.UnmanagedType.SafeArray && 
      (marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_DISPATCH ||
      marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_UNKNOWN ||
      marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_RECORD))
        this.Visit(marshallingInformation.SafeArrayElementUserDefinedSubtype);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given metadata constant.
    /// </summary>
    /// <param name="constant"></param>
    public virtual void Visit(IMetadataConstant constant)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given metadata array creation expression.
    /// </summary>
    /// <param name="createArray"></param>
    public virtual void Visit(IMetadataCreateArray createArray)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(createArray);
      this.Visit(createArray.ElementType);
      this.Visit(createArray.Initializers);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.      this.path.Pop();
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified expressions.
    /// </summary>
    /// <param name="expressions">The expressions.</param>
    public virtual void Visit(IEnumerable<IMetadataExpression> expressions)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IMetadataExpression expression in expressions) {
        this.Visit(expression);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given metadata expression.
    /// </summary>
    /// <param name="expression"></param>
    public virtual void Visit(IMetadataExpression expression) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(expression);
      this.Visit(expression.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
      this.path.Pop();
      expression.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified named arguments.
    /// </summary>
    /// <param name="namedArguments">The named arguments.</param>
    public virtual void Visit(IEnumerable<IMetadataNamedArgument> namedArguments)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IMetadataNamedArgument namedArgument in namedArguments) {
        this.Visit((IMetadataExpression)namedArgument);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given metadata named argument expression.
    /// </summary>
    /// <param name="namedArgument"></param>
    public virtual void Visit(IMetadataNamedArgument namedArgument)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(namedArgument);
      this.Visit(namedArgument.ArgumentValue);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given metadata typeof expression.
    /// </summary>
    /// <param name="typeOf"></param>
    public virtual void Visit(IMetadataTypeOf typeOf)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(typeOf);
      this.Visit(typeOf.TypeToGet);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not to decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given method body.
    /// </summary>
    /// <param name="methodBody"></param>
    public virtual void Visit(IMethodBody methodBody)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(methodBody);
      this.Visit(methodBody.LocalVariables);
      this.Visit(methodBody.Operations);
      this.Visit(methodBody.OperationExceptionInformation);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified methods.
    /// </summary>
    /// <param name="methods">The methods.</param>
    public virtual void Visit(IEnumerable<IMethodDefinition> methods)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IMethodDefinition method in methods) {
        this.Visit((ITypeDefinitionMember)method);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given method definition.
    /// </summary>
    /// <param name="method"></param>
    public virtual void Visit(IMethodDefinition method)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(method);
      this.VisitMethodReturnAttributes(method.ReturnValueAttributes);
      if (method.ReturnValueIsModified)
        this.Visit(method.ReturnValueCustomModifiers);
      if (method.HasDeclarativeSecurity)
        this.Visit(method.SecurityAttributes);
      if (method.IsGeneric) this.Visit(method.GenericParameters);
      this.Visit(method.Type);
      this.Visit(method.Parameters);
      if (method.IsPlatformInvoke)
        this.Visit(method.PlatformInvokeData);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified method implementations.
    /// </summary>
    /// <param name="methodImplementations">The method implementations.</param>
    public virtual void Visit(IEnumerable<IMethodImplementation> methodImplementations)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IMethodImplementation methodImplementation in methodImplementations) {
        this.Visit(methodImplementation);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given method implementation.
    /// </summary>
    /// <param name="methodImplementation"></param>
    public virtual void Visit(IMethodImplementation methodImplementation)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(methodImplementation);
      this.Visit(methodImplementation.ImplementedMethod);
      this.Visit(methodImplementation.ImplementingMethod);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified method references.
    /// </summary>
    /// <param name="methodReferences">The method references.</param>
    public virtual void Visit(IEnumerable<IMethodReference> methodReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IMethodReference methodReference in methodReferences) {
        this.Visit(methodReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given method reference.
    /// </summary>
    /// <param name="methodReference"></param>
    public virtual void Visit(IMethodReference methodReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      IGenericMethodInstanceReference/*?*/ genericMethodInstanceReference = methodReference as IGenericMethodInstanceReference;
      if (genericMethodInstanceReference != null)
        this.Visit(genericMethodInstanceReference);
      else
        this.Visit((ITypeMemberReference)methodReference);
    }

    /// <summary>
    /// Performs some computation with the given modified type reference.
    /// </summary>
    /// <param name="modifiedTypeReference"></param>
    public virtual void Visit(IModifiedTypeReference modifiedTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(modifiedTypeReference);
      this.Visit(modifiedTypeReference.CustomModifiers);
      this.Visit(modifiedTypeReference.UnmodifiedType);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given module.
    /// </summary>
    /// <param name="module"></param>
    public virtual void Visit(IModule module)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(module);
      this.Visit(module.ModuleAttributes);
      this.Visit(module.AssemblyReferences);
      this.Visit(module.NamespaceRoot);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified modules.
    /// </summary>
    /// <param name="modules">The modules.</param>
    public virtual void Visit(IEnumerable<IModule> modules)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IModule module in modules) {
        this.Visit((IUnit)module);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified module references.
    /// </summary>
    /// <param name="moduleReferences">The module references.</param>
    public virtual void Visit(IEnumerable<IModuleReference> moduleReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IModuleReference moduleReference in moduleReferences) {
        this.Visit((IUnitReference)moduleReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given module reference.
    /// </summary>
    /// <param name="moduleReference"></param>
    public virtual void Visit(IModuleReference moduleReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified types.
    /// </summary>
    /// <param name="types">The types.</param>
    public virtual void Visit(IEnumerable<INamedTypeDefinition> types)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (INamedTypeDefinition type in types) {
        this.Visit(type);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified namespace members.
    /// </summary>
    /// <param name="namespaceMembers">The namespace members.</param>
    public virtual void Visit(IEnumerable<INamespaceMember> namespaceMembers)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (INamespaceMember namespaceMember in namespaceMembers) {
        this.Visit(namespaceMember);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given alias for a namespace type definition.
    /// </summary>
    /// <param name="namespaceAliasForType"></param>
    public virtual void Visit(INamespaceAliasForType namespaceAliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given alias for a namespace type definition, as a reference.
    /// </summary>
    /// <param name="namespaceAliasForType"></param>
    public virtual void VisitReference(INamespaceAliasForType namespaceAliasForType) {
    }

    /// <summary>
    /// Visits the specified namespace member.
    /// </summary>
    /// <param name="namespaceMember">The namespace member.</param>
    public virtual void Visit(INamespaceMember namespaceMember) {
      if (this.stopTraversal) return;
      INamespaceDefinition/*?*/ nestedNamespace = namespaceMember as INamespaceDefinition;
      if (nestedNamespace != null)
        this.Visit(nestedNamespace);
      else {
        ITypeDefinition/*?*/ namespaceType = namespaceMember as ITypeDefinition;
        if (namespaceType != null)
          this.Visit(namespaceType);
        else {
          ITypeDefinitionMember/*?*/ globalFieldOrMethod = namespaceMember as ITypeDefinitionMember;
          if (globalFieldOrMethod != null)
            this.Visit(globalFieldOrMethod);
          else {
            INamespaceAliasForType/*?*/ namespaceAlias = namespaceMember as INamespaceAliasForType;
            if (namespaceAlias != null)
              this.Visit(namespaceAlias);
            else {
              //TODO: error
              namespaceMember.Dispatch(this);
            }
          }
        }
      }
    }

    /// <summary>
    /// Performs some computation with the given namespace type definition.
    /// </summary>
    /// <param name="namespaceTypeDefinition"></param>
    public virtual void Visit(INamespaceTypeDefinition namespaceTypeDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given namespace type reference.
    /// </summary>
    /// <param name="namespaceTypeReference"></param>
    public virtual void Visit(INamespaceTypeReference namespaceTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given alias to a nested type definition.
    /// </summary>
    /// <param name="nestedAliasForType"></param>
    public virtual void Visit(INestedAliasForType nestedAliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given alias to a nested type definition, as a reference.
    /// </summary>
    public virtual void VisitReference(INestedAliasForType nestedAliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace reference.
    /// </summary>
    /// <param name="nestedUnitNamespaceReference"></param>
    public virtual void Visit(INestedUnitNamespaceReference nestedUnitNamespaceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified nested types.
    /// </summary>
    /// <param name="nestedTypes">The nested types.</param>
    public virtual void Visit(IEnumerable<INestedTypeDefinition> nestedTypes)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (INestedTypeDefinition nestedType in nestedTypes) {
        this.Visit((ITypeDefinitionMember)nestedType);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given nested type definition.
    /// </summary>
    /// <param name="nestedTypeDefinition"></param>
    public virtual void Visit(INestedTypeDefinition nestedTypeDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given nested type reference.
    /// </summary>
    /// <param name="nestedTypeReference"></param>
    public virtual void Visit(INestedTypeReference nestedTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(nestedTypeReference);
      this.Visit(nestedTypeReference.ContainingType);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace.
    /// </summary>
    /// <param name="nestedUnitNamespace"></param>
    public virtual void Visit(INestedUnitNamespace nestedUnitNamespace)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given nested unit set namespace.
    /// </summary>
    /// <param name="nestedUnitSetNamespace"></param>
    public virtual void Visit(INestedUnitSetNamespace nestedUnitSetNamespace)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified operations.
    /// </summary>
    /// <param name="operations">The operations.</param>
    public virtual void Visit(IEnumerable<IOperation> operations) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IOperation operation in operations) {
        this.Visit(operation);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified operation.
    /// </summary>
    /// <param name="operation">The operation.</param>
    public virtual void Visit(IOperation operation) {
      ITypeReference/*?*/ typeReference = operation.Value as ITypeReference;
      if (typeReference != null) {
        if (operation.OperationCode == OperationCode.Newarr) {
          //^ assume operation.Value is IArrayTypeReference;
          this.Visit(((IArrayTypeReference)operation.Value).ElementType);
        } else
          this.Visit(typeReference);
      } else {
        IFieldReference/*?*/ fieldReference = operation.Value as IFieldReference;
        if (fieldReference != null)
          this.Visit(fieldReference);
        else {
          IMethodReference/*?*/ methodReference = operation.Value as IMethodReference;
          if (methodReference != null)
            this.Visit(methodReference);
          else {
            IParameterDefinition/*?*/ parameter = operation.Value as IParameterDefinition;
            if (parameter != null)
              this.VisitReference(parameter);
            else {
              ILocalDefinition/*?*/ local = operation.Value as ILocalDefinition;
              if (local != null)
                this.VisitReference(local);
            }
          }
        }
      }
    }

    /// <summary>
    /// Visits the specified operation exception informations.
    /// </summary>
    /// <param name="operationExceptionInformations">The operation exception informations.</param>
    public virtual void Visit(IEnumerable<IOperationExceptionInformation> operationExceptionInformations) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IOperationExceptionInformation operationExceptionInformation in operationExceptionInformations) {
        this.Visit(operationExceptionInformation);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified operation exception information.
    /// </summary>
    /// <param name="operationExceptionInformation">The operation exception information.</param>
    public virtual void Visit(IOperationExceptionInformation operationExceptionInformation) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(operationExceptionInformation);
      this.Visit(operationExceptionInformation.ExceptionType);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public virtual void Visit(IEnumerable<IParameterDefinition> parameters)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IParameterDefinition parameter in parameters) {
        this.Visit(parameter);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given parameter definition.
    /// </summary>
    /// <param name="parameterDefinition"></param>
    public virtual void Visit(IParameterDefinition parameterDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(parameterDefinition);
      this.Visit(parameterDefinition.Attributes);
      if (parameterDefinition.IsModified)
        this.Visit(parameterDefinition.CustomModifiers);
      if (parameterDefinition.HasDefaultValue)
        this.Visit((IMetadataExpression)parameterDefinition.DefaultValue);
      if (parameterDefinition.IsMarshalledExplicitly)
        this.Visit(parameterDefinition.MarshallingInformation);
      this.Visit(parameterDefinition.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given parameter definition, as a reference.
    /// </summary>
    /// <param name="parameterDefinition"></param>
    public virtual void VisitReference(IParameterDefinition parameterDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified parameter type informations.
    /// </summary>
    /// <param name="parameterTypeInformations">The parameter type informations.</param>
    public virtual void Visit(IEnumerable<IParameterTypeInformation> parameterTypeInformations)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IParameterTypeInformation parameterTypeInformation in parameterTypeInformations) {
        this.Visit(parameterTypeInformation);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given parameter type information.
    /// </summary>
    /// <param name="parameterTypeInformation"></param>
    public virtual void Visit(IParameterTypeInformation parameterTypeInformation)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(parameterTypeInformation);
      if (parameterTypeInformation.IsModified)
        this.Visit(parameterTypeInformation.CustomModifiers);
      this.Visit(parameterTypeInformation.Type);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified platform invoke information.
    /// </summary>
    /// <param name="platformInvokeInformation">The platform invoke information.</param>
    public virtual void Visit(IPlatformInvokeInformation platformInvokeInformation)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(platformInvokeInformation);
      this.Visit(platformInvokeInformation.ImportModule);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Performs some computation with the given pointer type reference.
    /// </summary>
    /// <param name="pointerTypeReference"></param>
    public virtual void Visit(IPointerTypeReference pointerTypeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(pointerTypeReference);
      this.Visit(pointerTypeReference.TargetType);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified properties.
    /// </summary>
    /// <param name="properties">The properties.</param>
    public virtual void Visit(IEnumerable<IPropertyDefinition> properties)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IPropertyDefinition property in properties) {
        this.Visit((ITypeDefinitionMember)property);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given property definition.
    /// </summary>
    /// <param name="propertyDefinition"></param>
    public virtual void Visit(IPropertyDefinition propertyDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(propertyDefinition);
      this.Visit(propertyDefinition.Accessors);
      this.Visit(propertyDefinition.Parameters);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified resource references.
    /// </summary>
    /// <param name="resourceReferences">The resource references.</param>
    public virtual void Visit(IEnumerable<IResourceReference> resourceReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IResourceReference resourceReference in resourceReferences) {
        this.Visit(resourceReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given reference to a manifest resource.
    /// </summary>
    /// <param name="resourceReference"></param>
    public virtual void Visit(IResourceReference resourceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace.
    /// </summary>
    /// <param name="rootUnitNamespace"></param>
    public virtual void Visit(IRootUnitNamespace rootUnitNamespace)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given root unit set namespace.
    /// </summary>
    /// <param name="rootUnitSetNamespace"></param>
    public virtual void Visit(IRootUnitSetNamespace rootUnitSetNamespace)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Performs some computation with the given security attribute.
    /// </summary>
    /// <param name="securityAttribute"></param>
    public virtual void Visit(ISecurityAttribute securityAttribute)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(securityAttribute);
      this.Visit(securityAttribute.Attributes);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified security attributes.
    /// </summary>
    /// <param name="securityAttributes">The security attributes.</param>
    public virtual void Visit(IEnumerable<ISecurityAttribute> securityAttributes)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ISecurityAttribute securityAttribute in securityAttributes) {
        this.Visit(securityAttribute);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Performs some computation with the given specialized event definition.
    /// </summary>
    public virtual void Visit(ISpecializedEventDefinition specializedEventDefinition) {
      this.Visit((IEventDefinition)specializedEventDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized field definition.
    /// </summary>
    public virtual void Visit(ISpecializedFieldDefinition specializedFieldDefinition) {
      this.Visit((IFieldDefinition)specializedFieldDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized field reference.
    /// </summary>
    public virtual void Visit(ISpecializedFieldReference specializedFieldReference) {
      this.Visit((IFieldReference)specializedFieldReference);
    }

    /// <summary>
    /// Performs some computation with the given specialized method definition.
    /// </summary>
    public virtual void Visit(ISpecializedMethodDefinition specializedMethodDefinition) {
      this.Visit((IMethodDefinition)specializedMethodDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized method reference.
    /// </summary>
    public virtual void Visit(ISpecializedMethodReference specializedMethodReference) {
      this.Visit((IMethodReference)specializedMethodReference);
    }

    /// <summary>
    /// Performs some computation with the given specialized propperty definition.
    /// </summary>
    public virtual void Visit(ISpecializedPropertyDefinition specializedPropertyDefinition) {
      this.Visit((IPropertyDefinition)specializedPropertyDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type definition.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeDefinition specializedNestedTypeDefinition) {
      this.Visit((INestedTypeDefinition)specializedNestedTypeDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type reference.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeReference specializedNestedTypeReference) {
      this.Visit((INestedTypeReference)specializedNestedTypeReference);
    }

    /// <summary>
    /// Visits the specified type members.
    /// </summary>
    /// <param name="typeMembers">The type members.</param>
    public virtual void Visit(IEnumerable<ITypeDefinitionMember> typeMembers)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ITypeDefinitionMember typeMember in typeMembers) {
        this.Visit(typeMember);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified types.
    /// </summary>
    /// <param name="types">The types.</param>
    public virtual void Visit(IEnumerable<ITypeDefinition> types)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ITypeDefinition type in types) {
        this.Visit(type);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified type definition.
    /// </summary>
    /// <param name="typeDefinition">The type definition.</param>
    public virtual void Visit(ITypeDefinition typeDefinition) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(typeDefinition);
      this.Visit(typeDefinition.Attributes);
      this.Visit(typeDefinition.BaseClasses);
      this.Visit(typeDefinition.ExplicitImplementationOverrides);
      if (typeDefinition.HasDeclarativeSecurity)
        this.Visit(typeDefinition.SecurityAttributes);
      this.Visit(typeDefinition.Interfaces);
      if (typeDefinition.IsGeneric)
        this.Visit(typeDefinition.GenericParameters);
      this.Visit(typeDefinition.Members);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      typeDefinition.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified type member.
    /// </summary>
    /// <param name="typeMember">The type member.</param>
    public virtual void Visit(ITypeDefinitionMember typeMember) {
      if (this.stopTraversal) return;
      ITypeDefinition/*?*/ nestedType = typeMember as ITypeDefinition;
      if (nestedType != null)
        this.Visit(nestedType);
      else {
        //^ int oldCount = this.path.Count;
        this.path.Push(typeMember);
        this.Visit(typeMember.Attributes);
        //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
        this.path.Pop();
        typeMember.Dispatch(this);
      }
    }

    /// <summary>
    /// Visits the specified type member reference.
    /// </summary>
    /// <param name="typeMemberReference">The type member reference.</param>
    public virtual void Visit(ITypeMemberReference typeMemberReference) {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(typeMemberReference);
      if (!(typeMemberReference is IDefinition))
        this.Visit(typeMemberReference.Attributes); //In principle, refererences can have attributes that are distinct from the definitions they refer to.
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified type references.
    /// </summary>
    /// <param name="typeReferences">The type references.</param>
    public virtual void Visit(IEnumerable<ITypeReference> typeReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ITypeReference typeReference in typeReferences) {
        this.Visit(typeReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified type reference.
    /// </summary>
    /// <param name="typeReference">The type reference.</param>
    public virtual void Visit(ITypeReference typeReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      this.DispatchAsReference(typeReference);
    }

    /// <summary>
    /// Use this routine, rather than ITypeReference.Dispatch, to call the appropriate derived overload of an ITypeReference.
    /// The former routine will call Visit(INamespaceTypeDefinition) rather than Visit(INamespaceTypeReference), etc., 
    /// in the case where a definition is used as a reference to itself.
    /// </summary>
    /// <param name="typeReference">A reference to a type definition. Note that a type definition can serve as a reference to itself.</param>
    protected void DispatchAsReference(ITypeReference typeReference) {
      INamespaceTypeReference/*?*/ namespaceTypeReference = typeReference as INamespaceTypeReference;
      if (namespaceTypeReference != null) {
        this.Visit(namespaceTypeReference);
        return;
      }
      INestedTypeReference/*?*/ nestedTypeReference = typeReference as INestedTypeReference;
      if (nestedTypeReference != null) {
        this.Visit(nestedTypeReference);
        return;
      }
      IArrayTypeReference/*?*/ arrayTypeReference = typeReference as IArrayTypeReference;
      if (arrayTypeReference != null) {
        this.Visit(arrayTypeReference);
        return;
      }
      IGenericTypeInstanceReference/*?*/ genericTypeInstanceReference = typeReference as IGenericTypeInstanceReference;
      if (genericTypeInstanceReference != null) {
        this.Visit(genericTypeInstanceReference);
        return;
      }
      IGenericTypeParameterReference/*?*/ genericTypeParameterReference = typeReference as IGenericTypeParameterReference;
      if (genericTypeParameterReference != null) {
        this.Visit(genericTypeParameterReference);
        return;
      }
      IGenericMethodParameterReference/*?*/ genericMethodParameterReference = typeReference as IGenericMethodParameterReference;
      if (genericMethodParameterReference != null) {
        this.Visit(genericMethodParameterReference);
        return;
      }
      IPointerTypeReference/*?*/ pointerTypeReference = typeReference as IPointerTypeReference;
      if (pointerTypeReference != null) {
        this.Visit(pointerTypeReference);
        return;
      }
      IFunctionPointerTypeReference/*?*/ functionPointerTypeReference = typeReference as IFunctionPointerTypeReference;
      if (functionPointerTypeReference != null) {
        this.Visit(functionPointerTypeReference);
        return;
      }
      IModifiedTypeReference/*?*/ modifiedTypeReference = typeReference as IModifiedTypeReference;
      if (modifiedTypeReference != null) {
        this.Visit(modifiedTypeReference);
        return;
      }
    }

    /// <summary>
    /// Visits the specified unit.
    /// </summary>
    /// <param name="unit">The unit.</param>
    public virtual void Visit(IUnit unit)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(unit);
      this.Visit(unit.NamespaceRoot);
      this.Visit(unit.UnitReferences);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      unit.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified unit references.
    /// </summary>
    /// <param name="unitReferences">The unit references.</param>
    public virtual void Visit(IEnumerable<IUnitReference> unitReferences)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (IUnitReference unitReference in unitReferences) {
        this.Visit(unitReference);
        if (this.stopTraversal) break;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    /// <summary>
    /// Visits the specified unit reference.
    /// </summary>
    /// <param name="unitReference">The unit reference.</param>
    public virtual void Visit(IUnitReference unitReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      this.DispatchAsReference(unitReference);
    }

    /// <summary>
    /// Use this routine, rather than IUnitReference.Dispatch, to call the appropriate derived overload of an IUnitReference.
    /// The former routine will call Visit(IAssembly) rather than Visit(IAssemblyReference), etc.
    /// in the case where a definition is used as the reference to itself.
    /// </summary>
    /// <param name="unitReference">A reference to a unit. Note that a unit can serve as a reference to itself.</param>
    private void DispatchAsReference(IUnitReference unitReference) {
      IAssemblyReference/*?*/ assemblyReference = unitReference as IAssemblyReference;
      if (assemblyReference != null) {
        this.Visit(assemblyReference);
        return;
      }
      IModuleReference/*?*/ moduleReference = unitReference as IModuleReference;
      if (moduleReference != null) {
        this.Visit(moduleReference);
        return;
      }
    }

    /// <summary>
    /// Visits the specified namespace definition.
    /// </summary>
    /// <param name="namespaceDefinition">The namespace definition.</param>
    public virtual void Visit(INamespaceDefinition namespaceDefinition)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(namespaceDefinition);
      this.Visit(namespaceDefinition.Members);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      namespaceDefinition.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace reference.
    /// </summary>
    /// <param name="rootUnitNamespaceReference"></param>
    public virtual void Visit(IRootUnitNamespaceReference rootUnitNamespaceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the specified unit namespace reference.
    /// </summary>
    /// <param name="unitNamespaceReference">The unit namespace reference.</param>
    public virtual void Visit(IUnitNamespaceReference unitNamespaceReference)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      unitNamespaceReference.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given unit set.
    /// </summary>
    /// <param name="unitSet"></param>
    public virtual void Visit(IUnitSet unitSet)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(unitSet);
      this.Visit(unitSet.UnitSetNamespaceRoot);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
    }

    /// <summary>
    /// Visits the specified unit set namespace.
    /// </summary>
    /// <param name="unitSetNamespace">The unit set namespace.</param>
    public virtual void Visit(IUnitSetNamespace unitSetNamespace)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      this.path.Push(unitSetNamespace);
      this.Visit(unitSetNamespace.Members);
      //^ assume this.path.Count == oldCount+1; //True because all of the virtual methods of this class promise not decrease this.path.Count.
      this.path.Pop();
      unitSetNamespace.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given Win32 resource.
    /// </summary>
    /// <param name="win32Resource"></param>
    public virtual void Visit(IWin32Resource win32Resource)
      //^ ensures this.path.Count == old(this.path.Count);
    {
    }

    /// <summary>
    /// Visits the method return attributes.
    /// </summary>
    /// <param name="customAttributes">The custom attributes.</param>
    public virtual void VisitMethodReturnAttributes(IEnumerable<ICustomAttribute> customAttributes)
      //^ ensures this.path.Count == old(this.path.Count);
    {
      if (this.stopTraversal) return;
      //^ int oldCount = this.path.Count;
      foreach (ICustomAttribute customAttribute in customAttributes) {
        this.Visit(customAttribute);
        if (this.stopTraversal) return;
      }
      //^ assume this.path.Count == oldCount; //True because all of the virtual methods of this class promise not decrease this.path.Count.
    }

    #endregion
  }

  /// <summary>
  /// A visitor base class that provides a dummy body for each method of IVisit.
  /// </summary>
  public class BaseMetadataVisitor : IMetadataVisitor {

    /// <summary>
    /// 
    /// </summary>
    public BaseMetadataVisitor() {
    }

    #region IMetadataVisitor Members

    /// <summary>
    /// Visits the specified alias for type.
    /// </summary>
    public virtual void Visit(IAliasForType aliasForType) {
      //IAliasForType is a base interface that should never be implemented directly.
      //Get aliasForType to call the most type specific visitor.
      aliasForType.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified alias for type.
    /// </summary>
    public virtual void VisitReference(IAliasForType aliasForType) {
      //IAliasForType is a base interface that should never be implemented directly.
      //Get aliasForType to call the most type specific visitor.
      aliasForType.DispatchAsReference(this);
    }

    /// <summary>
    /// Performs some computation with the given array type reference.
    /// </summary>
    public virtual void Visit(IArrayTypeReference arrayTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given assembly.
    /// </summary>
    public virtual void Visit(IAssembly assembly) {
    }

    /// <summary>
    /// Performs some computation with the given assembly reference.
    /// </summary>
    public virtual void Visit(IAssemblyReference assemblyReference) {
    }

    /// <summary>
    /// Performs some computation with the given custom attribute.
    /// </summary>
    public virtual void Visit(ICustomAttribute customAttribute) {
    }

    /// <summary>
    /// Performs some computation with the given custom modifier.
    /// </summary>
    public virtual void Visit(ICustomModifier customModifier) {
    }

    /// <summary>
    /// Performs some computation with the given event definition.
    /// </summary>
    public virtual void Visit(IEventDefinition eventDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given field definition.
    /// </summary>
    public virtual void Visit(IFieldDefinition fieldDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given field reference.
    /// </summary>
    public virtual void Visit(IFieldReference fieldReference) {
    }

    /// <summary>
    /// Performs some computation with the given file reference.
    /// </summary>
    public virtual void Visit(IFileReference fileReference) {
    }

    /// <summary>
    /// Performs some computation with the given function pointer type reference.
    /// </summary>
    public virtual void Visit(IFunctionPointerTypeReference functionPointerTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given generic method instance reference.
    /// </summary>
    public virtual void Visit(IGenericMethodInstanceReference genericMethodInstanceReference) {
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter.
    /// </summary>
    public virtual void Visit(IGenericMethodParameter genericMethodParameter) {
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter reference.
    /// </summary>
    public virtual void Visit(IGenericMethodParameterReference genericMethodParameterReference) {
    }

    /// <summary>
    /// Performs some computation with the given generic type instance reference.
    /// </summary>
    public virtual void Visit(IGenericTypeInstanceReference genericTypeInstanceReference) {
    }

    /// <summary>
    /// Performs some computation with the given generic parameter.
    /// </summary>
    public virtual void Visit(IGenericTypeParameter genericTypeParameter) {
    }

    /// <summary>
    /// Performs some computation with the given generic type parameter reference.
    /// </summary>
    public virtual void Visit(IGenericTypeParameterReference genericTypeParameterReference) {
    }

    /// <summary>
    /// Performs some computation with the given global field definition.
    /// </summary>
    public virtual void Visit(IGlobalFieldDefinition globalFieldDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given global method definition.
    /// </summary>
    public virtual void Visit(IGlobalMethodDefinition globalMethodDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given local definition.
    /// </summary>
    public virtual void Visit(ILocalDefinition localDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given local definition.
    /// </summary>
    public virtual void VisitReference(ILocalDefinition localDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given managed pointer type reference.
    /// </summary>
    public virtual void Visit(IManagedPointerTypeReference managedPointerTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given marshalling information.
    /// </summary>
    public virtual void Visit(IMarshallingInformation marshallingInformation) {
    }

    /// <summary>
    /// Performs some computation with the given metadata constant.
    /// </summary>
    public virtual void Visit(IMetadataConstant constant) {
    }

    /// <summary>
    /// Performs some computation with the given metadata array creation expression.
    /// </summary>
    public virtual void Visit(IMetadataCreateArray createArray) {
    }

    /// <summary>
    /// Performs some computation with the given metadata expression.
    /// </summary>
    public virtual void Visit(IMetadataExpression expression) {
      //IMetadataExpression is a base interface that should never be implemented directly.
      //Get expression to call the most type specific visitor.
      expression.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given metadata named argument expression.
    /// </summary>
    public virtual void Visit(IMetadataNamedArgument namedArgument) {
    }

    /// <summary>
    /// Performs some computation with the given metadata typeof expression.
    /// </summary>
    public virtual void Visit(IMetadataTypeOf typeOf) {
    }

    /// <summary>
    /// Performs some computation with the given method body.
    /// </summary>
    public virtual void Visit(IMethodBody methodBody) {
    }

    /// <summary>
    /// Performs some computation with the given method definition.
    /// </summary>
    public virtual void Visit(IMethodDefinition method) {
    }

    /// <summary>
    /// Performs some computation with the given method implementation.
    /// </summary>
    public virtual void Visit(IMethodImplementation methodImplementation) {
    }

    /// <summary>
    /// Performs some computation with the given method reference.
    /// </summary>
    public virtual void Visit(IMethodReference methodReference) {
    }

    /// <summary>
    /// Performs some computation with the given modified type reference.
    /// </summary>
    public virtual void Visit(IModifiedTypeReference modifiedTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given module.
    /// </summary>
    public virtual void Visit(IModule module) {
    }

    /// <summary>
    /// Performs some computation with the given module reference.
    /// </summary>
    public virtual void Visit(IModuleReference moduleReference) {
    }

    /// <summary>
    /// Performs some computation with the given alias for a namespace type definition.
    /// </summary>
    public virtual void Visit(INamespaceAliasForType namespaceAliasForType) {
    }

    /// <summary>
    /// Visits the specified namespace definition.
    /// </summary>
    public virtual void Visit(INamespaceDefinition namespaceDefinition) {
      //INamespaceDefinition is a base interface that should never be implemented directly.
      //Get namespaceDefinition to call the most type specific visitor.
      namespaceDefinition.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified namespace member.
    /// </summary>
    public virtual void Visit(INamespaceMember namespaceMember) {
      //INamespaceMember is a base interface that should never be implemented directly.
      //Get namespaceMember to call the most type specific visitor.
      namespaceMember.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given namespace type definition.
    /// </summary>
    public virtual void Visit(INamespaceTypeDefinition namespaceTypeDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given namespace type reference.
    /// </summary>
    public virtual void Visit(INamespaceTypeReference namespaceTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given alias to a nested type definition.
    /// </summary>
    public virtual void Visit(INestedAliasForType nestedAliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given alias to a nested type definition.
    /// </summary>
    public virtual void VisitReference(INestedAliasForType nestedAliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given nested type definition.
    /// </summary>
    public virtual void Visit(INestedTypeDefinition nestedTypeDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given nested type reference.
    /// </summary>
    public virtual void Visit(INestedTypeReference nestedTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace.
    /// </summary>
    public virtual void Visit(INestedUnitNamespace nestedUnitNamespace) {
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace reference.
    /// </summary>
    public virtual void Visit(INestedUnitNamespaceReference nestedUnitNamespaceReference) {
    }

    /// <summary>
    /// Performs some computation with the given nested unit set namespace.
    /// </summary>
    public virtual void Visit(INestedUnitSetNamespace nestedUnitSetNamespace) {
    }

    /// <summary>
    /// Performs some computation with the given IL operation.
    /// </summary>
    public virtual void Visit(IOperation operation) {
    }

    /// <summary>
    /// Performs some computation with the given IL operation exception information instance.
    /// </summary>
    public virtual void Visit(IOperationExceptionInformation operationExceptionInformation) {
    }

    /// <summary>
    /// Performs some computation with the given parameter definition.
    /// </summary>
    public virtual void Visit(IParameterDefinition parameterDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given parameter definition.
    /// </summary>
    public virtual void VisitReference(IParameterDefinition parameterDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given property definition.
    /// </summary>
    public virtual void Visit(IPropertyDefinition propertyDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given parameter type information.
    /// </summary>
    public virtual void Visit(IParameterTypeInformation parameterTypeInformation) {
    }

    /// <summary>
    /// Performs some compuation with the given platoform invoke information.
    /// </summary>
    public virtual void Visit(IPlatformInvokeInformation platformInvokeInformation) {
    }

    /// <summary>
    /// Performs some computation with the given pointer type reference.
    /// </summary>
    public virtual void Visit(IPointerTypeReference pointerTypeReference) {
    }

    /// <summary>
    /// Performs some computation with the given reference to a manifest resource.
    /// </summary>
    public virtual void Visit(IResourceReference resourceReference) {
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace.
    /// </summary>
    public virtual void Visit(IRootUnitNamespace rootUnitNamespace) {
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace reference.
    /// </summary>
    public virtual void Visit(IRootUnitNamespaceReference rootUnitNamespaceReference) {
    }

    /// <summary>
    /// Performs some computation with the given root unit set namespace.
    /// </summary>
    public virtual void Visit(IRootUnitSetNamespace rootUnitSetNamespace) {
    }

    /// <summary>
    /// Performs some computation with the given security attribute.
    /// </summary>
    public virtual void Visit(ISecurityAttribute securityAttribute) {
    }

    /// <summary>
    /// Performs some computation with the given specialized event definition.
    /// </summary>
    public virtual void Visit(ISpecializedEventDefinition specializedEventDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given specialized field definition.
    /// </summary>
    public virtual void Visit(ISpecializedFieldDefinition specializedFieldDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given specialized field reference.
    /// </summary>
    public virtual void Visit(ISpecializedFieldReference specializedFieldReference) {
    }

    /// <summary>
    /// Performs some computation with the given specialized method definition.
    /// </summary>
    public virtual void Visit(ISpecializedMethodDefinition specializedMethodDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given specialized method reference.
    /// </summary>
    public virtual void Visit(ISpecializedMethodReference specializedMethodReference) {
    }

    /// <summary>
    /// Performs some computation with the given specialized propperty definition.
    /// </summary>
    public virtual void Visit(ISpecializedPropertyDefinition specializedPropertyDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type definition.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeDefinition specializedNestedTypeDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type reference.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeReference specializedNestedTypeReference) {
    }

    /// <summary>
    /// Visits the specified type member.
    /// </summary>
    public virtual void Visit(ITypeDefinitionMember typeMember) {
      //ITypeDefinitionMember is a base interface that should never be implemented directly.
      //Get typeMember to call the most type specific visitor.
      typeMember.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified type reference.
    /// </summary>
    public virtual void Visit(ITypeReference typeReference) {
      //ITypeReference is a base interface that should never be implemented directly.
      //Get typeReference to call the most type specific visitor.
      typeReference.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified unit.
    /// </summary>
    public virtual void Visit(IUnit unit) {
      //IUnit is a base interface that should never be implemented directly.
      //Get unit to call the most type specific visitor.
      unit.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified unit reference.
    /// </summary>
    public virtual void Visit(IUnitReference unitReference) {
      //IUnitReference is a base interface that should never be implemented directly.
      //Get unitReference to call the most type specific visitor.
      unitReference.Dispatch(this);
    }

    /// <summary>
    /// Visits the specified unit namespace reference.
    /// </summary>
    public virtual void Visit(IUnitNamespaceReference unitNamespaceReference) {
      //IUnitNamespaceReference is a base interface that should never be implemented directly.
      //Get unitNamespaceReference to call the most type specific visitor.
      unitNamespaceReference.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given unit set.
    /// </summary>
    public virtual void Visit(IUnitSet unitSet) {
    }

    /// <summary>
    /// Visits the specified unit set namespace.
    /// </summary>
    public virtual void Visit(IUnitSetNamespace unitSetNamespace) {
      //IUnitSetNamespace is a base interface that should never be implemented directly.
      //Get unitSetNamespace to call the most type specific visitor.
      unitSetNamespace.Dispatch(this);
    }

    /// <summary>
    /// Performs some computation with the given Win32 resource.
    /// </summary>
    public virtual void Visit(IWin32Resource win32Resource) {
    }

    #endregion


  }

  /// <summary>
  /// A visitor base class that provides a dummy body for each method of IMetadataVisitor.
  /// </summary>
  public class MetadataVisitor : IMetadataVisitor {

    /// <summary>
    /// A visitor base class that provides a dummy body for each method of IMetadataVisitor.
    /// </summary>
    public MetadataVisitor() {
    }

    #region IMetadataVisitor Members

    /// <summary>
    /// Visits the specified alias for type.
    /// </summary>
    public virtual void Visit(IAliasForType aliasForType) {
    }

    /// <summary>
    /// Performs some computation with the given array type reference.
    /// </summary>
    public virtual void Visit(IArrayTypeReference arrayTypeReference) {
      this.Visit((ITypeReference)arrayTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given assembly.
    /// </summary>
    public virtual void Visit(IAssembly assembly) {
      this.Visit((IModule)assembly);
    }

    /// <summary>
    /// Performs some computation with the given assembly reference.
    /// </summary>
    public virtual void Visit(IAssemblyReference assemblyReference) {
      this.Visit((IModuleReference)assemblyReference);
    }

    /// <summary>
    /// Performs some computation with the given custom attribute.
    /// </summary>
    public virtual void Visit(ICustomAttribute customAttribute) {
    }

    /// <summary>
    /// Performs some computation with the given custom modifier.
    /// </summary>
    public virtual void Visit(ICustomModifier customModifier) {
    }

    /// <summary>
    /// Performs some computation with the given event definition.
    /// </summary>
    public virtual void Visit(IEventDefinition eventDefinition) {
      this.Visit((ITypeDefinitionMember)eventDefinition);
    }

    /// <summary>
    /// Performs some computation with the given field definition.
    /// </summary>
    public virtual void Visit(IFieldDefinition fieldDefinition) {
      this.Visit((ITypeDefinitionMember)fieldDefinition);
    }

    /// <summary>
    /// Performs some computation with the given field reference.
    /// </summary>
    public virtual void Visit(IFieldReference fieldReference) {
      this.Visit((ITypeMemberReference)fieldReference);
    }

    /// <summary>
    /// Performs some computation with the given file reference.
    /// </summary>
    public virtual void Visit(IFileReference fileReference) {
    }

    /// <summary>
    /// Performs some computation with the given function pointer type reference.
    /// </summary>
    public virtual void Visit(IFunctionPointerTypeReference functionPointerTypeReference) {
      this.Visit((ITypeReference)functionPointerTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given generic method instance reference.
    /// </summary>
    public virtual void Visit(IGenericMethodInstanceReference genericMethodInstanceReference) {
      this.Visit((IMethodReference)genericMethodInstanceReference);
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter.
    /// </summary>
    public virtual void Visit(IGenericMethodParameter genericMethodParameter) {
      this.Visit((IGenericParameter)genericMethodParameter);
    }

    /// <summary>
    /// Performs some computation with the given generic method parameter reference.
    /// </summary>
    public virtual void Visit(IGenericMethodParameterReference genericMethodParameterReference) {
      this.Visit((IGenericParameterReference)genericMethodParameterReference);
    }

    /// <summary>
    /// Performs some computation with the given generic parameter.
    /// </summary>
    public virtual void Visit(IGenericParameter genericParameter) {
      this.Visit((INamedTypeDefinition)genericParameter);
    }

    /// <summary>
    /// Performs some computation with the given generic parameter.
    /// </summary>
    public virtual void Visit(IGenericParameterReference genericParameterReference) {
      this.Visit((ITypeReference)genericParameterReference);
    }

    /// <summary>
    /// Performs some computation with the given generic type instance reference.
    /// </summary>
    public virtual void Visit(IGenericTypeInstanceReference genericTypeInstanceReference) {
      this.Visit((ITypeReference)genericTypeInstanceReference);
    }

    /// <summary>
    /// Performs some computation with the given generic parameter.
    /// </summary>
    public virtual void Visit(IGenericTypeParameter genericTypeParameter) {
      this.Visit((IGenericParameter)genericTypeParameter);
    }

    /// <summary>
    /// Performs some computation with the given generic type parameter reference.
    /// </summary>
    public virtual void Visit(IGenericTypeParameterReference genericTypeParameterReference) {
      this.Visit((IGenericParameterReference)genericTypeParameterReference);
    }

    /// <summary>
    /// Performs some computation with the given global field definition.
    /// </summary>
    public virtual void Visit(IGlobalFieldDefinition globalFieldDefinition) {
      this.Visit((IFieldDefinition)globalFieldDefinition);
    }

    /// <summary>
    /// Performs some computation with the given global method definition.
    /// </summary>
    public virtual void Visit(IGlobalMethodDefinition globalMethodDefinition) {
      this.Visit((IMethodDefinition)globalMethodDefinition);
    }

    /// <summary>
    /// Performs some computation with the given local definition.
    /// </summary>
    public virtual void Visit(ILocalDefinition localDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given local definition.
    /// </summary>
    public virtual void VisitReference(ILocalDefinition localDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given managed pointer type reference.
    /// </summary>
    public virtual void Visit(IManagedPointerTypeReference managedPointerTypeReference) {
      this.Visit((ITypeReference)managedPointerTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given marshalling information.
    /// </summary>
    public virtual void Visit(IMarshallingInformation marshallingInformation) {
    }

    /// <summary>
    /// Performs some computation with the given metadata constant.
    /// </summary>
    public virtual void Visit(IMetadataConstant constant) {
      this.Visit((IMetadataExpression)constant);
    }

    /// <summary>
    /// Performs some computation with the given metadata array creation expression.
    /// </summary>
    public virtual void Visit(IMetadataCreateArray createArray) {
      this.Visit((IMetadataExpression)createArray);
    }

    /// <summary>
    /// Performs some computation with the given metadata expression.
    /// </summary>
    public virtual void Visit(IMetadataExpression expression) {
    }

    /// <summary>
    /// Performs some computation with the given metadata named argument expression.
    /// </summary>
    public virtual void Visit(IMetadataNamedArgument namedArgument) {
      this.Visit((IMetadataExpression)namedArgument);
    }

    /// <summary>
    /// Performs some computation with the given metadata typeof expression.
    /// </summary>
    public virtual void Visit(IMetadataTypeOf typeOf) {
      this.Visit((IMetadataExpression)typeOf);
    }

    /// <summary>
    /// Performs some computation with the given method body.
    /// </summary>
    public virtual void Visit(IMethodBody methodBody) {
    }

    /// <summary>
    /// Performs some computation with the given method definition.
    /// </summary>
    public virtual void Visit(IMethodDefinition method) {
      this.Visit((ITypeDefinitionMember)method);
    }

    /// <summary>
    /// Performs some computation with the given method implementation.
    /// </summary>
    public virtual void Visit(IMethodImplementation methodImplementation) {
    }

    /// <summary>
    /// Performs some computation with the given method reference.
    /// </summary>
    public virtual void Visit(IMethodReference methodReference) {
    }

    /// <summary>
    /// Performs some computation with the given modified type reference.
    /// </summary>
    public virtual void Visit(IModifiedTypeReference modifiedTypeReference) {
      this.Visit((ITypeReference)modifiedTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given module.
    /// </summary>
    public virtual void Visit(IModule module) {
      this.Visit((IUnit)module);
    }

    /// <summary>
    /// Performs some computation with the given module reference.
    /// </summary>
    public virtual void Visit(IModuleReference moduleReference) {
      this.Visit((IUnitReference)moduleReference);
    }

    /// <summary>
    /// Performs some computation with the given named type definition.
    /// </summary>
    public virtual void Visit(INamedTypeDefinition namedTypeDefinition) {
      this.Visit((ITypeDefinition)namedTypeDefinition);
    }

    /// <summary>
    /// Performs some computation with the given named type reference.
    /// </summary>
    public virtual void Visit(INamedTypeReference namedTypeReference) {
      this.Visit((ITypeReference)namedTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given alias for a namespace type definition.
    /// </summary>
    public virtual void Visit(INamespaceAliasForType namespaceAliasForType) {
      this.Visit((IAliasForType)namespaceAliasForType);
    }

    /// <summary>
    /// Visits the specified namespace definition.
    /// </summary>
    public virtual void Visit(INamespaceDefinition namespaceDefinition) {
    }

    /// <summary>
    /// Visits the specified namespace member.
    /// </summary>
    public virtual void Visit(INamespaceMember namespaceMember) {
    }

    /// <summary>
    /// Performs some computation with the given namespace type definition.
    /// </summary>
    public virtual void Visit(INamespaceTypeDefinition namespaceTypeDefinition) {
      this.Visit((INamedTypeDefinition)namespaceTypeDefinition);
    }

    /// <summary>
    /// Performs some computation with the given namespace type reference.
    /// </summary>
    public virtual void Visit(INamespaceTypeReference namespaceTypeReference) {
      this.Visit((INamedTypeReference)namespaceTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given alias to a nested type definition.
    /// </summary>
    public virtual void Visit(INestedAliasForType nestedAliasForType) {
      this.Visit((IAliasForType)nestedAliasForType);
    }

    /// <summary>
    /// Performs some computation with the given nested type definition.
    /// </summary>
    public virtual void Visit(INestedTypeDefinition nestedTypeDefinition) {
      this.Visit((INamedTypeDefinition)nestedTypeDefinition);
    }

    /// <summary>
    /// Performs some computation with the given nested type reference.
    /// </summary>
    public virtual void Visit(INestedTypeReference nestedTypeReference) {
      this.Visit((INamedTypeReference)nestedTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace.
    /// </summary>
    public virtual void Visit(INestedUnitNamespace nestedUnitNamespace) {
      this.Visit((IUnitNamespace)nestedUnitNamespace);
    }

    /// <summary>
    /// Performs some computation with the given nested unit namespace reference.
    /// </summary>
    public virtual void Visit(INestedUnitNamespaceReference nestedUnitNamespaceReference) {
      this.Visit((IUnitNamespaceReference)nestedUnitNamespaceReference);
    }

    /// <summary>
    /// Performs some computation with the given nested unit set namespace.
    /// </summary>
    public virtual void Visit(INestedUnitSetNamespace nestedUnitSetNamespace) {
      this.Visit((IUnitSetNamespace)nestedUnitSetNamespace);
    }

    /// <summary>
    /// Performs some computation with the given IL operation.
    /// </summary>
    public virtual void Visit(IOperation operation) {
    }

    /// <summary>
    /// Performs some computation with the given IL operation exception information instance.
    /// </summary>
    public virtual void Visit(IOperationExceptionInformation operationExceptionInformation) {
    }

    /// <summary>
    /// Performs some computation with the given parameter definition.
    /// </summary>
    public virtual void Visit(IParameterDefinition parameterDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given parameter definition.
    /// </summary>
    public virtual void VisitReference(IParameterDefinition parameterDefinition) {
    }

    /// <summary>
    /// Performs some computation with the given property definition.
    /// </summary>
    public virtual void Visit(IPropertyDefinition propertyDefinition) {
      this.Visit((ITypeDefinitionMember)propertyDefinition);
    }

    /// <summary>
    /// Performs some computation with the given parameter type information.
    /// </summary>
    public virtual void Visit(IParameterTypeInformation parameterTypeInformation) {
    }

    /// <summary>
    /// Performs some compuation with the given platoform invoke information.
    /// </summary>
    public virtual void Visit(IPlatformInvokeInformation platformInvokeInformation) {
    }

    /// <summary>
    /// Performs some computation with the given pointer type reference.
    /// </summary>
    public virtual void Visit(IPointerTypeReference pointerTypeReference) {
      this.Visit((ITypeReference)pointerTypeReference);
    }

    /// <summary>
    /// Performs some computation with the given reference to a manifest resource.
    /// </summary>
    public virtual void Visit(IResourceReference resourceReference) {
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace.
    /// </summary>
    public virtual void Visit(IRootUnitNamespace rootUnitNamespace) {
      this.Visit((IUnitNamespace)rootUnitNamespace);
    }

    /// <summary>
    /// Performs some computation with the given root unit namespace reference.
    /// </summary>
    public virtual void Visit(IRootUnitNamespaceReference rootUnitNamespaceReference) {
      this.Visit((IUnitNamespaceReference)rootUnitNamespaceReference);
    }

    /// <summary>
    /// Performs some computation with the given root unit set namespace.
    /// </summary>
    public virtual void Visit(IRootUnitSetNamespace rootUnitSetNamespace) {
      this.Visit((IUnitSetNamespace)rootUnitSetNamespace);
    }

    /// <summary>
    /// Performs some computation with the given security attribute.
    /// </summary>
    public virtual void Visit(ISecurityAttribute securityAttribute) {
    }

    /// <summary>
    /// Performs some computation with the given specialized event definition.
    /// </summary>
    public virtual void Visit(ISpecializedEventDefinition specializedEventDefinition) {
      this.Visit((IEventDefinition)specializedEventDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized field definition.
    /// </summary>
    public virtual void Visit(ISpecializedFieldDefinition specializedFieldDefinition) {
      this.Visit((IFieldDefinition)specializedFieldDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized field reference.
    /// </summary>
    public virtual void Visit(ISpecializedFieldReference specializedFieldReference) {
      this.Visit((IFieldReference)specializedFieldReference);
    }

    /// <summary>
    /// Performs some computation with the given specialized method definition.
    /// </summary>
    public virtual void Visit(ISpecializedMethodDefinition specializedMethodDefinition) {
      this.Visit((IMethodDefinition)specializedMethodDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized method reference.
    /// </summary>
    public virtual void Visit(ISpecializedMethodReference specializedMethodReference) {
      this.Visit((IMethodReference)specializedMethodReference);
    }

    /// <summary>
    /// Performs some computation with the given specialized propperty definition.
    /// </summary>
    public virtual void Visit(ISpecializedPropertyDefinition specializedPropertyDefinition) {
      this.Visit((IPropertyDefinition)specializedPropertyDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type definition.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeDefinition specializedNestedTypeDefinition) {
      this.Visit((INestedTypeDefinition)specializedNestedTypeDefinition);
    }

    /// <summary>
    /// Performs some computation with the given specialized nested type reference.
    /// </summary>
    public virtual void Visit(ISpecializedNestedTypeReference specializedNestedTypeReference) {
      this.Visit((INestedTypeReference)specializedNestedTypeReference);
    }

    /// <summary>
    /// Visits the specified type definition.
    /// </summary>
    public virtual void Visit(ITypeDefinition typeDefinition) {
    }

    /// <summary>
    /// Visits the specified type member.
    /// </summary>
    public virtual void Visit(ITypeDefinitionMember typeMember) {
    }

    /// <summary>
    /// Visits the specified type member reference.
    /// </summary>
    public virtual void Visit(ITypeMemberReference typeMember) {
    }

    /// <summary>
    /// Visits the specified type reference.
    /// </summary>
    public virtual void Visit(ITypeReference typeReference) {
    }

    /// <summary>
    /// Visits the specified unit.
    /// </summary>
    public virtual void Visit(IUnit unit) {
    }

    /// <summary>
    /// Visits the specified unit reference.
    /// </summary>
    public virtual void Visit(IUnitReference unitReference) {
    }

    /// <summary>
    /// Visits the specified unit namespace.
    /// </summary>
    public virtual void Visit(IUnitNamespace unitNamespace) {
      this.Visit((INamespaceDefinition)unitNamespace);
    }

    /// <summary>
    /// Visits the specified unit namespace reference.
    /// </summary>
    public virtual void Visit(IUnitNamespaceReference unitNamespaceReference) {
    }

    /// <summary>
    /// Performs some computation with the given unit set.
    /// </summary>
    public virtual void Visit(IUnitSet unitSet) {
    }

    /// <summary>
    /// Visits the specified unit set namespace.
    /// </summary>
    public virtual void Visit(IUnitSetNamespace unitSetNamespace) {
    }

    /// <summary>
    /// Performs some computation with the given Win32 resource.
    /// </summary>
    public virtual void Visit(IWin32Resource win32Resource) {
    }

    #endregion


  }

  /// <summary>
  /// A class that traverses the metadata model in depth first, left to right order,
  /// calling visitors on each model instance in pre-order as well as post-order.
  /// </summary>
  public class MetadataTraverser {

    /// <summary>
    /// A class that traverses the metadata model in depth first, left to right order,
    /// calling visitors on each model instance in pre-order as well as post-order.
    /// </summary>
    public MetadataTraverser() {
      this.dispatchingVisitor = new Dispatcher() { traverser = this };
    }

    Dispatcher dispatchingVisitor;
    class Dispatcher : IMetadataVisitor {
      internal MetadataTraverser traverser;

      public void Visit(IArrayTypeReference arrayTypeReference) {
        this.traverser.Traverse(arrayTypeReference);
      }

      public void Visit(IAssembly assembly) {
        this.traverser.Traverse(assembly);
      }

      public void Visit(IAssemblyReference assemblyReference) {
        this.traverser.Traverse(assemblyReference);
      }

      public void Visit(IEventDefinition eventDefinition) {
        this.traverser.Traverse(eventDefinition);
      }

      public void Visit(IFieldDefinition fieldDefinition) {
        this.traverser.Traverse(fieldDefinition);
      }

      public void Visit(IFieldReference fieldReference) {
        this.traverser.TraverseUnspecialized(fieldReference);
      }

      public void Visit(IFunctionPointerTypeReference functionPointerTypeReference) {
        this.traverser.Traverse(functionPointerTypeReference);
      }

      public void Visit(IGenericMethodInstanceReference genericMethodInstanceReference) {
        this.traverser.Traverse(genericMethodInstanceReference);
      }

      public void Visit(IGenericMethodParameter genericMethodParameter) {
        this.traverser.Traverse(genericMethodParameter);
      }

      public void Visit(IGenericMethodParameterReference genericMethodParameterReference) {
        this.traverser.Traverse(genericMethodParameterReference);
      }

      public void Visit(IGenericTypeInstanceReference genericTypeInstanceReference) {
        this.traverser.Traverse(genericTypeInstanceReference);
      }

      public void Visit(IGenericTypeParameter genericTypeParameter) {
        this.traverser.Traverse(genericTypeParameter);
      }

      public void Visit(IGenericTypeParameterReference genericTypeParameterReference) {
        this.traverser.Traverse(genericTypeParameterReference);
      }

      public void Visit(IGlobalFieldDefinition globalFieldDefinition) {
        this.traverser.Traverse(globalFieldDefinition);
      }

      public void Visit(IGlobalMethodDefinition globalMethodDefinition) {
        this.traverser.Traverse(globalMethodDefinition);
      }

      public void Visit(IManagedPointerTypeReference managedPointerTypeReference) {
        this.traverser.Traverse(managedPointerTypeReference);
      }

      public void Visit(IMetadataConstant constant) {
        this.traverser.Traverse(constant);
      }

      public void Visit(IMetadataCreateArray createArray) {
        this.traverser.Traverse(createArray);
      }

      public void Visit(IMetadataNamedArgument namedArgument) {
        this.traverser.Traverse(namedArgument);
      }

      public void Visit(IMetadataTypeOf typeOf) {
        this.traverser.Traverse(typeOf);
      }

      public void Visit(IMethodDefinition method) {
        this.traverser.Traverse(method);
      }

      public void Visit(IMethodReference methodReference) {
        this.traverser.TraverseUnspecialized(methodReference);
      }

      public void Visit(IModifiedTypeReference modifiedTypeReference) {
        this.traverser.Traverse(modifiedTypeReference);
      }

      public void Visit(IModule module) {
        this.traverser.Traverse(module);
      }

      public void Visit(IModuleReference moduleReference) {
        this.traverser.Traverse(moduleReference);
      }

      public void Visit(INamespaceAliasForType namespaceAliasForType) {
        this.traverser.Traverse(namespaceAliasForType);
      }

      public void Visit(INamespaceTypeDefinition namespaceTypeDefinition) {
        this.traverser.Traverse(namespaceTypeDefinition);
      }

      public void Visit(INamespaceTypeReference namespaceTypeReference) {
        this.traverser.Traverse(namespaceTypeReference);
      }

      public void Visit(INestedAliasForType nestedAliasForType) {
        this.traverser.Traverse(nestedAliasForType);
      }

      public void Visit(INestedTypeDefinition nestedTypeDefinition) {
        this.traverser.Traverse(nestedTypeDefinition);
      }

      public void Visit(INestedTypeReference nestedTypeReference) {
        this.traverser.TraverseUnspecialized(nestedTypeReference);
      }

      public void Visit(INestedUnitNamespace nestedUnitNamespace) {
        this.traverser.Traverse(nestedUnitNamespace);
      }

      public void Visit(INestedUnitNamespaceReference nestedUnitNamespaceReference) {
        this.traverser.Traverse(nestedUnitNamespaceReference);
      }

      public void Visit(INestedUnitSetNamespace nestedUnitSetNamespace) {
        this.traverser.Traverse(nestedUnitSetNamespace);
      }

      public void Visit(IParameterDefinition parameterDefinition) {
        this.traverser.Traverse(parameterDefinition);
      }

      public void Visit(IPointerTypeReference pointerTypeReference) {
        this.traverser.Traverse(pointerTypeReference);
      }

      public void Visit(IPropertyDefinition propertyDefinition) {
        this.traverser.Traverse(propertyDefinition);
      }

      public void Visit(IRootUnitNamespace rootUnitNamespace) {
        this.traverser.Traverse(rootUnitNamespace);
      }

      public void Visit(IRootUnitNamespaceReference rootUnitNamespaceReference) {
        this.traverser.Traverse(rootUnitNamespaceReference);
      }

      public void Visit(IRootUnitSetNamespace rootUnitSetNamespace) {
        this.traverser.Traverse(rootUnitSetNamespace);
      }

      public void Visit(ISpecializedEventDefinition specializedEventDefinition) {
        //we are traversing a generic type instance.
        this.traverser.Traverse((IEventDefinition)specializedEventDefinition);
      }

      public void Visit(ISpecializedFieldDefinition specializedFieldDefinition) {
        //references are dispatched in such a way that they never get here.
        //thus, assume that we are traversing a generic type instance.
        this.traverser.Traverse((IFieldDefinition)specializedFieldDefinition);
      }

      public void Visit(ISpecializedFieldReference specializedFieldReference) {
        this.traverser.Traverse(specializedFieldReference);
      }

      public void Visit(ISpecializedMethodDefinition specializedMethodDefinition) {
        //references are dispatched in such a way that they never get here.
        //thus, assume that we are traversing a generic type instance.
        this.traverser.Traverse((IMethodDefinition)specializedMethodDefinition);
      }

      public void Visit(ISpecializedMethodReference specializedMethodReference) {
        this.traverser.Traverse(specializedMethodReference);
      }

      public void Visit(ISpecializedPropertyDefinition specializedPropertyDefinition) {
        //we are traversing a generic type instance.
        this.traverser.Traverse((IPropertyDefinition)specializedPropertyDefinition);
      }

      public void Visit(ISpecializedNestedTypeDefinition specializedNestedTypeDefinition) {
        //references are dispatched in such a way that they never get here.
        //thus, assume that we are traversing a generic type instance.
        this.traverser.Traverse((INestedTypeDefinition)specializedNestedTypeDefinition);
      }

      public void Visit(ISpecializedNestedTypeReference specializedNestedTypeReference) {
        this.traverser.Traverse(specializedNestedTypeReference);
      }


      #region IMetadataVisitor Members

      public void Visit(ICustomAttribute customAttribute) {
        Contract.Assume(false);
      }

      public void Visit(ICustomModifier customModifier) {
        Contract.Assume(false);
      }

      public void Visit(IFileReference fileReference) {
        Contract.Assume(false);
      }

      public void Visit(ILocalDefinition localDefinition) {
        Contract.Assume(false);
      }

      public void VisitReference(ILocalDefinition localDefinition) {
        Contract.Assume(false);
      }

      public void Visit(IMarshallingInformation marshallingInformation) {
        Contract.Assume(false);
      }

      public void Visit(IMetadataExpression expression) {
        Contract.Assume(false);
      }

      public void Visit(IMethodBody methodBody) {
        Contract.Assume(false);
      }

      public void Visit(IMethodImplementation methodImplementation) {
        Contract.Assume(false);
      }

      public void Visit(IOperation operation) {
        Contract.Assume(false);
      }

      public void Visit(IOperationExceptionInformation operationExceptionInformation) {
        Contract.Assume(false);
      }

      public void VisitReference(IParameterDefinition parameterDefinition) {
        Contract.Assume(false);
      }

      public void Visit(IParameterTypeInformation parameterTypeInformation) {
        Contract.Assume(false);
      }

      public void Visit(IPlatformInvokeInformation platformInvokeInformation) {
        Contract.Assume(false);
      }

      public void Visit(IResourceReference resourceReference) {
        Contract.Assume(false);
      }

      public void Visit(ISecurityAttribute securityAttribute) {
        Contract.Assume(false);
      }

      public void Visit(IUnitSet unitSet) {
        Contract.Assume(false);
      }

      public void Visit(IWin32Resource win32Resource) {
        Contract.Assume(false);
      }

      #endregion
    }

    /// <summary>
    /// A table in which we record the traversal of objects that can be reached several times (because they are references or can be referred to)
    /// so that we can avoid traversing them more than once.
    /// </summary>
    protected SetOfObjects objectsThatHaveAlreadyBeenTraversed = new SetOfObjects(1024*4);

    IMetadataVisitor/*?*/ preorderVisitor;
    IMetadataVisitor/*?*/ postorderVisitor;
    bool stopTraversal;
    bool traverseIntoMethodBodies;

    /// <summary>
    /// A visitor that should be called on each object being traversed, before any of its children are traversed. May be null.
    /// </summary>
    public IMetadataVisitor/*?*/ PreorderVisitor {
      get { return this.preorderVisitor; }
      set { this.preorderVisitor = value; }
    }

    /// <summary>
    /// A visitor that should be called on each object being traversed, after all of its children are traversed. May be null. 
    /// </summary>
    public IMetadataVisitor/*?*/ PostorderVisitor {
      get { return this.postorderVisitor; }
      set { this.postorderVisitor = value; }
    }

    /// <summary>
    /// If this is true, the traverser will stop the traversal. Typically this is set by a Visit and
    /// causes the traversal to terminate immediately after the Visit method returns.
    /// </summary>
    public bool StopTraversal {
      get { return this.stopTraversal; }
      set { this.stopTraversal = value; }
    }

    /// <summary>
    /// If this is true, the traversal descends into method bodies. If false, method bodies are ignored.
    /// </summary>
    public bool TraverseIntoMethodBodies {
      get { return this.traverseIntoMethodBodies; }
      set { this.traverseIntoMethodBodies = value; }
    }

    /// <summary>
    /// Traverses the alias for type.
    /// </summary>
    public void Traverse(IAliasForType aliasForType) {
      aliasForType.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the alias for type member.
    /// </summary>
    public void Traverse(IAliasMember aliasMember) {
      aliasMember.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the array type reference.
    /// </summary>
    public void Traverse(IArrayTypeReference arrayTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(arrayTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(arrayTypeReference); //No need to dispatch. This call is already type specific.
      if (this.stopTraversal) return;
      this.TraverseChildren(arrayTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(arrayTypeReference);
    }

    /// <summary>
    /// Traverses the assembly.
    /// </summary>
    public void Traverse(IAssembly assembly) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(assembly);
      if (this.stopTraversal) return;
      this.TraverseChildren(assembly);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(assembly);
    }


    /// <summary>
    /// Traverses the assembly reference.
    /// </summary>
    public void Traverse(IAssemblyReference assemblyReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(assemblyReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(assemblyReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(assemblyReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(assemblyReference);
    }

    /// <summary>
    /// Traverses the custom attribute.
    /// </summary>
    public void Traverse(ICustomAttribute customAttribute) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(customAttribute);
      if (this.stopTraversal) return;
      this.TraverseChildren(customAttribute);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(customAttribute);
    }

    /// <summary>
    /// Traverses the custom modifier.
    /// </summary>
    public void Traverse(ICustomModifier customModifier) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(customModifier);
      if (this.stopTraversal) return;
      this.TraverseChildren(customModifier);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(customModifier);
    }

    /// <summary>
    /// Traverses the definition.
    /// </summary>
    /// <param name="definition"></param>
    public void Traverse(IDefinition definition) {
      definition.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the event definition.
    /// </summary>
    public void Traverse(IEventDefinition eventDefinition) {
      //specialized events are simply traversed as if they were normal events
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(eventDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(eventDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(eventDefinition);
    }

    /// <summary>
    /// Traverses the field definition.
    /// </summary>
    public void Traverse(IFieldDefinition fieldDefinition) {
      //specialized fields are simply traversed as if they were normal fields
      if (this.preorderVisitor != null) fieldDefinition.Dispatch(this.preorderVisitor);
      if (this.stopTraversal) return;
      this.TraverseChildren(fieldDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) fieldDefinition.Dispatch(this.postorderVisitor);
    }

    /// <summary>
    /// Traverses the field reference.
    /// </summary>
    public void Traverse(IFieldReference fieldReference) {
      fieldReference.DispatchAsReference(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the unspecialized field reference.
    /// </summary>
    private void TraverseUnspecialized(IFieldReference fieldReference) {
      Contract.Requires(!(fieldReference is ISpecializedFieldReference));
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(fieldReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(fieldReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(fieldReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(fieldReference);
    }

    /// <summary>
    /// Traverses the file reference.
    /// </summary>
    public void Traverse(IFileReference fileReference) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(fileReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(fileReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(fileReference);
    }

    /// <summary>
    /// Traverses the function pointer type reference.
    /// </summary>
    public void Traverse(IFunctionPointerTypeReference functionPointerTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(functionPointerTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(functionPointerTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(functionPointerTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(functionPointerTypeReference);
    }

    /// <summary>
    /// Traverses the generic method instance reference.
    /// </summary>
    public void Traverse(IGenericMethodInstanceReference genericMethodInstanceReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(genericMethodInstanceReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericMethodInstanceReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericMethodInstanceReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericMethodInstanceReference);
    }

    /// <summary>
    /// Traverses the generic method parameter.
    /// </summary>
    public void Traverse(IGenericMethodParameter genericMethodParameter) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericMethodParameter);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericMethodParameter);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericMethodParameter);
    }

    /// <summary>
    /// Traverses the generic method parameter reference.
    /// </summary>
    public void Traverse(IGenericMethodParameterReference genericMethodParameterReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(genericMethodParameterReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericMethodParameterReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericMethodParameterReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericMethodParameterReference);
    }

    /// <summary>
    /// Traverses the generic type instance reference.
    /// </summary>
    public void Traverse(IGenericTypeInstanceReference genericTypeInstanceReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(genericTypeInstanceReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericTypeInstanceReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericTypeInstanceReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericTypeInstanceReference);
    }

    /// <summary>
    /// Traverses the generic type parameter.
    /// </summary>
    public void Traverse(IGenericTypeParameter genericTypeParameter) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericTypeParameter);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericTypeParameter);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericTypeParameter);
    }

    /// <summary>
    /// Traverses the generic type parameter reference.
    /// </summary>
    public void Traverse(IGenericTypeParameterReference genericTypeParameterReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(genericTypeParameterReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(genericTypeParameterReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(genericTypeParameterReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(genericTypeParameterReference);
    }

    /// <summary>
    /// Traverses the global field definition.
    /// </summary>
    public void Traverse(IGlobalFieldDefinition globalFieldDefinition) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(globalFieldDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(globalFieldDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(globalFieldDefinition);
    }

    /// <summary>
    /// Traverses the global method definition.
    /// </summary>
    public void Traverse(IGlobalMethodDefinition globalMethodDefinition) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(globalMethodDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(globalMethodDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(globalMethodDefinition);
    }

    /// <summary>
    /// Traverses the specified local definition.
    /// </summary>
    public void Traverse(ILocalDefinition localDefinition) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(localDefinition)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(localDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(localDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(localDefinition);
    }

    /// <summary>
    /// Traverses the managed pointer type reference.
    /// </summary>
    public void Traverse(IManagedPointerTypeReference managedPointerTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(managedPointerTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(managedPointerTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(managedPointerTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(managedPointerTypeReference);
    }

    /// <summary>
    /// Traverses the marshalling information.
    /// </summary>
    public void Traverse(IMarshallingInformation marshallingInformation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(marshallingInformation);
      if (this.stopTraversal) return;
      this.TraverseChildren(marshallingInformation);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(marshallingInformation);
    }

    /// <summary>
    /// Traverses the metadata constant.
    /// </summary>
    public void Traverse(IMetadataConstant constant) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(constant);
      if (this.stopTraversal) return;
      this.TraverseChildren(constant);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(constant);
    }

    /// <summary>
    /// Traverses the metadata array creation expression.
    /// </summary>
    public void Traverse(IMetadataCreateArray createArray) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(createArray);
      if (this.stopTraversal) return;
      this.TraverseChildren(createArray);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(createArray);
    }

    /// <summary>
    /// Traverses the metadata expression.
    /// </summary>
    public void Traverse(IMetadataExpression expression) {
      expression.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the metadata named argument expression.
    /// </summary>
    public void Traverse(IMetadataNamedArgument namedArgument) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(namedArgument);
      if (this.stopTraversal) return;
      this.TraverseChildren(namedArgument);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(namedArgument);
    }

    /// <summary>
    /// Traverses the metadata typeof expression.
    /// </summary>
    public void Traverse(IMetadataTypeOf typeOf) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(typeOf);
      if (this.stopTraversal) return;
      this.TraverseChildren(typeOf);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(typeOf);
    }

    /// <summary>
    /// Traverses the method body.
    /// </summary>
    public virtual void Traverse(IMethodBody methodBody) {
      if (!this.TraverseIntoMethodBodies) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(methodBody);
      if (this.stopTraversal) return;
      this.TraverseChildren(methodBody);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(methodBody);
    }

    /// <summary>
    /// Traverses the method definition.
    /// </summary>
    public void Traverse(IMethodDefinition method) {
      if (this.preorderVisitor != null) method.Dispatch(this.preorderVisitor);
      if (this.stopTraversal) return;
      this.TraverseChildren(method);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) method.Dispatch(this.postorderVisitor);
    }

    /// <summary>
    /// Traverses the method implementation.
    /// </summary>
    public void Traverse(IMethodImplementation methodImplementation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(methodImplementation);
      this.Traverse(methodImplementation.ImplementedMethod);
      if (this.stopTraversal) return;
      this.Traverse(methodImplementation.ImplementingMethod);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(methodImplementation);
    }

    /// <summary>
    /// Traverses the method reference.
    /// </summary>
    public void Traverse(IMethodReference methodReference) {
      methodReference.DispatchAsReference(this.dispatchingVisitor);
    }

    private void TraverseUnspecialized(IMethodReference methodReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(methodReference)) return;
      if (this.preorderVisitor != null) methodReference.DispatchAsReference(this.preorderVisitor);
      if (this.stopTraversal) return;
      this.TraverseChildren(methodReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) methodReference.DispatchAsReference(this.postorderVisitor);
    }

    /// <summary>
    /// Traverses the modified type reference.
    /// </summary>
    public void Traverse(IModifiedTypeReference modifiedTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(modifiedTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(modifiedTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(modifiedTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(modifiedTypeReference);
    }

    /// <summary>
    /// Traverses the module.
    /// </summary>
    public void Traverse(IModule module) {
      var assembly = module as IAssembly;
      if (assembly != null) {
        this.Traverse(assembly);
        return;
      }
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(module);
      if (this.stopTraversal) return;
      this.TraverseChildren(module);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(module);
    }

    /// <summary>
    /// Traverses the module reference.
    /// </summary>
    public void Traverse(IModuleReference moduleReference) {
      var assemblyReference = moduleReference as IAssemblyReference;
      if (assemblyReference != null) {
        this.Traverse(assemblyReference);
        return;
      }
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(moduleReference)) return;
      if (this.preorderVisitor != null) moduleReference.DispatchAsReference(this.preorderVisitor);
      if (this.stopTraversal) return;
      this.TraverseChildren(moduleReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) moduleReference.DispatchAsReference(this.postorderVisitor);
    }

    /// <summary>
    /// Traverses the alias for a namespace type definition.
    /// </summary>
    public void Traverse(INamespaceAliasForType namespaceAliasForType) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(namespaceAliasForType);
      if (this.stopTraversal) return;
      this.TraverseChildren(namespaceAliasForType);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(namespaceAliasForType);
    }

    /// <summary>
    /// Traverses the namespace definition.
    /// </summary>
    public void Traverse(INamespaceDefinition namespaceDefinition) {
      namespaceDefinition.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the namespace member.
    /// </summary>
    public void Traverse(INamespaceMember namespaceMember) {
      namespaceMember.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the namespace type definition.
    /// </summary>
    public void Traverse(INamespaceTypeDefinition namespaceTypeDefinition) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(namespaceTypeDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(namespaceTypeDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(namespaceTypeDefinition);
    }

    /// <summary>
    /// Traverses the namespace type reference.
    /// </summary>
    public void Traverse(INamespaceTypeReference namespaceTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(namespaceTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(namespaceTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(namespaceTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(namespaceTypeReference);
    }

    /// <summary>
    /// Traverses the nested alias for type.
    /// </summary>
    public void Traverse(INestedAliasForType nestedAliasForType) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedAliasForType);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedAliasForType);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedAliasForType);
    }

    /// <summary>
    /// Traverses the nested type definition.
    /// </summary>
    public void Traverse(INestedTypeDefinition nestedTypeDefinition) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedTypeDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedTypeDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedTypeDefinition);
    }

    /// <summary>
    /// Traverses the nested type reference.
    /// </summary>
    public void Traverse(INestedTypeReference nestedTypeReference) {
      nestedTypeReference.DispatchAsReference(this.dispatchingVisitor);
    }

    private void TraverseUnspecialized(INestedTypeReference nestedTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(nestedTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedTypeReference);
    }

    /// <summary>
    /// Traverses the specified nested unit namespace.
    /// </summary>
    public void Traverse(INestedUnitNamespace nestedUnitNamespace) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedUnitNamespace);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedUnitNamespace);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedUnitNamespace);
    }

    /// <summary>
    /// Traverses the specified nested unit namespace reference.
    /// </summary>
    public void Traverse(INestedUnitNamespaceReference nestedUnitNamespaceReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(nestedUnitNamespaceReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedUnitNamespaceReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedUnitNamespaceReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedUnitNamespaceReference);
    }

    /// <summary>
    /// Traverses the specified nested unit set namespace.
    /// </summary>
    public void Traverse(INestedUnitSetNamespace nestedUnitSetNamespace) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(nestedUnitSetNamespace);
      if (this.stopTraversal) return;
      this.TraverseChildren(nestedUnitSetNamespace);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(nestedUnitSetNamespace);
    }

    /// <summary>
    /// Traverses the specified operation.
    /// </summary>
    public void Traverse(IOperation operation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(operation);
      if (this.stopTraversal) return;
      this.TraverseChildren(operation);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(operation);
    }

    /// <summary>
    /// Traverses the specified operation exception information.
    /// </summary>
    public void Traverse(IOperationExceptionInformation operationExceptionInformation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(operationExceptionInformation);
      if (this.stopTraversal) return;
      this.TraverseChildren(operationExceptionInformation);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(operationExceptionInformation);
    }

    /// <summary>
    /// Traverses the parameter definition.
    /// </summary>
    public void Traverse(IParameterDefinition parameterDefinition) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(parameterDefinition)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(parameterDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(parameterDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(parameterDefinition);
    }

    /// <summary>
    /// Traverses the parameter type information.
    /// </summary>
    public void Traverse(IParameterTypeInformation parameterTypeInformation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(parameterTypeInformation);
      if (this.stopTraversal) return;
      this.TraverseChildren(parameterTypeInformation);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(parameterTypeInformation);
    }

    /// <summary>
    /// Traverses the specified platform invoke information.
    /// </summary>
    public void Traverse(IPlatformInvokeInformation platformInvokeInformation) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(platformInvokeInformation);
      if (this.stopTraversal) return;
      this.TraverseChildren(platformInvokeInformation);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(platformInvokeInformation);
    }

    /// <summary>
    /// Traverses the pointer type reference.
    /// </summary>
    public void Traverse(IPointerTypeReference pointerTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(pointerTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(pointerTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(pointerTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(pointerTypeReference);
    }

    /// <summary>
    /// Traverses the property definition.
    /// </summary>
    public void Traverse(IPropertyDefinition propertyDefinition) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(propertyDefinition)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(propertyDefinition);
      if (this.stopTraversal) return;
      this.TraverseChildren(propertyDefinition);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(propertyDefinition);
    }

    /// <summary>
    /// Traverses the reference to a manifest resource.
    /// </summary>
    public void Traverse(IResourceReference resourceReference) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(resourceReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(resourceReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(resourceReference);
    }

    /// <summary>
    /// Traverses the specified root unit namespace.
    /// </summary>
    public void Traverse(IRootUnitNamespace rootUnitNamespace) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(rootUnitNamespace);
      if (this.stopTraversal) return;
      this.TraverseChildren(rootUnitNamespace);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(rootUnitNamespace);
    }

    /// <summary>
    /// Traverses the specified root unit set namespace.
    /// </summary>
    public void Traverse(IRootUnitSetNamespace rootUnitSetNamespace) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(rootUnitSetNamespace);
      if (this.stopTraversal) return;
      this.TraverseChildren(rootUnitSetNamespace);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(rootUnitSetNamespace);
    }

    /// <summary>
    /// Traverses the specified root unit namespace reference.
    /// </summary>
    public void Traverse(IRootUnitNamespaceReference rootUnitNamespaceReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(rootUnitNamespaceReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(rootUnitNamespaceReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(rootUnitNamespaceReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(rootUnitNamespaceReference);
    }

    /// <summary>
    /// Traverses the security attribute.
    /// </summary>
    public void Traverse(ISecurityAttribute securityAttribute) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(securityAttribute);
      if (this.stopTraversal) return;
      this.TraverseChildren(securityAttribute);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(securityAttribute);
    }

    /// <summary>
    /// Traverses the specialized field reference.
    /// </summary>
    public void Traverse(ISpecializedFieldReference specializedFieldReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(specializedFieldReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(specializedFieldReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(specializedFieldReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(specializedFieldReference);
    }

    /// <summary>
    /// Traverses the specialized method reference.
    /// </summary>
    public void Traverse(ISpecializedMethodReference specializedMethodReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(specializedMethodReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(specializedMethodReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(specializedMethodReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(specializedMethodReference);
    }

    /// <summary>
    /// Traverses the specialized method reference.
    /// </summary>
    public void Traverse(ISpecializedNestedTypeReference specializedNestedTypeReference) {
      if (!this.objectsThatHaveAlreadyBeenTraversed.Add(specializedNestedTypeReference)) return;
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(specializedNestedTypeReference);
      if (this.stopTraversal) return;
      this.TraverseChildren(specializedNestedTypeReference);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(specializedNestedTypeReference);
    }

    /// <summary>
    /// Traverses the specified type definition.
    /// </summary>
    public void Traverse(ITypeDefinition typeDefinition) {
      typeDefinition.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the specified type member.
    /// </summary>
    public void Traverse(ITypeDefinitionMember typeMember) {
      typeMember.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the specified type reference.
    /// </summary>
    public void Traverse(ITypeReference typeReference) {
      typeReference.DispatchAsReference(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the specified unit namespace definition.
    /// </summary>
    public void Traverse(IUnitNamespace namespaceDefinition) {
      namespaceDefinition.Dispatch(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the specified unit namespace reference.
    /// </summary>
    public void Traverse(IUnitNamespaceReference unitNamespaceReference) {
      unitNamespaceReference.DispatchAsReference(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the specified unit reference.
    /// </summary>
    public void Traverse(IUnitReference unitReference) {
      unitReference.DispatchAsReference(this.dispatchingVisitor);
    }

    /// <summary>
    /// Traverses the Win32 resource.
    /// </summary>
    public void Traverse(IWin32Resource win32Resource) {
      if (this.preorderVisitor != null) this.preorderVisitor.Visit(win32Resource);
      if (this.stopTraversal) return;
      this.TraverseChildren(win32Resource);
      if (this.stopTraversal) return;
      if (this.postorderVisitor != null) this.postorderVisitor.Visit(win32Resource);
    }

    /// <summary>
    /// Traverses the enumeration of aliases for types.
    /// </summary>
    public void Traverse(IEnumerable<IAliasForType> aliasesForTypes) {
      foreach (IAliasForType aliasForType in aliasesForTypes) {
        this.Traverse(aliasForType);
        if (this.stopTraversal) return;
      }
    }

    /// <summary>
    /// Traverses the enumeration of aliases for types.
    /// </summary>
    public void Traverse(IEnumerable<IAliasMember> aliasesForTypes) {
      foreach (IAliasMember aliasMember in aliasesForTypes) {
        this.Traverse(aliasMember);
        if (this.stopTraversal) return;
      }
    }

    /// <summary>
    /// Traverses the specified assembly references.
    /// </summary>
    public void Traverse(IEnumerable<IAssemblyReference> assemblyReferences) {
      foreach (IAssemblyReference assemblyReference in assemblyReferences) {
        this.Traverse(assemblyReference);
        if (this.stopTraversal) return;
      }
    }

    /// <summary>
    /// Traverses the specified custom attributes.
    /// </summary>
    public void Traverse(IEnumerable<ICustomAttribute> customAttributes) {
      foreach (ICustomAttribute customAttribute in customAttributes) {
        this.Traverse(customAttribute);
        if (this.stopTraversal) return;
      }
    }

    /// <summary>
    /// Traverses the specified custom modifiers.
    /// </summary>
    public void Traverse(IEnumerable<ICustomModifier> customModifiers) {
      foreach (ICustomModifier customModifier in customModifiers) {
        this.Traverse(customModifier);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified events.
    /// </summary>
    public void Traverse(IEnumerable<IEventDefinition> events) {
      foreach (IEventDefinition eventDef in events) {
        this.Traverse(eventDef);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified fields.
    /// </summary>
    public void Traverse(IEnumerable<IFieldDefinition> fields) {
      foreach (IFieldDefinition field in fields) {
        this.Traverse(field);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified file references.
    /// </summary>
    public void Traverse(IEnumerable<IFileReference> fileReferences) {
      foreach (IFileReference fileReference in fileReferences) {
        this.Traverse(fileReference);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified generic parameters.
    /// </summary>
    public void Traverse(IEnumerable<IGenericMethodParameter> genericMethodParameters) {
      foreach (var genericMethodParameter in genericMethodParameters) {
        this.Traverse(genericMethodParameter);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified generic parameters.
    /// </summary>
    public void Traverse(IEnumerable<IGenericTypeParameter> genericTypeParameters) {
      foreach (var genericTypeParameter in genericTypeParameters) {
        this.Traverse(genericTypeParameter);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified local definitions.
    /// </summary>
    public void Traverse(IEnumerable<ILocalDefinition> localDefinitions) {
      foreach (ILocalDefinition localDefinition in localDefinitions) {
        this.Traverse(localDefinition);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified expressions.
    /// </summary>
    public void Traverse(IEnumerable<IMetadataExpression> expressions) {
      foreach (IMetadataExpression expression in expressions) {
        this.Traverse(expression);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified named arguments.
    /// </summary>
    public void Traverse(IEnumerable<IMetadataNamedArgument> namedArguments) {
      foreach (IMetadataNamedArgument namedArgument in namedArguments) {
        this.Traverse(namedArgument);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified methods.
    /// </summary>
    public void Traverse(IEnumerable<IMethodDefinition> methods) {
      foreach (IMethodDefinition method in methods) {
        this.Traverse(method);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified method implementations.
    /// </summary>
    public void Traverse(IEnumerable<IMethodImplementation> methodImplementations) {
      foreach (IMethodImplementation methodImplementation in methodImplementations) {
        this.Traverse(methodImplementation);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified method references.
    /// </summary>
    public void Traverse(IEnumerable<IMethodReference> methodReferences) {
      foreach (IMethodReference methodReference in methodReferences) {
        this.Traverse(methodReference);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified modules.
    /// </summary>
    public void Traverse(IEnumerable<IModule> modules) {
      foreach (IModule module in modules) {
        this.Traverse(module);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified module references.
    /// </summary>
    public void Traverse(IEnumerable<IModuleReference> moduleReferences) {
      foreach (IModuleReference moduleReference in moduleReferences) {
        this.Traverse(moduleReference);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified named types.
    /// </summary>
    public void Traverse(IEnumerable<INamedTypeDefinition> types) {
      foreach (INamedTypeDefinition type in types) {
        this.Traverse(type);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified namespace members.
    /// </summary>
    public void Traverse(IEnumerable<INamespaceMember> namespaceMembers) {
      foreach (INamespaceMember namespaceMember in namespaceMembers) {
        this.Traverse(namespaceMember);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified nested types.
    /// </summary>
    public void Traverse(IEnumerable<INestedTypeDefinition> nestedTypes) {
      foreach (INestedTypeDefinition nestedType in nestedTypes) {
        this.Traverse(nestedType);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified operations.
    /// </summary>
    public void Traverse(IEnumerable<IOperation> operations) {
      foreach (IOperation operation in operations) {
        this.Traverse(operation);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified operation exception informations.
    /// </summary>
    public void Traverse(IEnumerable<IOperationExceptionInformation> operationExceptionInformations) {
      foreach (IOperationExceptionInformation operationExceptionInformation in operationExceptionInformations) {
        this.Traverse(operationExceptionInformation);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified parameters.
    /// </summary>
    public void Traverse(IEnumerable<IParameterDefinition> parameters) {
      foreach (IParameterDefinition parameter in parameters) {
        this.Traverse(parameter);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified parameter type informations.
    /// </summary>
    public void Traverse(IEnumerable<IParameterTypeInformation> parameterTypeInformations) {
      foreach (IParameterTypeInformation parameterTypeInformation in parameterTypeInformations) {
        this.Traverse(parameterTypeInformation);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified properties.
    /// </summary>
    public void Traverse(IEnumerable<IPropertyDefinition> properties) {
      foreach (IPropertyDefinition property in properties) {
        this.Traverse(property);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified resource references.
    /// </summary>
    public void Traverse(IEnumerable<IResourceReference> resourceReferences) {
      foreach (IResourceReference resourceReference in resourceReferences) {
        this.Traverse(resourceReference);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified security attributes.
    /// </summary>
    public void Traverse(IEnumerable<ISecurityAttribute> securityAttributes) {
      foreach (ISecurityAttribute securityAttribute in securityAttributes) {
        this.Traverse(securityAttribute);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified type members.
    /// </summary>
    /// <remarks>Not used by the traverser itself.</remarks>
    public void Traverse(IEnumerable<ITypeDefinitionMember> typeMembers) {
      foreach (ITypeDefinitionMember typeMember in typeMembers) {
        this.Traverse(typeMember);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified type references.
    /// </summary>
    public void Traverse(IEnumerable<ITypeReference> typeReferences) {
      foreach (ITypeReference typeReference in typeReferences) {
        this.Traverse(typeReference);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the specified type references.
    /// </summary>
    public void Traverse(IEnumerable<IWin32Resource> win32Resources) {
      foreach (IWin32Resource win32Resource in win32Resources) {
        this.Traverse(win32Resource);
        if (this.stopTraversal) break;
      }
    }

    /// <summary>
    /// Traverses the children of the alias for type.
    /// </summary>
    public virtual void TraverseChildren(IAliasForType aliasForType) {
      if (this.stopTraversal) return;
      this.Traverse(aliasForType.AliasedType);
      if (this.stopTraversal) return;
      this.Traverse(aliasForType.Attributes);
      if (this.stopTraversal) return;
      this.Traverse(aliasForType.Members);
    }

    /// <summary>
    /// Traverses the children of the array type reference.
    /// </summary>
    public virtual void TraverseChildren(IArrayTypeReference arrayTypeReference) {
      this.TraverseChildren((ITypeReference)arrayTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(arrayTypeReference.ElementType);
    }

    /// <summary>
    /// Traverses the children of the array type reference.
    /// </summary>
    public virtual void TraverseChildren(IAssembly assembly) {
      this.Traverse(assembly.AssemblyAttributes);
      if (this.stopTraversal) return;
      this.Traverse(assembly.ExportedTypes);
      if (this.stopTraversal) return;
      this.Traverse(assembly.Files);
      if (this.stopTraversal) return;
      this.Traverse(assembly.MemberModules);
      if (this.stopTraversal) return;
      this.Traverse(assembly.Resources);
      if (this.stopTraversal) return;
      this.Traverse(assembly.SecurityAttributes);
      this.TraverseChildren((IModule)assembly);
    }

    /// <summary>
    /// Traverses the children of the assembly reference.
    /// </summary>
    public virtual void TraverseChildren(IAssemblyReference assemblyReference) {
      this.TraverseChildren((IModuleReference)assemblyReference);
    }

    /// <summary>
    /// Traverses the children of the custom attribute.
    /// </summary>
    public virtual void TraverseChildren(ICustomAttribute customAttribute) {
      this.Traverse(customAttribute.Arguments);
      if (this.stopTraversal) return;
      this.Traverse(customAttribute.Constructor);
      if (this.stopTraversal) return;
      this.Traverse(customAttribute.NamedArguments);
    }

    /// <summary>
    /// Traverses the children of the custom modifier.
    /// </summary>
    public virtual void TraverseChildren(ICustomModifier customModifier) {
      this.Traverse(customModifier.Modifier);
    }

    /// <summary>
    /// Traverses the children of the event definition.
    /// </summary>
    public virtual void TraverseChildren(IEventDefinition eventDefinition) {
      this.TraverseChildren((ITypeDefinitionMember)eventDefinition);
      if (this.stopTraversal) return;
      this.Traverse(eventDefinition.Accessors);
      if (this.stopTraversal) return;
      this.Traverse(eventDefinition.Adder);
      if (this.stopTraversal) return;
      if (eventDefinition.Caller != null) {
        this.Traverse(eventDefinition.Caller);
        if (this.stopTraversal) return;
      }
      this.Traverse(eventDefinition.Remover);
      if (this.stopTraversal) return;
      this.Traverse(eventDefinition.Type);
    }

    /// <summary>
    /// Traverses the children of the field definition.
    /// </summary>
    public virtual void TraverseChildren(IFieldDefinition fieldDefinition) {
      this.TraverseChildren((ITypeDefinitionMember)fieldDefinition);
      if (this.stopTraversal) return;
      if (fieldDefinition.IsCompileTimeConstant) {
        this.Traverse(fieldDefinition.CompileTimeValue);
        if (this.stopTraversal) return;
      }
      if (fieldDefinition.IsModified) {
        this.Traverse(fieldDefinition.CustomModifiers);
        if (this.stopTraversal) return;
      }
      if (fieldDefinition.IsMarshalledExplicitly) {
        this.Traverse(fieldDefinition.MarshallingInformation);
        if (this.stopTraversal) return;
      }
      this.Traverse(fieldDefinition.Type);
    }

    /// <summary>
    /// Traverses the children of the field reference.
    /// </summary>
    public virtual void TraverseChildren(IFieldReference fieldReference) {
      this.Traverse(fieldReference.ContainingType);
      if (this.stopTraversal) return;
      if (fieldReference.IsModified) {
        this.Traverse(fieldReference.CustomModifiers);
        if (this.stopTraversal) return;
      }
      this.Traverse(fieldReference.Type);
    }

    /// <summary>
    /// Traverses the children of the file reference.
    /// </summary>
    public virtual void TraverseChildren(IFileReference fileReference) {
      //no children
    }

    /// <summary>
    /// Traverses the children of the function pointer type reference.
    /// </summary>
    public virtual void TraverseChildren(IFunctionPointerTypeReference functionPointerTypeReference) {
      this.TraverseChildren((ITypeReference)functionPointerTypeReference);
      this.Traverse(functionPointerTypeReference.Type);
      if (this.stopTraversal) return;
      this.Traverse(functionPointerTypeReference.Parameters);
      if (this.stopTraversal) return;
      this.Traverse(functionPointerTypeReference.ExtraArgumentTypes);
      if (this.stopTraversal) return;
      if (functionPointerTypeReference.ReturnValueIsModified)
        this.Traverse(functionPointerTypeReference.ReturnValueCustomModifiers);
    }

    /// <summary>
    /// Traverses the children of the generic method instance reference.
    /// </summary>
    public virtual void TraverseChildren(IGenericMethodInstanceReference genericMethodInstanceReference) {
      this.TraverseChildren((IMethodReference)genericMethodInstanceReference);
      this.Traverse(genericMethodInstanceReference.Attributes);
      if (this.stopTraversal) return;
      this.Traverse(genericMethodInstanceReference.ContainingType);
      if (this.stopTraversal) return;
      this.Traverse(genericMethodInstanceReference.GenericArguments);
      if (this.stopTraversal) return;
      this.Traverse(genericMethodInstanceReference.GenericMethod); //cannot be generic
      if (this.stopTraversal) return;
      this.Traverse(genericMethodInstanceReference.Parameters);
      if (this.stopTraversal) return;
      this.Traverse(genericMethodInstanceReference.Type);
      if (this.stopTraversal) return;
      if (genericMethodInstanceReference.ReturnValueIsModified)
        this.Traverse(genericMethodInstanceReference.ReturnValueCustomModifiers);
    }

    /// <summary>
    /// Traverses the children of the generic method parameter.
    /// </summary>
    public virtual void TraverseChildren(IGenericMethodParameter genericMethodParameter) {
      this.TraverseChildren((IGenericParameter)genericMethodParameter);
    }

    /// <summary>
    /// Traverses the children of the generic method parameter reference.
    /// </summary>
    public virtual void TraverseChildren(IGenericMethodParameterReference genericMethodParameterReference) {
      this.TraverseChildren((IGenericParameterReference)genericMethodParameterReference);
      if (this.stopTraversal) return;
      this.Traverse(genericMethodParameterReference.DefiningMethod);
    }

    /// <summary>
    /// Traverses the children of the generic parameter reference.
    /// </summary>
    public virtual void TraverseChildren(IGenericParameterReference genericParameterReference) {
      this.TraverseChildren((ITypeReference)genericParameterReference);
    }

    /// <summary>
    /// Traverses the children of the generic parameter.
    /// </summary>
    public virtual void TraverseChildren(IGenericParameter genericParameter) {
      this.TraverseChildren((INamedTypeDefinition)genericParameter);
      if (this.stopTraversal) return;
      this.Traverse(genericParameter.Constraints);
    }

    /// <summary>
    /// Traverses the children of the generic type instance reference.
    /// </summary>
    public virtual void TraverseChildren(IGenericTypeInstanceReference genericTypeInstanceReference) {
      this.TraverseChildren((ITypeReference)genericTypeInstanceReference);
      if (this.stopTraversal) return;
      this.Traverse(genericTypeInstanceReference.GenericType);
      if (this.stopTraversal) return;
      this.Traverse(genericTypeInstanceReference.GenericArguments);
    }

    /// <summary>
    /// Traverses the children of the generic type parameter.
    /// </summary>
    public virtual void TraverseChildren(IGenericTypeParameter genericTypeParameter) {
      this.TraverseChildren((IGenericParameter)genericTypeParameter);
    }

    /// <summary>
    /// Traverses the children of the generic type parameter reference.
    /// </summary>
    public virtual void TraverseChildren(IGenericTypeParameterReference genericTypeParameterReference) {
      this.TraverseChildren((IGenericParameterReference)genericTypeParameterReference);
      if (this.stopTraversal) return;
      this.Traverse(genericTypeParameterReference.DefiningType);
    }

    /// <summary>
    /// Traverses the children of the global field definition.
    /// </summary>
    public virtual void TraverseChildren(IGlobalFieldDefinition globalFieldDefinition) {
      this.TraverseChildren((IFieldDefinition)globalFieldDefinition);
    }

    /// <summary>
    /// Traverses the children of the global method definition.
    /// </summary>
    public virtual void TraverseChildren(IGlobalMethodDefinition globalMethodDefinition) {
      this.TraverseChildren((IMethodDefinition)globalMethodDefinition);
    }

    /// <summary>
    /// Traverses the children of the specified local definition.
    /// </summary>
    public virtual void TraverseChildren(ILocalDefinition localDefinition) {
      if (localDefinition.IsConstant)
        this.Traverse(localDefinition.CompileTimeValue);
      if (localDefinition.IsModified)
        this.Traverse(localDefinition.CustomModifiers);
      if (this.stopTraversal) return;
      this.Traverse(localDefinition.Type);
    }

    /// <summary>
    /// Traverses the children of the managed pointer type reference.
    /// </summary>
    public virtual void TraverseChildren(IManagedPointerTypeReference managedPointerTypeReference) {
      this.TraverseChildren((ITypeReference)managedPointerTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(managedPointerTypeReference.TargetType);
    }

    /// <summary>
    /// Traverses the children of the marshalling information.
    /// </summary>
    public virtual void TraverseChildren(IMarshallingInformation marshallingInformation) {
      if (marshallingInformation.UnmanagedType == System.Runtime.InteropServices.UnmanagedType.CustomMarshaler) {
        this.Traverse(marshallingInformation.CustomMarshaller);
        if (this.stopTraversal) return;
      }
      if (marshallingInformation.UnmanagedType == System.Runtime.InteropServices.UnmanagedType.SafeArray && 
      (marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_DISPATCH ||
      marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_UNKNOWN ||
      marshallingInformation.SafeArrayElementSubtype == System.Runtime.InteropServices.VarEnum.VT_RECORD))
        this.Traverse(marshallingInformation.SafeArrayElementUserDefinedSubtype);
    }

    /// <summary>
    /// Traverses the children of the metadata constant.
    /// </summary>
    public virtual void TraverseChildren(IMetadataConstant constant) {
      this.TraverseChildren((IMetadataExpression)constant);
    }

    /// <summary>
    /// Traverses the children of the metadata array creation expression.
    /// </summary>
    public virtual void TraverseChildren(IMetadataCreateArray createArray) {
      this.TraverseChildren((IMetadataExpression)createArray);
      if (this.stopTraversal) return;
      this.Traverse(createArray.ElementType);
      if (this.stopTraversal) return;
      this.Traverse(createArray.Initializers);
    }

    /// <summary>
    /// Traverses the children of the metadata expression.
    /// </summary>
    public virtual void TraverseChildren(IMetadataExpression expression) {
      this.Traverse(expression.Type);
    }

    /// <summary>
    /// Traverses the children of the metadata named argument expression.
    /// </summary>
    public virtual void TraverseChildren(IMetadataNamedArgument namedArgument) {
      this.TraverseChildren((IMetadataExpression)namedArgument);
      if (this.stopTraversal) return;
      this.Traverse(namedArgument.ArgumentValue);
    }

    /// <summary>
    /// Traverses the children of the metadata typeof expression.
    /// </summary>
    public virtual void TraverseChildren(IMetadataTypeOf typeOf) {
      this.TraverseChildren((IMetadataExpression)typeOf);
      if (this.stopTraversal) return;
      this.Traverse(typeOf.TypeToGet);
    }

    /// <summary>
    /// Traverses the children of the method body.
    /// </summary>
    public virtual void TraverseChildren(IMethodBody methodBody) {
      this.Traverse(methodBody.LocalVariables);
      if (this.stopTraversal) return;
      this.Traverse(methodBody.Operations);
      if (this.stopTraversal) return;
      this.Traverse(methodBody.OperationExceptionInformation);
    }

    /// <summary>
    /// Traverses the children of the method definition.
    /// </summary>
    public virtual void TraverseChildren(IMethodDefinition method) {
      this.TraverseChildren((ITypeDefinitionMember)method);
      if (this.stopTraversal) return;
      this.Traverse(method.ReturnValueAttributes);
      if (this.stopTraversal) return;
      if (method.ReturnValueIsModified) {
        this.Traverse(method.ReturnValueCustomModifiers);
        if (this.stopTraversal) return;
      }
      if (method.HasDeclarativeSecurity) {
        this.Traverse(method.SecurityAttributes);
        if (this.stopTraversal) return;
      }
      this.Traverse(method.Type);
      if (this.stopTraversal) return;
      if (method.IsGeneric) {
        this.Traverse(method.GenericParameters);
        if (this.stopTraversal) return;
      }
      this.Traverse(method.Parameters);
      if (method.IsPlatformInvoke) {
        this.Traverse(method.PlatformInvokeData);
        if (this.stopTraversal) return;
      }
      if (!method.IsAbstract && !method.IsExternal)
        this.Traverse(method.Body);
    }

    /// <summary>
    /// Traverses the children of the method implementation.
    /// </summary>
    public virtual void TraverseChildren(IMethodImplementation methodImplementation) {
      this.Traverse(methodImplementation.ImplementedMethod);
      if (this.stopTraversal) return;
      this.Traverse(methodImplementation.ImplementingMethod);
    }

    /// <summary>
    /// Traverses the children of the method reference.
    /// </summary>
    public virtual void TraverseChildren(IMethodReference methodReference) {
      this.Traverse(methodReference.Attributes);
      if (this.stopTraversal) return;
      this.Traverse(methodReference.ContainingType);
      if (this.stopTraversal) return;
      this.Traverse(methodReference.Parameters);
      if (this.stopTraversal) return;
      if (methodReference.AcceptsExtraArguments) {
        this.Traverse(methodReference.ExtraParameters);
        if (this.stopTraversal) return;
      }
      this.Traverse(methodReference.Type);
      if (this.stopTraversal) return;
      if (methodReference.ReturnValueIsModified)
        this.Traverse(methodReference.ReturnValueCustomModifiers);
    }

    /// <summary>
    /// Traverses the children of the modified type reference.
    /// </summary>
    public virtual void TraverseChildren(IModifiedTypeReference modifiedTypeReference) {
      this.TraverseChildren((ITypeReference)modifiedTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(modifiedTypeReference.CustomModifiers);
      if (this.stopTraversal) return;
      this.Traverse(modifiedTypeReference.UnmodifiedType);
    }

    /// <summary>
    /// Traverses the children of the module.
    /// </summary>
    public virtual void TraverseChildren(IModule module) {
      this.Traverse(module.ModuleAttributes);
      if (this.stopTraversal) return;
      this.Traverse(module.AssemblyReferences);
      if (this.stopTraversal) return;
      this.Traverse(module.ModuleReferences);
      if (this.stopTraversal) return;
      this.Traverse(module.Win32Resources);
      if (this.stopTraversal) return;
      this.Traverse(module.UnitNamespaceRoot);
    }

    /// <summary>
    /// Traverses the children of the module reference.
    /// </summary>
    public virtual void TraverseChildren(IModuleReference moduleReference) {
      this.TraverseChildren((IUnitReference)moduleReference);
    }

    /// <summary>
    /// Traverses the children of the named type definition.
    /// </summary>
    public virtual void TraverseChildren(INamedTypeDefinition namedTypeDefinition) {
      this.TraverseChildren((ITypeDefinition)namedTypeDefinition);
    }

    /// <summary>
    /// Traverses the children of the namespace alias for type.
    /// </summary>
    public virtual void TraverseChildren(INamespaceAliasForType namespaceAliasForType) {
      this.TraverseChildren((IAliasForType)namespaceAliasForType);
    }

    /// <summary>
    /// Traverses the specified namespace definition.
    /// </summary>
    public virtual void TraverseChildren(INamespaceDefinition namespaceDefinition) {
      this.Traverse(namespaceDefinition.Members);
    }

    /// <summary>
    /// Traverses the children of the namespace type definition.
    /// </summary>
    public virtual void TraverseChildren(INamespaceTypeDefinition namespaceTypeDefinition) {
      this.TraverseChildren((INamedTypeDefinition)namespaceTypeDefinition);
    }

    /// <summary>
    /// Traverses the children of the namespace type reference.
    /// </summary>
    public virtual void TraverseChildren(INamespaceTypeReference namespaceTypeReference) {
      this.TraverseChildren((ITypeReference)namespaceTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(namespaceTypeReference.ContainingUnitNamespace);
    }

    /// <summary>
    /// Traverses the children of the nested alias for type.
    /// </summary>
    public virtual void TraverseChildren(INestedAliasForType nestedAliasForType) {
      this.TraverseChildren((IAliasForType)nestedAliasForType);
    }

    /// <summary>
    /// Traverses the children of the nested type definition.
    /// </summary>
    public virtual void TraverseChildren(INestedTypeDefinition nestedTypeDefinition) {
      this.TraverseChildren((INamedTypeDefinition)nestedTypeDefinition);
    }

    /// <summary>
    /// Traverses the children of the nested type reference.
    /// </summary>
    public virtual void TraverseChildren(INestedTypeReference nestedTypeReference) {
      this.TraverseChildren((ITypeReference)nestedTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(nestedTypeReference.ContainingType);
      if (this.stopTraversal) return;
    }

    /// <summary>
    /// Traverses the specified nested unit namespace.
    /// </summary>
    public virtual void TraverseChildren(INestedUnitNamespace nestedUnitNamespace) {
      this.TraverseChildren((IUnitNamespace)nestedUnitNamespace);
    }

    /// <summary>
    /// Traverses the specified nested unit namespace reference.
    /// </summary>
    public virtual void TraverseChildren(INestedUnitNamespaceReference nestedUnitNamespaceReference) {
      this.TraverseChildren((IUnitNamespaceReference)nestedUnitNamespaceReference);
    }

    /// <summary>
    /// Traverses the specified nested unit namespace.
    /// </summary>
    public virtual void TraverseChildren(INestedUnitSetNamespace nestedUnitSetNamespace) {
      this.TraverseChildren((IUnitSetNamespace)nestedUnitSetNamespace);
    }

    /// <summary>
    /// Traverses the specified operation.
    /// </summary>
    public virtual void TraverseChildren(IOperation operation) {
      ITypeReference/*?*/ typeReference = operation.Value as ITypeReference;
      if (typeReference != null)
        this.Traverse(typeReference);
      else {
        IFieldReference/*?*/ fieldReference = operation.Value as IFieldReference;
        if (fieldReference != null)
          this.Traverse(fieldReference);
        else {
          IMethodReference/*?*/ methodReference = operation.Value as IMethodReference;
          if (methodReference != null)
            this.Traverse(methodReference);
          else {
            var parameter = operation.Value as IParameterDefinition;
            if (parameter != null)
              this.Traverse(parameter);
            else {
              var local = operation.Value as ILocalDefinition;
              if (local != null)
                this.Traverse(local);
            }
          }
        }
      }
    }

    /// <summary>
    /// Traverses the specified operation exception information.
    /// </summary>
    public virtual void TraverseChildren(IOperationExceptionInformation operationExceptionInformation) {
      if (operationExceptionInformation.HandlerKind == HandlerKind.Catch || operationExceptionInformation.HandlerKind == HandlerKind.Filter)
        this.Traverse(operationExceptionInformation.ExceptionType);
    }

    /// <summary>
    /// Traverses the children of the parameter definition.
    /// </summary>
    public virtual void TraverseChildren(IParameterDefinition parameterDefinition) {
      this.Traverse(parameterDefinition.Attributes);
      if (this.stopTraversal) return;
      if (parameterDefinition.IsModified) {
        this.Traverse(parameterDefinition.CustomModifiers);
        if (this.stopTraversal) return;
      }
      if (parameterDefinition.HasDefaultValue) {
        this.Traverse(parameterDefinition.DefaultValue);
        if (this.stopTraversal) return;
      }
      if (parameterDefinition.IsMarshalledExplicitly) {
        this.Traverse(parameterDefinition.MarshallingInformation);
        if (this.stopTraversal) return;
      }
      this.Traverse(parameterDefinition.Type);
    }

    /// <summary>
    /// Traverses the children of the parameter type information.
    /// </summary>
    public virtual void TraverseChildren(IParameterTypeInformation parameterTypeInformation) {
      if (parameterTypeInformation.IsModified) {
        this.Traverse(parameterTypeInformation.CustomModifiers);
        if (this.stopTraversal) return;
      }
      this.Traverse(parameterTypeInformation.Type);
    }

    /// <summary>
    /// Traverses the specified platform invoke information.
    /// </summary>
    public virtual void TraverseChildren(IPlatformInvokeInformation platformInvokeInformation) {
      this.Traverse(platformInvokeInformation.ImportModule);
    }

    /// <summary>
    /// Traverses the children of the pointer type reference.
    /// </summary>
    public virtual void TraverseChildren(IPointerTypeReference pointerTypeReference) {
      this.TraverseChildren((ITypeReference)pointerTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(pointerTypeReference.TargetType);
    }

    /// <summary>
    /// Traverses the children of the property definition.
    /// </summary>
    public virtual void TraverseChildren(IPropertyDefinition propertyDefinition) {
      this.TraverseChildren((ITypeDefinitionMember)propertyDefinition);
      if (this.stopTraversal) return;
      this.Traverse(propertyDefinition.Accessors);
      if (this.stopTraversal) return;
      if (propertyDefinition.HasDefaultValue) {
        this.Traverse(propertyDefinition.DefaultValue);
        if (this.stopTraversal) return;
      }
      if (propertyDefinition.Getter != null) {
        this.Traverse(propertyDefinition.Getter);
        if (this.stopTraversal) return;
      }
      this.Traverse(propertyDefinition.Parameters);
      if (this.stopTraversal) return;
      this.Traverse(propertyDefinition.ReturnValueAttributes);
      if (this.stopTraversal) return;
      if (propertyDefinition.Setter != null)
        this.Traverse(propertyDefinition.Setter);
    }

    /// <summary>
    /// Traverses the children of the reference to a manifest resource.
    /// </summary>
    public virtual void TraverseChildren(IResourceReference resourceReference) {
      this.Traverse(resourceReference.Attributes);
      if (this.stopTraversal) return;
      this.Traverse(resourceReference.DefiningAssembly);
    }

    /// <summary>
    /// Traverses the specified root unit namespace.
    /// </summary>
    public virtual void TraverseChildren(IRootUnitNamespace rootUnitNamespace) {
      this.TraverseChildren((IUnitNamespace)rootUnitNamespace);
    }

    /// <summary>
    /// Traverses the specified root unit namespace.
    /// </summary>
    public virtual void TraverseChildren(IRootUnitSetNamespace rootUnitSetNamespace) {
      this.TraverseChildren((IUnitSetNamespace)rootUnitSetNamespace);
    }

    /// <summary>
    /// Traverses the specified root unit namespace reference.
    /// </summary>
    public virtual void TraverseChildren(IRootUnitNamespaceReference rootUnitNamespaceReference) {
      this.TraverseChildren((IUnitNamespaceReference)rootUnitNamespaceReference);
    }

    /// <summary>
    /// Traverses the children of the security attribute.
    /// </summary>
    public virtual void TraverseChildren(ISecurityAttribute securityAttribute) {
      this.Traverse(securityAttribute.Attributes);
    }

    /// <summary>
    /// Traverses the children of the specialized field reference.
    /// </summary>
    public virtual void TraverseChildren(ISpecializedFieldReference specializedFieldReference) {
      this.TraverseChildren((IFieldReference)specializedFieldReference);
      if (this.stopTraversal) return;
      this.Traverse(specializedFieldReference.UnspecializedVersion);
    }

    /// <summary>
    /// Traverses the children of the specialized method reference.
    /// </summary>
    public virtual void TraverseChildren(ISpecializedMethodReference specializedMethodReference) {
      this.TraverseChildren((IMethodReference)specializedMethodReference);
      if (this.stopTraversal) return;
      this.Traverse(specializedMethodReference.UnspecializedVersion);
    }

    /// <summary>
    /// Traverses the children of the specialized nested type reference.
    /// </summary>
    public virtual void TraverseChildren(ISpecializedNestedTypeReference specializedNestedTypeReference) {
      this.TraverseChildren((INestedTypeReference)specializedNestedTypeReference);
      if (this.stopTraversal) return;
      this.Traverse(specializedNestedTypeReference.UnspecializedVersion);
    }

    /// <summary>
    /// Traverses the specified type definition.
    /// </summary>
    public virtual void TraverseChildren(ITypeDefinition typeDefinition) {
      this.Traverse(typeDefinition.Attributes);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.BaseClasses);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.ExplicitImplementationOverrides);
      if (this.stopTraversal) return;
      if (typeDefinition.HasDeclarativeSecurity) {
        this.Traverse(typeDefinition.SecurityAttributes);
        if (this.stopTraversal) return;
      }
      this.Traverse(typeDefinition.Interfaces);
      if (this.stopTraversal) return;
      var namespaceTypeDefinition = typeDefinition as INamespaceTypeDefinition;
      if (namespaceTypeDefinition != null)
        this.TraverseInterfaceImplementationAttributes(namespaceTypeDefinition);
      if (typeDefinition.IsGeneric) {
        this.Traverse(typeDefinition.GenericParameters);
        if (this.stopTraversal) return;
      }
      this.Traverse(typeDefinition.Events);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.Fields);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.Methods);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.NestedTypes);
      if (this.stopTraversal) return;
      this.Traverse(typeDefinition.Properties);
    }

    /// <summary>
    /// Traverses the children of the type definition member.
    /// </summary>
    public virtual void TraverseChildren(ITypeDefinitionMember typeMember) {
      this.Traverse(typeMember.Attributes);
    }

    /// <summary>
    /// Traverses the children of the specified type reference.
    /// </summary>
    public virtual void TraverseChildren(ITypeReference typeReference) {
      //type reference attributes are distinct from type definition attributes. When a definition serves as a reference, the reference is assumed to be unattributed.
      if (!(typeReference is ITypeDefinition))
        this.Traverse(typeReference.Attributes);
    }

    /// <summary>
    /// Traverses the specified unit namespace definition.
    /// </summary>
    public virtual void TraverseChildren(IUnitNamespace namespaceDefinition) {
      this.TraverseChildren((INamespaceDefinition)namespaceDefinition);
    }

    /// <summary>
    /// Traverses the specified unit namespace definition.
    /// </summary>
    public virtual void TraverseChildren(IUnitSetNamespace unitSetNamespace) {
      this.TraverseChildren((INamespaceDefinition)unitSetNamespace);
    }

    /// <summary>
    /// Traverses the specified unit namespace reference.
    /// </summary>
    public virtual void TraverseChildren(IUnitNamespaceReference unitNamespaceReference) {
      this.Traverse(unitNamespaceReference.Unit);
    }

    /// <summary>
    /// Traverses the children of the unit reference.
    /// </summary>
    public virtual void TraverseChildren(IUnitReference unitReference) {
      //no children to traverse
    }

    /// <summary>
    /// Traverses the children of the Win32 resource.
    /// </summary>
    public virtual void TraverseChildren(IWin32Resource win32Resource) {
      //no children to traverse
    }

    /// <summary>
    /// Traverses any attributes that describe how the namespace type definition implements its interfaces.
    /// </summary>
    public virtual void TraverseInterfaceImplementationAttributes(INamespaceTypeDefinition namespaceTypeDefinition) {
      foreach (var iface in namespaceTypeDefinition.Interfaces) {
        this.Traverse(namespaceTypeDefinition.AttributesFor(iface));
        if (this.stopTraversal) break;
      }
    }

  }
}

