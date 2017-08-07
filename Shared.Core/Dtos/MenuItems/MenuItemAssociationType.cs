using Shared.Core.Attributes;
using Shared.I18n.Constants;
using System;

namespace Shared.Core.Dtos.MenuItems
{
    public enum MenuItemAssociationType
    {
        [Enum(MessageKeyConstants.LABEL_EMPTY_LINK)]
        EMPTY_LINK,

        [Enum(MessageKeyConstants.LABEL_SECTION_MENU)]
        SECTION_MENU,

        [Enum(MessageKeyConstants.LABEL_HOME)]
        HOME,

        [Enum(MessageKeyConstants.LABEL_LINK_TO_LIST)]
        LINK_TO_LIST,

        [Enum(MessageKeyConstants.LABEL_LINK_TO_SPECIFIC)]
        LINK_TO_SPECIFIC,

        [Enum(MessageKeyConstants.LABEL_LINK)]
        LINK
    }
}
