using Shared.Core.Attributes;

namespace Shared.Core.Validators.Comparators
{
    /// <summary>
    /// The string equality validator.
    /// </summary>
    public class StringComparator : IComparator<string>
    {
        public bool Compare(string first, string second, CompareOperator expectedCompareOperator)
        {
            if (first.Equals(second))
            {
                return CompareOperator.EQUAL == expectedCompareOperator;
            }
            return CompareOperator.NON_EQUAL == expectedCompareOperator;
        }
    }
}
