using Shared.Core.Dtos;
using Shared.Core.Dtos.Resources;
using Shared.Services.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Resources
{
    public class PhotoResourceDto : ResourceDto
    {
        public Type OwnerType { get; set; }
        //public PhotoThumbnailInfo PhotoThumbnailInfo { get; set; }

        public string GetThumbnailName()
        {
            return "t_" + Name;
        }

        public string GetAbsoluteThumbnailFilePath()
        {
            return GetAbsolutePath() + GetThumbnailName();
        }

        public string GetRelativeThumbnailFile()
        {
            return Path + GetThumbnailName();
        }

        public PhotoResourceDto()
        {
            Path = "/Content/images/nophoto.png";
        }

        //public PhotoResourceDto(Type type)
        //{
        //    //PhotoThumbnailInfo = PhotoThumbnailInfoProvider.GetDefault(type);
        //}
    }
}
