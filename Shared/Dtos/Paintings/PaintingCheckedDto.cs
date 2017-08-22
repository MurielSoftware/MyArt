using Shared.Core.Dtos;
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

        public override string ToString()
        {
            return Title;
        }
    }
}
