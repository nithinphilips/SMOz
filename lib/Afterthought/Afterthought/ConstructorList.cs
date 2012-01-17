﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;

namespace Afterthought
{
	public partial class Amendment<TType, TAmended> : Amendment
	{
		#region ConstructorList

		public partial class ConstructorList : ConstructorEnumeration
		{
			IList<Amendment.Constructor> constructors;

			internal ConstructorList()
				: base(new List<Amendment.Constructor>())
			{
				this.constructors = (IList<Amendment.Constructor>)base.constructors;
			}

			internal TConstructor Add<TConstructor>(TConstructor constructor)
				where TConstructor : Amendment.Constructor
			{
				constructors.Add(constructor);
				return constructor;
			}
		}

		#endregion

		#region ConstructorEnumeration

		public partial class ConstructorEnumeration : MemberEnumeration<ConstructorEnumeration>, IEnumerable
		{
			internal IEnumerable<Amendment.Constructor> constructors;

			internal ConstructorEnumeration(IEnumerable<Amendment.Constructor> constructors)
			{
				this.constructors = constructors;
			}

			#region Delegates

			public delegate void BeforeConstructor(TAmended instance, string constructor, object[] parameters);

			public delegate void AfterConstructorAction(TAmended instance, string constructor, object[] parameters);
			
			public delegate object AfterConstructorFunc(TAmended instance, string constructor, object[] parameters, object result);
			
			public delegate void CatchConstructorAction(TAmended instance, string constructor, object[] parameters);
			
			public delegate object CatchConstructorFunc(TAmended instance, string constructor, object[] parameters, object result);

			#endregion

			#region Constructors

			/// <summary>
			/// Gets the set of constructors that match the specified criteria.
			/// </summary>
			/// <param name="predicate"></param>
			/// <returns></returns>
			public ConstructorEnumeration Where(Func<Amendment.Constructor, bool> predicate)
			{
				return new ConstructorEnumeration(constructors.Where(predicate));
			}

			public ConstructorEnumeration Before(BeforeConstructor before)
			{
				foreach (Amendment.Constructor constructor in this)
					constructor.BeforeMethod = before.Method;
				return this;
			}

			public ConstructorEnumeration After(AfterConstructorAction after)
			{
				foreach (Amendment.Constructor constructor in this)
					constructor.AfterMethod = after.Method;
				return this;
			}

			public ConstructorEnumeration After(AfterConstructorFunc after)
			{
				foreach (Amendment.Constructor constructor in this)
					constructor.AfterMethod = after.Method;
				return this;
			}

			public ConstructorEnumeration Catch(CatchConstructorAction @catch)
			{
				foreach (Amendment.Constructor constructor in this)
					constructor.CatchMethod = @catch.Method;
				return this;
			}

			public ConstructorEnumeration Catch(CatchConstructorFunc @catch)
			{
				foreach (Amendment.Constructor constructor in this)
					constructor.CatchMethod = @catch.Method;
				return this;
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return constructors.GetEnumerator();
			}

			#endregion
		}	

		#endregion

		#region ConstructorEnumeration<TConstructor, TEnumeration>

		public partial class ConstructorEnumeration<TConstructor, TEnumeration> : MemberEnumeration<TEnumeration>, IEnumerable
			where TConstructor : Amendment.Constructor
			where TEnumeration : ConstructorEnumeration<TConstructor, TEnumeration>, new()
		{
			protected IEnumerable<TConstructor> constructors;

			/// <summary>
			/// Gets the set of constructors that match the specified criteria.
			/// </summary>
			/// <param name="predicate"></param>
			/// <returns></returns>
			public TEnumeration Where(Func<TConstructor, bool> predicate)
			{
				return new TEnumeration() { constructors = constructors.Where(predicate) };
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return constructors.GetEnumerator();
			}
		}

		#endregion
	}
}
