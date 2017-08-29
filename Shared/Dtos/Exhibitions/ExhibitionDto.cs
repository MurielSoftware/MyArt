using Shared.Core.Attributes;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Validators;
using Shared.Dtos.Paintings;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exhibitions
{
    public class ExhibitionDto : UserDefinableDto
    {
        [Display(Name = MessageKeyConstants.LABEL_NAME, ResourceType = typeof(Resource))]
        [Required]
        public virtual string Name { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_ADDRESS, ResourceType = typeof(Resource))]
        public virtual string Address { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_CITY, ResourceType = typeof(Resource))]
        public virtual string City { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_ZIPCODE, ResourceType = typeof(Resource))]
        public virtual string Zipcode { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_GPS, ResourceType = typeof(Resource))]
        public virtual string Gps { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_START, ResourceType = typeof(Resource))]
        [Required]
        public virtual DateTime Start { get; set; }
        public virtual string TimeStart { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_END, ResourceType = typeof(Resource))]
        [Required]
        public virtual DateTime End { get; set; }
        public virtual string TimeEnd { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_DESCRIPTION, ResourceType = typeof(Resource))]
        public virtual string Description { get; set; }

        [Reference(DaoConstants.ATTRIBUTE_PAINTINGS)]
        public virtual ReferenceString PaintingsReference { get; set; }

        public virtual List<PaintingCheckedDto> PaintingsCheckedDto { get; set; }



        public ExhibitionDto()
        {
            Start = DateTime.Now;
            End = DateTime.Now;
        }
    }
}
