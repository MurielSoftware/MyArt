using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UserDefinableDto : BaseDto
    {
        public virtual bool Public { get; set; }

        public virtual ReferenceString UserCreatorReference { get; set; }
        public virtual Guid? UserCreatorId { get; set; }
    }
}
