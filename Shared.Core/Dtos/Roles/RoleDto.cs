using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System.ComponentModel.DataAnnotations;

namespace Shared.Core.Dtos.Roles
{
    public class RoleDto : BaseDto
    {
        [Display(Name = MessageKeyConstants.LABEL_NAME, ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public virtual string Name { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_USER_CREATION, ResourceType = typeof(Resource))]
        public virtual bool UserCreation { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_ROLE_CREATION, ResourceType = typeof(Resource))]
        public virtual bool RoleCreation { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_CREATE_UPDATE_DELETE_ALL, ResourceType = typeof(Resource))]
        public virtual bool CreateUpdateDeleteAll { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_MENU_CREATION, ResourceType = typeof(Resource))]
        public virtual bool MenuCreation { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
