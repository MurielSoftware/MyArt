using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos.References;
using Shared.Core.Validators;
using Shared.Dtos.Galleries;
using Shared.Dtos.Resources;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Paintings
{
    public class PaintingDto : UserDefinableDto, IPhotoResourcableDto
    {
        [Display(Name = MessageKeyConstants.LABEL_TITLE, ResourceType = typeof(Resource))]
        [Required]
        public virtual string Title { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_DATE_FINISH, ResourceType = typeof(Resource))]
        [Required]
        public virtual DateTime Date { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_SURFACE, ResourceType = typeof(Resource))]
        [Required]
        public virtual Surface Surface { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_TECHNIQUE, ResourceType = typeof(Resource))]
        [Required]
        public virtual Technique Technique { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_WIDTH, ResourceType = typeof(Resource))]
        [Required]
        public virtual float Width { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_HEIGHT, ResourceType = typeof(Resource))]
        [Required]
        public virtual float Height { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_DESCRIPTION, ResourceType = typeof(Resource))]
        public virtual string Description { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_INFO, ResourceType = typeof(Resource))]
        public virtual string Info { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_COLLECTION, ResourceType = typeof(Resource))]
        public virtual Guid? CollectionId { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_OWNER, ResourceType = typeof(Resource))]
        public virtual string Owner { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_COLLECTION, ResourceType = typeof(Resource))]
        [ListReference(DaoConstants.ATTRIBUTE_COLLECTION)]
        public virtual ReferenceString CollectionReference { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_AUTHOR, ResourceType = typeof(Resource))]
        [Reference(DaoConstants.ATTRIBUTE_USERS)]
        [ReferenceRequired(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public virtual ReferenceString UsersReference { get; set; }

        public virtual ProfileGalleryDto ProfileGalleryDto { get; set; }
        public virtual PhotoResourceDto CoverPhotoResourceDto { get; set; }

        public PaintingDto()
        {
            Date = DateTime.Now;
            ProfileGalleryDto = new ProfileGalleryDto(GetType());
            CoverPhotoResourceDto = new PhotoResourceDto();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
