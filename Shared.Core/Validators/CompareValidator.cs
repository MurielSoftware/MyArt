using Shared.Core.Attributes;
using Shared.Core.Validators.Comparators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Core.Validators
{
    /// <summary>
    /// Comparator validator for compare two values.
    /// </summary>
    public class CompareValidator : ValidationAttribute
    {
        private string _comparePropertyName;
        private CompareOperator _compareOperator;
        private CompareType _compareType;

        public CompareValidator(string comparePropertyName, CompareOperator compareOperator, CompareType compareType)
        {
            _comparePropertyName = comparePropertyName;
            _compareOperator = compareOperator;
            _compareType = compareType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(_comparePropertyName);

            if(propertyInfo == null)
            {
                return null;
            }

            if(value == null)
            {
                return null;
            }

            if(!propertyInfo.PropertyType.Equals(value.GetType()))
            {
                return null;
            }

            bool result = false;

            switch(_compareType)
            {
                case CompareType.NUMBER:
                    result = new NumberComparator().Compare((float)value, (float)propertyInfo.GetValue(validationContext.ObjectInstance), _compareOperator);
                    break;
                case CompareType.TIME:
                    result = new TimeComparator().Compare((string)value, (string)propertyInfo.GetValue(validationContext.ObjectInstance), _compareOperator);
                    break;
                case CompareType.DATE:
                    result = new DateComparator().Compare((DateTime)value, (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance), _compareOperator);
                    break;
                case CompareType.STRING:
                    result = new StringComparator().Compare((string)value, (string)propertyInfo.GetValue(validationContext.ObjectInstance), _compareOperator);
                    break;
            }

            if (!result)
            {
                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, _comparePropertyName));
            }

            return ValidationResult.Success;
        }
    }
}
