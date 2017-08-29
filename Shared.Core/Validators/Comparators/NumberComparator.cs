using Shared.Core.Attributes;

namespace Shared.Core.Validators.Comparators
{
    /// <summary>
    /// The Number comparator.
    /// </summary>
    public class NumberComparator : IComparator<float>
    {
        public bool Compare(float first, float second, CompareOperator expectedCompareOperator)
        {
            if(first > second)
            {
                return CompareOperator.GREATER_THAN == expectedCompareOperator;
            }
            else if(first<second)
            {
                return CompareOperator.LESS_THAN == expectedCompareOperator;
            }
            return CompareOperator.EQUAL == expectedCompareOperator;
        }
    }
}
