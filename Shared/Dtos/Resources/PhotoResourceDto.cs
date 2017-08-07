using Shared.Core.Dtos;
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
    }
}
