using Shared.Core.Dtos.References;
using System.ComponentModel.DataAnnotations;

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
