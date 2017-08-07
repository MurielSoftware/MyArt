using Server.Converters.References.Reference;
using Server.Model;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System.Reflection;
using Shared.Core.Attributes;
using Server.Converters.References.Utils;

namespace Server.Converters.References.List.EntityToDto
{
    /// <summary>
    /// Converts the single list reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source Entity</typeparam>
    /// <typeparam name="U">The type of the referenced Entity</typeparam>
    public class SingleListReferenceAttributeEntityToDto<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto targetDto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            PropertyInfo referencedEntityIdPropertyInfo = ReferenceConversionUtils.GetReferencedPropertyId(sourceEntity, referenceAttribute); //sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            U referencedEntity = (U)referencedEntityPropertyInfo.GetValue(sourceEntity, null);
            if (referencedEntity != null)
            {
                sourcePropertyInfo.SetValue(targetDto, new ReferenceString(referencedEntity.Id, referencedEntity.ToString()));
            }
        }
    }
}
