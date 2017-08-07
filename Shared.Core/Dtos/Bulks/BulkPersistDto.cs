using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.Bulks
{
    public class BulkPersistDto : BaseDto
    {
        [Display(Name = MessageKeyConstants.LABEL_INPUT, ResourceType = typeof(Resource))]
        public virtual string Input { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_ATTRIBUTE_DELIMITER, ResourceType = typeof(Resource))]
        public virtual string AttributeDelimiter { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_RECORD_DELIMITER, ResourceType = typeof(Resource))]
        public virtual string RecordDelimiter { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_RECORD_DELIMITER_TYPE, ResourceType = typeof(Resource))]
        public virtual RecordDelimiterType RecordDelimiterType { get; set; }
    }
}
