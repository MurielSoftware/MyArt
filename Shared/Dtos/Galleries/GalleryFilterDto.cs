using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Galleries
{
    public class GalleryFilterDto : BaseFilterDto
    {
        [Filter(DaoConstants.ATTRIBUTE_OWNER_ID, CompareOperator.EQUAL)]
        public virtual Guid? OwnerId { get; set; }
    }
}
