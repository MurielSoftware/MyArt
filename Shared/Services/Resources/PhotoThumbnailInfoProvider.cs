using Shared.Dtos.Paintings;
using Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Resources
{
    public class PhotoThumbnailInfoProvider
    {
        private static Dictionary<Type, PhotoThumbnailInfo> DTO_TYPE_TO_GALLERY_SPECIFICATION = CreateTypeToPhotoThumbnailInfo();

        public static PhotoThumbnailInfo GetDefault(Type dtoType)
        {
            if (!DTO_TYPE_TO_GALLERY_SPECIFICATION.ContainsKey(dtoType))
            {
                return null;
            }
            return DTO_TYPE_TO_GALLERY_SPECIFICATION[dtoType];
        }

        private static Dictionary<Type, PhotoThumbnailInfo> CreateTypeToPhotoThumbnailInfo()
        {
            Dictionary<Type, PhotoThumbnailInfo> map = new Dictionary<Type, PhotoThumbnailInfo>();
            map.Add(typeof(PaintingDto), new PhotoThumbnailInfo(340, 255, "/Upload/Paintings/{0}/Galleries/{1}/", 5));
            map.Add(typeof(UserDto), new PhotoThumbnailInfo(140, 190, "/Upload/Users/{0}/Galleries/{1}/", 1));
            return map;
        }
    }
}
