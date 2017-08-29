using Shared.Core.Utils;
using System.ComponentModel.DataAnnotations;

namespace Shared.Core.Validators
{
    /// <summary>
    /// Validator for password.
    /// </summary>
    public class PasswordValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //UserSession userSession = SessionManager.GetInstance().GetSession<UserSession>(UserSession.SESSION_NAME);
            //if (userSession == null || value == null)
            //{
            //    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            //}

            //if (!PasswordUtils.Verify(userSession.UserAuthenticationDto.Password, value.ToString()))
            //{
            //    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            //}
            return ValidationResult.Success;
        }
    }
}
