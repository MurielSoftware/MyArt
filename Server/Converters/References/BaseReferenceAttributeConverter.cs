using Server.Converters.References.Utils;
using Server.Model;
using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters.References
{
    /// <summary>
    /// Base reference converter.
    /// </summary>
    /// <typeparam name="T">The source type</typeparam>
    /// <typeparam name="U">The target type</typeparam>
    public abstract class BaseReferenceAttributeConverter<T, U> : IConverter<T, U>
    {
        public abstract ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target);

        public abstract void Convert(IUnitOfWork unitOfWork, T source, U target, PropertyInfo sourcePropertyInfo);

        /// <summary>
        /// Creates the single or multi converter instance.
        /// </summary>
        /// <param name="converterType">The type of the converter</param>
        /// <param name="sourceEntityType">The source entity type</param>
        /// <param name="referencedEntityType">The referenced entity type</param>
        /// <returns></returns>
        protected IReferenceConverter CreateReferenceConverter(Type converterType, Type sourceEntityType, Type referencedEntityType)
        {
            Type type = converterType.MakeGenericType(new Type[] { sourceEntityType, referencedEntityType });
            return (IReferenceConverter)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Gets the type of the referenced entity. If it is multiple reference the type is got from the generic type of the collection.
        /// </summary>
        /// <param name="entity">The source entity</param>
        /// <param name="referencedAttribute">The referenced attribute of the DTO property</param>
        /// <returns>The type of the referenced entity</returns>
        protected Type GetReferencedEntityType(BaseEntity entity, ReferenceAttribute referenceAttribute)
        {
            PropertyInfo referencePropertyInfo = entity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            if(ReferenceConversionUtils.IsCollectionPropertyType(entity.GetType(), referenceAttribute))
            {
                return GetReferencedTargetPropertyGenericType(referencePropertyInfo);
            }
            return referencePropertyInfo.PropertyType;
        }

        private Type GetReferencedTargetPropertyGenericType(PropertyInfo targetProperty)
        {
            return targetProperty.PropertyType.GetGenericArguments().FirstOrDefault();
        }
    }
}
