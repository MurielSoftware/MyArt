using System.Collections.Generic;
using Server.Model;
using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System.Reflection;
using Shared.Core.Dtos.References;

namespace Server.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Converts the multi reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class MultiReferenceAttributeEntityToDtoConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            IEnumerable<U> referencedEntities = (IEnumerable<U>)referencedEntityPropertyInfo.GetValue(sourceEntity, null);

            if (referencedEntities == null)
            {
                return;
            }

            ReferenceString referencedString = new ReferenceString(string.Empty);
            foreach (U referencedEntity in referencedEntities)
            {
                referencedString.Append(referencedEntity.Id, referencedEntity.ToString());
            }
            sourcePropertyInfo.SetValue(dto, referencedString);
        }
    }
}
