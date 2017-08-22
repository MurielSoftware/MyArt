using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Attributes;

namespace Shared.Dtos.Paintings
{
    public enum Surface
    {
        [Enum(MessageKeyConstants.LABEL_PAPER)]
        PAPER,

        [Enum(MessageKeyConstants.LABEL_SOLOLIT)]
        SOLOLIT,

        [Enum(MessageKeyConstants.LABEL_CANVAS)]
        CANVAS,

        [Enum(MessageKeyConstants.LABEL_GLASS)]
        GLASS,

        [Enum(MessageKeyConstants.LABEL_WOOD)]
        WOOD
    }
}
