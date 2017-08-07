using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Attributes
{
    /// <summary>
    /// The property attribute for enums.
    /// </summary>
    public class EnumAttribute : Attribute
    {
        public string ResourceKey { get; set; }
        public object DtoType { get; set; }
        //public RoleType? RoleType { get; set; }

        public EnumAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public EnumAttribute(string resourceKey, object type)
            : this(resourceKey)
        {
            DtoType = type;
        }

        //public EnumAttribute(string resourceKey, RoleType roleType)
        //    : this(resourceKey)
        //{
        //    RoleType = roleType;
        //}
    }
}
