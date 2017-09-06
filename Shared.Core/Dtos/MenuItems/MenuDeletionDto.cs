using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.MenuItems
{
    public class MenuDeletionDto : DeletionDto
    {
        public virtual Guid? ParentMenuItemId { get; set; }
    }
}
