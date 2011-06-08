using System;
using System.Collections.Generic;
using System.ComponentModel;
using Afterthought;

[assembly: AsmTest.NotificationAmender]

namespace AsmTest
{

    public class NotificationAmenderAttribute : Attribute, IAmendmentAttribute
    {
        IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments( Type target )
        {
            if (target.GetCustomAttributes( typeof( NotifyPropertyChangedAttribute ), true ).Length > 0 && target.GetInterface( "System.ComponentModel.INotifyPropertyChanged" ) == null)
            {
                yield return (ITypeAmendment)typeof( NotificationAmendment<> ).MakeGenericType( target ).GetConstructor( Type.EmptyTypes ).Invoke( new object[ 0 ] );
            }
        }
    }

    public static class NotificationAmender<T>
    {
        public static void OnPropertyChanged<P>( INotifyPropertyChangedAmendment instance, string property, P oldValue, P value, P newValue )
        {
            // Only raise property changed if the value of the property actually changed
            if ((oldValue == null ^ newValue == null) || (oldValue != null && !oldValue.Equals( newValue )))
            {
                instance.OnPropertyChanged( new PropertyChangedEventArgs( property ) );
            }
        }
    }

    public class NotificationAmendment<T> : Afterthought.Amendment<T, INotifyPropertyChangedAmendment>
    {
        public override void Amend()
        {
            // Create the PropertyChanged event
            var propertyChanged = new Event<PropertyChangedEventHandler>( "PropertyChanged" );

            // Implement INotifyPropertyChanged, specifying the PropertyChanged event
            ImplementInterface<INotifyPropertyChanged>( propertyChanged );

            // Implement INotifyPropertyChangedAmendment, specifying a method that raises the PropertyChanged event
            ImplementInterface<INotifyPropertyChangedAmendment>
            (
                propertyChanged.RaisedBy( "OnPropertyChanged" )
            );
        }

        public override void Amend<TProperty>( Property<TProperty> property )
        {
            // Raise property change notifications
            //if (property.PropertyInfo.CanRead && property.PropertyInfo.CanWrite
            //    && property.PropertyInfo.GetSetMethod() != null && property.PropertyInfo.GetSetMethod().IsPublic)
            //{
            //    property.AfterSet = NotificationAmender<T>.OnPropertyChanged<TProperty>;
            //}
        }
    }

    public interface INotifyPropertyChangedAmendment : INotifyPropertyChanged
    {
        void OnPropertyChanged( PropertyChangedEventArgs args );
    }

    [AttributeUsage( AttributeTargets.Class )]
    public sealed class NotifyPropertyChangedAttribute : Attribute
    {
    }

    [AttributeUsage( AttributeTargets.Property )]
    public sealed class AlsoNotifyAsChangedAttribute : Attribute
    {
        public AlsoNotifyAsChangedAttribute( params string[] others )
        {
            Properties = others;
        }
        public string[] Properties { get; set; }
    }
}
