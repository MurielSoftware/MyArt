using Shared.Core.Dtos;
using Shared.Dtos.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Resources
{
    public interface IPhotoResourcableDto : IDto
    {
        ProfileGalleryDto ProfileGalleryDto { get; set; }
        PhotoResourceDto CoverPhotoResourceDto { get; set; }
    }
}
