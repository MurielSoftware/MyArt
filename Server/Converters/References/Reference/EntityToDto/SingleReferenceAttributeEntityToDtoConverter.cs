using Server.Model;
using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System.Reflection;
using Server.Converters.References.Utils;
using Shared.Core.Dtos.References;

namespace Server.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Converts the single reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the target entity</typeparam>
    public class SingleReferenceAttributeEntityToDtoConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            PropertyInfo referencedEntityIdPropertyInfo = ReferenceConversionUtils.GetReferencedPropertyId(sourceEntity, referenceAttribute); //sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            U referencedEntity = (U)referencedEntityPropertyInfo.GetValue(sourceEntity, null);
            if(referencedEntity != null)
            {
                sourcePropertyInfo.SetValue(dto, new ReferenceString(referencedEntity.Id, referencedEntity.ToString()));
            }
        }
    }
}
