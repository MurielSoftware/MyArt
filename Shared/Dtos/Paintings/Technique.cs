using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Attributes;

namespace Shared.Dtos.Paintings
{
    public enum Technique
    {
        [Enum(MessageKeyConstants.LABEL_PEN)]
        PEN,

        [Enum(MessageKeyConstants.LABEL_PENCIL)]
        PENCIL,

        [Enum(MessageKeyConstants.LABEL_CRAYONS)]
        CRAYONS,

        [Enum(MessageKeyConstants.LABEL_AQUAREL)]
        AQUAREL,

        [Enum(MessageKeyConstants.LABEL_ACRYLIC)]
        ACRYLIC,

        [Enum(MessageKeyConstants.LABEL_OIL)]
        OIL,

        [Enum(MessageKeyConstants.LABEL_TEMPER)]
        TEMPER,

        [Enum(MessageKeyConstants.LABEL_PASTEL)]
        PASTEL,

        [Enum(MessageKeyConstants.LABEL_CHALK)]
        CHALK
    }
}
