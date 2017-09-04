using Shared.Core.Dtos;
using Shared.Core.Dtos.Resources;
using Shared.Dtos.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Paintings
{
    public class PaintingCheckedDto : CheckedDto
    {
        public virtual string Title { get; set; }
        public virtual PhotoResourceDto PhotoResourceDto { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
