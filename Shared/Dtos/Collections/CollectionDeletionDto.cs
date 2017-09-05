using Shared.Core.Dtos;
using Shared.I18n.Constants;
using Shared.I18n.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Collections
{
    public class CollectionDeletionDto : DeletionDto
    {
        [Display(Name = MessageKeyConstants.LABEL_DELETE_ALL_PAINTINGS, ResourceType = typeof(Resource))]
        public virtual bool DeleteAllPaintings { get; set; }

        [Display(Name = MessageKeyConstants.LABEL_SET_PAINTINGS_TO_DEFAULT_COLLECTION, ResourceType = typeof(Resource))]
        public virtual bool SetPaintingsToDefaultCollection { get; set; }
    }
}
