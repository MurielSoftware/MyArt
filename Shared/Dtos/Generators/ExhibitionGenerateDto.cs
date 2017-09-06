using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Generators
{
    public class ExhibitionGenerateDto : BaseDto
    {
        public virtual Guid ExhibitionId { get; set; }
        public virtual GenerateFormat Format { get; set; }
    }
}
