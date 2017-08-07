using Shared.Core.Attributes;
using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.Bulks
{
    public enum RecordDelimiterType
    {
        [EnumAttribute(MessageKeyConstants.LABEL_NEW_LINE)]
        NEW_LINE,

        [EnumAttribute(MessageKeyConstants.LABEL_STRING)]
        STRING
    }
}
