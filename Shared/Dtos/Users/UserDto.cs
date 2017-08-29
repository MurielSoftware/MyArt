using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Dtos.Galleries;
using Shared.Dtos.Resources;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shared.Dtos.Users
{
    public class UserDto : UserDefinableDto, IPhotoResourcableDto
    {
        [Display(Name = MessageKeyConstants.LABEL_FIRSTNAME, ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public virtual string FirstName { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_SURNAME, ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public virtual string Surname { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_EMAIL, ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_EMAIL_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        [Remote("IsEmailUnique", WebConstants.CONTROLLER_USER)]
        public virtual string Email { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_PASSWORD, ResourceType = typeof(Resource))]
        public virtual string Password { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_DESCRIPTION, ResourceType = typeof(Resource))]
        public virtual string Description { get; set; }

        public virtual Guid RoleId { get; set; }

        [ListReference(DaoConstants.ATTRIBUTE_ROLE)]
        [Display(Name = MessageKeyConstants.LABEL_ROLE, ResourceType = typeof(Resource))]
        public virtual ReferenceString RoleReference { get; set; }


        [Display(Name = MessageKeyConstants.LABEL_PAINTINGS_COUNT, ResourceType = typeof(Resource))]
        public virtual int PaintingsCount { get; set; }

        public virtual ProfileGalleryDto ProfileGalleryDto { get; set; }
        public virtual PhotoCropDto PhotoCropDto { get; set; }
        public virtual PhotoResourceDto CoverPhotoResourceDto { get; set; }

        public UserDto()
        {
            ProfileGalleryDto = new ProfileGalleryDto(GetType());
            PhotoCropDto = new PhotoCropDto();
        }
    }
}
