﻿using Shared.Dtos.Paintings;
using Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Galleries
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
            return new Dictionary<Type, PhotoThumbnailInfo>
            {
                { typeof(PaintingDto), new PhotoThumbnailInfo(340, 255, "/Upload/Paintings/{0}/Galleries/{1}/", 5) },
                { typeof(UserDto), new PhotoThumbnailInfo(140, 190, "/Upload/Users/{0}/Galleries/{1}/", 1) }
            };
        }
    }
}
