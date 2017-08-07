using Shared.Core.Context;
using Shared.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters.Attributes
{
    /// <summary>
    /// Converts the primitive properties (string, int,...)
    /// </summary>
    /// <typeparam name="T">The source type</typeparam>
    /// <typeparam name="U">The target type</typeparam>
    public class PrimitiveAttributeConverter<T, U> : IConverter<T, U>
    {
        public void Convert(IUnitOfWork unitOfWork, T source, U target, PropertyInfo sourcePropertyInfo)
        {
            PropertyInfo targetPropertyInfo = target.GetType().GetProperty(sourcePropertyInfo.Name);
            if(targetPropertyInfo == null)
            {
                return;
            }
            targetPropertyInfo.SetValue(target, sourcePropertyInfo.GetValue(source));
        }

        public ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return source.GetType().GetProperties();
        }
    }
}
