using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos
{
    public class CheckedDto : BaseDto
    {
        public virtual bool Checked { get; set; }
    }
}
