using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Administration
{
    public class AdministrationDto : BaseDto
    {
        public virtual int PaintingCount { get; set; }
        public virtual int ArtistCount { get; set; }
        public virtual int CollectionCount { get; set; }
        public virtual int ExhibitionCount { get; set; }
    }
}
