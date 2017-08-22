﻿using Shared.Core.Services;
using Shared.Dtos.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Utils;
using System.Drawing;
using System.Drawing.Imaging;

namespace Server.Services.Resources
{
    public class PhotoUploadService : BaseResourceUploadService<PhotoResourceDto>
    {
        public PhotoUploadService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override PhotoResourceDto Upload(PhotoResourceDto photoResourceDto)
        {
            PhotoSupportService photoSupportService = new PhotoSupportService(_unitOfWork);
            IOUtils.CreateDirectories(photoResourceDto.GetAbsolutePath());

            Image imageFromStream = Image.FromStream(photoResourceDto.Stream);
            Image image = photoSupportService.Resize(imageFromStream, 1024, 768);
            Image thumbnail = photoSupportService.Resize(imageFromStream, photoResourceDto.PhotoThumbnailInfo.MinimumWidth, photoResourceDto.PhotoThumbnailInfo.MinimumHeight);
            image.Save(photoResourceDto.GetAbsoluteFilePath(), ImageFormat.Jpeg);
            thumbnail.Save(photoResourceDto.GetAbsoluteThumbnailFilePath(), ImageFormat.Jpeg);

            return photoResourceDto;
        }

        public override void Delete(PhotoResourceDto photoResourceDto)
        {
            IOUtils.Delete(photoResourceDto.GetAbsoluteFilePath());
            IOUtils.Delete(photoResourceDto.GetAbsoluteThumbnailFilePath());
            IOUtils.DeleteDirectoryIfNeeded(photoResourceDto.GetAbsolutePath());
        }
    }
}
