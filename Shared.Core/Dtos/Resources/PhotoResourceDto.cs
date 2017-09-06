using Shared.Core.Utils;
using System;

namespace Shared.Core.Dtos.Resources
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

        public PhotoResourceDto()
        {
            Path = "/Content/images/nophoto.png";
        }

        public static string GetAbsoluteThumbnailFilePath(string relativePath, string name)
        {
            return IOUtils.GetUploadRoot() + relativePath + "t_" + name;
        }
    }
}
