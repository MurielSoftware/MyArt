using Server.Model;
using Shared.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters.References.Utils
{
    public class ReferenceConversionUtils
    {
        //public static string GetReferencedId(ReferenceAttribute referenceAttribute)
        //{
        //    return referenceAttribute.RefencedPropertyName + "Id";
        //}

        public static PropertyInfo GetReferencedPropertyId(BaseEntity baseEntity, ReferenceAttribute referenceAttribute)
        {
            PropertyInfo propertyInfo = baseEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            ForeignKeyAttribute foreignKeyAttribute = propertyInfo.GetCustomAttribute<ForeignKeyAttribute>();
            return baseEntity.GetType().GetProperty(foreignKeyAttribute.Name);
            //return referenceAttribute.RefencedPropertyName + "Id";
        }

        public static bool IsCollectionPropertyType(Type type, ReferenceAttribute referenceAttribute)
        {
            PropertyInfo propertyInfo = type.GetProperty(referenceAttribute.RefencedPropertyName);
            // return propertyInfo.PropertyType.IsAssignableFrom(typeof(IEnumerable));
            return typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
        }
    }
}
