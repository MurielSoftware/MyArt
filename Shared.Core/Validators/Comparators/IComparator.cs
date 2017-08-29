using Shared.Core.Attributes;

namespace Shared.Core.Validators.Comparators
{
    /// <summary>
    /// Interface should be implemented by all comparators.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare</typeparam>
    public interface IComparator<T>
    {
        /// <summary>
        /// Compares two values and compare the result with the expected compare result. 
        /// If the result is expected returns true, otherwise return false.
        /// </summary>
        /// <param name="first">The first value to compare</param>
        /// <param name="second">The second value to compare</param>
        /// <param name="expectedCompareOperator">The expected compare operator.</param>
        /// <returns>Returns true if the compare of the values is expected, otherwise it returns false</returns>
        bool Compare(T first, T second, CompareOperator expectedCompareOperator);
    }
}
