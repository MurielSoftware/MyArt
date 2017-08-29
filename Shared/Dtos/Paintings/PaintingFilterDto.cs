using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Paintings
{
    public class PaintingFilterDto : BaseFilterDto
    {
        [Filter(DaoConstants.ATTRIBUTE_USERS_ID, CompareOperator.IS_IN_COLLECTION)]
        public virtual Guid? UserId { get; set; }

        [Filter("Exhibitions.Id", CompareOperator.IS_IN_COLLECTION)]
        public virtual Guid? ExhibitionId { get; set; }
    }
}
