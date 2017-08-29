using System.ComponentModel.DataAnnotations;

namespace Shared.Core.Validators
{
    /// <summary>
    /// Validator for time which is filled as a string.
    /// </summary>
    public class TimeValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] temp = value.ToString().Trim(' ').Split(':');
                int hours = int.Parse(temp[0]);
                int minutes = int.Parse(temp[1]);

                if (hours > 23 || hours < 0 || minutes > 59 || minutes < 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}
