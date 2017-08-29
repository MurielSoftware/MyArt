using Shared.Services.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Galleries
{
    public class GalleryDto : UserDefinableDto
    {
        public virtual string Name { get; set; }
        public virtual Guid? CoverPhotoId { get; set; }
        public virtual Guid? OwnerId { get; set; }

        public virtual PhotoThumbnailInfo PhotoThumbnailInfo { get; set; }

        public GalleryDto()
        {

        }

        public GalleryDto(Type type)
        {
            PhotoThumbnailInfo = PhotoThumbnailInfoProvider.GetDefault(type);
        }
    }
}
