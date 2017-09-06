using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exhibitions
{
    public class ExhibitionFilterDto : BaseFilterDto
    {
        [Filter("Paintings.Users.Id", CompareOperator.IS_IN_COLLECTION)]
        public virtual Guid? UserId { get; set; }

        [Filter(DaoConstants.ATTRIBUTE_NAME, CompareOperator.EQUAL)]
        public virtual string Name { get; set; }
    }
}
