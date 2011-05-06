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
	#region Amendment.Event

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into 
	/// a specific <see cref="Type"/> during compilation.
	/// </summary>
	public abstract partial class Amendment
	{
		public abstract class Event : InterfaceMember, IEventAmendment
		{
			internal Event(string name)
				: base(name)
			{ }

			internal Event(EventInfo eventInfo)
				: base(eventInfo.Name)
			{
				this.EventInfo = eventInfo;
			}

			public abstract Type Type { get; }

			public EventInfo EventInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return base.IsAmended || AdderMethod != null || RemoverMethod != null ||
						BeforeAddMethod != null || AfterAddMethod != null || BeforeRemoveMethod != null || AfterRemoveMethod != null;
				}
			}

			EventInfo IEventAmendment.Implements { get { return Implements; } }

			MethodInfo IEventAmendment.Adder { get { return AdderMethod; } }

			MethodInfo IEventAmendment.Remover { get { return RemoverMethod; } }

			MethodInfo IEventAmendment.BeforeAdd { get { return BeforeAddMethod; } }

			MethodInfo IEventAmendment.AfterAdd { get { return AfterAddMethod; } }

			MethodInfo IEventAmendment.BeforeRemove { get { return BeforeRemoveMethod; } }

			MethodInfo IEventAmendment.AfterRemove { get { return AfterRemoveMethod; } }

			EventInfo implements;
			internal EventInfo Implements
			{
				get
				{
					return implements;
				}
				set
				{
					if (implements != null)
						throw new InvalidOperationException("The event implementation may only be set once.");
					implements = value;
					if (implements != null)
						Name = implements.DeclaringType.FullName + "." + implements.Name;
				}
			}

			internal MethodInfo AdderMethod { get; set; }

			internal MethodInfo RemoverMethod { get; set; }

			internal MethodInfo BeforeAddMethod { get; set; }

			internal MethodInfo AfterAddMethod { get; set; }

			internal MethodInfo BeforeRemoveMethod { get; set; }

			internal MethodInfo AfterRemoveMethod { get; set; }

			/// <summary>
			/// Creates a concrete event with the specified instance type, event type, and name.
			/// </summary>
			/// <param name="instanceType"></param>
			/// <param name="eventType"></param>
			/// <param name="name"></param>
			/// <returns></returns>
			public static Event Create(Type instanceType, Type eventType, string name)
			{
				Type amendmentType = typeof(Amendment<,>).MakeGenericType(instanceType, instanceType);
				Type eventAmendmentType = amendmentType.GetNestedType("Event`1").MakeGenericType(instanceType, instanceType, eventType);
				return (Event)eventAmendmentType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string) }, null).Invoke(new object[] { name });
			}

			/// <summary>
			/// Creates a new <see cref="Event"/> that implements the specified interface event.
			/// </summary>
			/// <param name="instanceType"></param>
			/// <param name="interfaceEvent"></param>
			/// <returns></returns>
			public static Event Implement(Type instanceType, EventInfo interfaceEvent)
			{
				// Ensure the event is declared on an interface
				if (!interfaceEvent.DeclaringType.IsInterface)
					throw new ArgumentException("Only interface events may be implemented.");

				var evt = Create(instanceType, interfaceEvent.EventHandlerType, interfaceEvent.Name);
				evt.Implements = interfaceEvent;
				return evt;
			}

			/// <summary>
			/// Creates a new method that will raise this event.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			public abstract Method RaisedBy(string name);
		}
	}

	#endregion

	#region Amendment<TType, TAmended>.Event<TEvent>

	public partial class Amendment<TType, TAmended> : Amendment
	{
		public class Event<TEvent> : Event
		{
			public Event(string name)
				: base(name)
			{ }

			internal Event(EventInfo prop)
				: base(prop)
			{ }

			protected virtual Event UnderlyingEvent
			{
				get
				{
					return this;
				}
			}

			public override Type Type
			{
				get
				{
					return typeof(TEvent);
				}
			}

			public EventAdder Adder { set { UnderlyingEvent.AdderMethod = value.Method; } }

			public EventRemover Remover { set { UnderlyingEvent.RemoverMethod = value.Method; } }

			public EventAdder BeforeAdd { set { UnderlyingEvent.BeforeAddMethod = value.Method; } }

			public EventAdder AfterAdd { set { UnderlyingEvent.AfterAddMethod = value.Method; } }

			public EventRemover BeforeRemove { set { UnderlyingEvent.BeforeRemoveMethod = value.Method; } }

			public EventRemover AfterRemove { set { UnderlyingEvent.AfterRemoveMethod = value.Method; } }

			public delegate void EventAdder(TAmended instance, string eventName, TEvent value);

			public delegate void EventRemover(TAmended instance, string eventName, TEvent value);

			public Event<TActual> OfType<TActual>()
			{
				return new Event<TEvent, TActual>(this);
			}

			/// <summary>
			/// Creates a new method that will raise this event.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			public override Amendment.Method RaisedBy(string name)
			{
				Console.WriteLine("What!");
				return Method.Raise(name, this);
			}
		}
	}

	#endregion

	#region Amendment<TType, TAmended>.Event<TEvent, TAmended>

	public partial class Amendment<TType, TAmended> : Amendment
	{
		internal class Event<TEvent, TAmended> : Event<TAmended>
		{
			Event<TEvent> underlyingEvent;

			internal Event(Event<TEvent> underlyingEvent)
				: base(underlyingEvent.EventInfo)
			{
				this.underlyingEvent = underlyingEvent;
			}

			protected override Event UnderlyingEvent
			{
				get
				{
					return underlyingEvent;
				}
			}
		}
	}

	#endregion

}
