using Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters
{
    public interface IConverter<T, U>
    {
        /// <summary>
        /// Gets the properties to convert by this specific converter.
        /// </summary>
        /// <param name="source">The source entity to convert</param>
        /// <param name="target">The target entity</param>
        /// <returns>Returns the list of the properties to convert</returns>
        ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target);

        /// <summary>
        /// Converts the property.
        /// </summary>
        /// <param name="unitOfWork">The current unit of Work </param>
        /// <param name="source">The source entity to convert</param>
        /// <param name="target">The target entity</param>
        /// <param name="sourcePropertyInfo">The current property to convert</param>
        void Convert(IUnitOfWork unitOfWork, T source, U target, PropertyInfo sourcePropertyInfo);
    }
}
