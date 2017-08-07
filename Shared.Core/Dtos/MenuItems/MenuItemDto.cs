using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.MenuItems
{
    public class MenuItemDto : BaseDto
    {
        [Display(Name = MessageKeyConstants.LABEL_NAME, ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = MessageKeyConstants.VALIDATION_REQUIRED_MESSAGE, ErrorMessageResourceType = typeof(Resource))]
        public virtual string Name { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_LINK, ResourceType = typeof(Resource))]
        public virtual string Url { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_BUILTIN, ResourceType = typeof(Resource))]
        public virtual bool BuiltIn { get; set; }

        public virtual int Order { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_LINK, ResourceType = typeof(Resource))]
        public virtual MenuItemEntityType EntityType { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_ASSOCIATION_TYPE, ResourceType = typeof(Resource))]
        public virtual MenuItemAssociationType AssociationType { get; set; }

        public virtual int Level { get; set; }

        public virtual Guid? BlogCategoryId { get; set; }
        public virtual Guid? ParentMenuItemId { get; set; }
        public virtual Guid? UserDefinableId { get; set; }

        [Reference("UserDefinable")]
        public virtual ReferenceString UserDefinableReference { get; set; }

        public virtual bool HasChildren { get; set; }
        public virtual ICollection<MenuItemDto> SectionSubmenu { get; set; }
    }
}
