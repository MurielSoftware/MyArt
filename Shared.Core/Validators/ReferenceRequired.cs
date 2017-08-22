using Shared.Core.Dtos;
using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Validators
{
    public class ReferenceRequired : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            if(!(value is ReferenceString))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            ReferenceString referenceString = (ReferenceString)value;
            if(referenceString.Value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
