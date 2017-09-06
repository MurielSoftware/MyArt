using Shared.Core.Attributes;
using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Generators
{
    public enum GenerateFormat
    {
        [Enum(MessageKeyConstants.LABEL_PDF)]
        PDF,

        [Enum(MessageKeyConstants.LABEL_WORD)]
        WORD
    }
}
