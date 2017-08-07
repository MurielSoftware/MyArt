using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Users
{
    public class UserFilterDto : BaseFilterDto
    {
        [Filter(DaoConstants.ATTRIBUTE_FIRSTNAME, CompareOperator.CONTAINS)]
        public virtual string FirstName { get; set; }

        [Filter(DaoConstants.ATTRIBUTE_SURNAME, CompareOperator.CONTAINS)]
        public virtual string Surname { get; set; }
    }
}
