using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Dtos.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Resources
{
    public interface IPhotoCRUDService : ICRUDService<PhotoResourceDto>
    {
        void Crop(PhotoCropDto photoCropDto);
    }
}
