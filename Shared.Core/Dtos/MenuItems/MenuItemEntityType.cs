using Shared.Core.Attributes;
using Shared.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.MenuItems
{
    public enum MenuItemEntityType
    {
        [Enum(DaoConstants.ENTITY_TEACHER)]
        USER
    }
}
