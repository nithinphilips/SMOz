using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afterthought;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Data.Metadata.Edm;

namespace EntityFramework
{
	/// <summary>
	/// Amends types after compilation to support Entity Framework entity interfaces.
	/// </summary>
	/// <typeparam name="TType"></typeparam>
	public class EntityAmendment<TType> : Amendment<TType, IEntityType>
	{
		HashSet<Type> entityTypes;

		public EntityAmendment(HashSet<Type> entityTypes)
		{
			this.entityTypes = entityTypes;
		}

		/// <summary>
		/// Amend the type to implement <see cref="IGraphEntity"/> and related interfaces.
		/// </summary>
		public override void Amend()
		{
			// IEntityType
			ImplementInterface<IEntityType>();

			// IEntityWithKey
			ImplementInterface<IEntityWithKey>();

			// IEntityWithRelationships
			ImplementInterface<IEntityWithRelationships>(
				new Property<RelationshipManager>("RelationshipManager") { LazyInitializer = EntityAdapter.InitializeRelationshipManager }
			);

			// IEntityChangeTracker
			ImplementInterface<IEntityWithChangeTracker>(
				Method.Create<IEntityChangeTracker>("SetChangeTracker", EntityAdapter.SetChangeTracker)
			);
		}

		/// <summary>
		/// Amend public properties to support the entity framework.
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="property"></param>
		public override void Amend<TProperty>(Property<TProperty> property)
		{
			// Only amend public writable properties
			if (property.PropertyInfo.GetGetMethod() == null || property.PropertyInfo.GetSetMethod() == null || !property.PropertyInfo.GetGetMethod().IsPublic)
				return;

			// Amend entity properties
			if (entityTypes.Contains(typeof(TProperty)))
			{
				property.OfType<IEntityType>().Getter = EntityAdapter.GetReference<IEntityType>;
				property.OfType<IEntityType>().Setter = EntityAdapter.SetReference<IEntityType>;
			}

			// Amend entity list properties
			else if ((typeof(TProperty).IsGenericType && typeof(TProperty).GetGenericTypeDefinition() == typeof(ICollection<>) && entityTypes.Contains(typeof(TProperty).GetGenericArguments()[0])))
			{
				property.Getter = EntityAdapter.GetList<TProperty>;
				property.OfType<ICollection<IEntityType>>().Setter = EntityAdapter.SetReference<ICollection<IEntityType>>;
			}

			// Amend value properties
			else
			{
				property.BeforeSet = EntityAdapter.BeforeSetValue<TProperty>;
				property.AfterSet = EntityAdapter.AfterSetValue<TProperty>;
			}
		}
	}
}

