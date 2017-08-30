using System;
using Shared.Core.Attributes;
using Shared.Core.Utils;

namespace Shared.Core.Validators.Comparators
{
    /// <summary>
    /// The time comparator.
    /// </summary>
    public class TimeComparator : IComparator<string>
    {
        public bool Compare(string first, string second, CompareOperator expectedCompareOperator)
        {
            DateTime? firstDateTime = StringUtils.ParseTime(first);
            DateTime? secondDateTime = StringUtils.ParseTime(second);

            if(firstDateTime > secondDateTime)
            {
                return CompareOperator.GREATER_THAN == expectedCompareOperator;
            }
            else if(firstDateTime < secondDateTime)
            {
                return CompareOperator.LESS_THAN == expectedCompareOperator;
            }
            return CompareOperator.EQUAL == expectedCompareOperator;
        }
    }
}