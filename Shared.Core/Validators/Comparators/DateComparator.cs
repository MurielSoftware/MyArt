using System;
using Shared.Core.Attributes;

namespace Shared.Core.Validators.Comparators
{
    /// <summary>
    /// Date comparator.
    /// </summary>
    public class DateComparator : IComparator<DateTime>
    {
        public bool Compare(DateTime first, DateTime second, CompareOperator expectedCompareOperator)
        {
            if (first > second)
            {
                return CompareOperator.GREATER_THAN == expectedCompareOperator || CompareOperator.GREATER_OR_EQUAL_THAN == expectedCompareOperator;
            }
            else if (first < second)
            {
                return CompareOperator.LESS_THAN == expectedCompareOperator || CompareOperator.LESS_OR_EQUAL_THAN == expectedCompareOperator;
            }
            return CompareOperator.EQUAL == expectedCompareOperator || CompareOperator.GREATER_OR_EQUAL_THAN == expectedCompareOperator || CompareOperator.LESS_OR_EQUAL_THAN == expectedCompareOperator; 
        }
    }
}