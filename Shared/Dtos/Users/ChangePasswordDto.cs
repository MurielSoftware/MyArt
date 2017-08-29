using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Validators;
using Shared.Core.Validators.Comparators;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Users
{
    public class ChangePasswordDto
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = MessageKeyConstants.LABEL_OLD_PASSWORD, ResourceType = typeof(Resource))]
        [PasswordValidator(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_OLD_PASSWORD_WRONG_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = MessageKeyConstants.LABEL_NEW_PASSWORD, ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 5)]
        [CompareValidator("NewPasswordAgain", CompareOperator.EQUAL, CompareType.STRING, ErrorMessageResourceName = MessageKeyConstants.VALIDATION_NEW_PASSWORDS_NOT_EQUAL_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = MessageKeyConstants.LABEL_NEW_PASSWORD_AGAIN, ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 5)]
        public string NewPasswordAgain { get; set; }
    }
}
