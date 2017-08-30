using Server.Model;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using System.Reflection;
using Shared.Core.Attributes;
using Server.Converters.References.Utils;
using Shared.Core.Dtos.References;

namespace Server.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Base Entity to DTO reference converter.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the target DTO</typeparam>
    public class ReferenceAttributeEntityToDtoConverter<T, U> : BaseReferenceAttributeConverter<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
        public override ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return target.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ReferenceAttribute)) && !Attribute.IsDefined(x, typeof(ListReferenceAttribute)))
                .ToList();
        }

        public override void Convert(IUnitOfWork unitOfWork, T entity, U dto, PropertyInfo sourcePropertyInfo)
        {
            ReferenceAttribute referenceAttribute = sourcePropertyInfo.GetCustomAttribute<ReferenceAttribute>();
            ReferenceString referenceString = sourcePropertyInfo.GetValue(dto) as ReferenceString;
            Type referencedEntityType = GetReferencedEntityType(entity, referenceAttribute);
            IReferenceConverter referenceConverter = GetReferenceConverter(referenceAttribute, typeof(T), referencedEntityType);
            referenceConverter.Convert(unitOfWork, entity, dto, sourcePropertyInfo, referenceAttribute, referenceString);
        }

        private IReferenceConverter GetReferenceConverter(ReferenceAttribute referenceAttribute, Type sourceEntityType, Type referencedEntityType)
        {
            if (ReferenceConversionUtils.IsCollectionPropertyType(sourceEntityType, referenceAttribute))
            {
                return CreateReferenceConverter(typeof(MultiReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
            }
            return CreateReferenceConverter(typeof(SingleReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
        }
    }
}
