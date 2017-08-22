using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Resources
{
    public interface IPhotoResourcableDto : IDto
    {
        PhotoResourceDto PhotoResourceDto { get; set; }
    }
}
