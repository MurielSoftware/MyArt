using Server.Model;
using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters.References
{
    public interface IReferenceConverter
    {
        /// <summary>
        /// Converts the reference.
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        /// <param name="sourceEntity">The source entity</param>
        /// <param name="dto">The appropriate DTO</param>
        /// <param name="sourcePropertyInfo">The current property</param>
        /// <param name="referenceAttribute">The referenced attribute of the property</param>
        /// <param name="referenceString">The value of the referenced property</param>
        void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString);
    }
}
