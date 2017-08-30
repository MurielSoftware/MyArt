using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.References
{
    public class ListReferenceDto
    {
        public Dictionary<Guid, string> References { get; set; }

        public ListReferenceDto()
        {
            References = new Dictionary<Guid, string>();
        }
    }
}
