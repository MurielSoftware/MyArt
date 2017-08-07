using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Attributes
{
    public enum CompareOperator
    {
        EQUAL,
        NON_EQUAL,
        GREATER_THAN,
        LESS_THAN,
        GREATER_OR_EQUAL_THAN,
        LESS_OR_EQUAL_THAN,
        CONTAINS,
        IS_IN_COLLECTION
    };

    public class FilterAttribute : Attribute
    {
        public string SourceJoinProperty { get; set; }
        public string TargetProperty { get; set; }
        public CompareOperator Operator { get; set; }
        //public int? EnumAttribute { get; set; }

        public FilterAttribute(string targetProperty, CompareOperator compareOperator)
        {
            TargetProperty = targetProperty;
            Operator = compareOperator;
        }

        public FilterAttribute(string sourceJoinProperty, string targetProperty, CompareOperator compareOperator)
            : this(targetProperty, compareOperator)
        {
            SourceJoinProperty = sourceJoinProperty;
        }
    }
}
