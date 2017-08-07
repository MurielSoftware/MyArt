using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Attributes
{
    public class ReferenceAttribute : Attribute
    {
        public string RefencedPropertyName { get; private set; }

        public ReferenceAttribute(string refencedPropertyName)
        {
            RefencedPropertyName = refencedPropertyName;
        }
    }
}
