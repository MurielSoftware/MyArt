using Shared.Core.Attributes;
using Shared.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.Resources
{
    public class ResourceFilterDto : BaseFilterDto
    {
        [Filter(DaoConstants.USER_DEFINABLE_ID, CompareOperator.EQUAL)]
        public virtual Guid? UserDefinableId { get; set; }

        [Filter("UserDefinable.OwnerId", CompareOperator.EQUAL)]
        public virtual Guid? UserDefinableOwnerId { get; set; }
    }
}
