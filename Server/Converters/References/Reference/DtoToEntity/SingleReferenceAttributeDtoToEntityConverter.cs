using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using System.Reflection;
using Shared.Core.Dtos;
using Shared.Core.Attributes;
using Server.Converters.References.Utils;

namespace Server.Converters.References.Reference.DtoToEntity
{
    /// <summary>
    /// Converts the single reference from DTO to Entity
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class SingleReferenceAttributeDtoToEntityConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            //PropertyInfo targetProperty = sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            PropertyInfo targetProperty = ReferenceConversionUtils.GetReferencedPropertyId(sourceEntity, referenceAttribute);
            if (referenceString == null || string.IsNullOrEmpty(referenceString.Value))
            {
                targetProperty.SetValue(sourceEntity, null);
            }
            else
            {
                targetProperty.SetValue(sourceEntity, referenceString.GetId());
            }
        }
    }
}
