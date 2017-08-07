using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Attributes
{
    public class ListReferenceAttribute : ReferenceAttribute
    {
        public ListReferenceAttribute(string refencedPropertyName) 
            : base(refencedPropertyName)
        {
        }
    }
}
