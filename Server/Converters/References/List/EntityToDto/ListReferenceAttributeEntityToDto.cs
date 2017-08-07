using Server.Model;
using Shared.Core.Dtos;
using Shared.Core.Context;
using System.Reflection;
using Server.Converters.References.Reference;
using Shared.Core.Attributes;
using Server.Converters.References.Reference.EntityToDto;
using System;
using Server.Converters.References.Utils;

namespace Server.Converters.References.List.EntityToDto
{
    /// <summary>
    /// Converts the list reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the Entity</typeparam>
    /// <typeparam name="U">The type of the DTO</typeparam>
    public class ListReferenceAttributeEntityToDto<T, U> : BaseListReferenceAttributeConverter<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
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
                return null;
            }
            return CreateReferenceConverter(typeof(SingleListReferenceAttributeEntityToDto<,>), sourceEntityType, referencedEntityType);
        }
    }
}
