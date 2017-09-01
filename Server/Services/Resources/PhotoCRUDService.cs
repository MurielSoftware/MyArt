using Server.Model;
using Shared.Dtos.Resources;
using Shared.Services.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Services;
using Shared.Core.Dtos;
using System.Drawing;
using Shared.Core.Dtos.Resources;
using Shared.Services.Galleries;

namespace Server.Services.Resources
{
    public class PhotoCRUDService : BaseResourceCRUDService<PhotoResourceDto, Resource>, IPhotoCRUDService
    {
        public PhotoCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override BaseResourceUploadService<PhotoResourceDto> GetUploadResourceService()
        {
            return new PhotoUploadService(_unitOfWork);
        }

        public override PhotoResourceDto Persist(PhotoResourceDto photoResourceDto)
        {
            Gallery gallery = null;
            bool automaticCoverPhotoSet = false;
            if(Guid.Empty.Equals(photoResourceDto.UserDefinableId))
            {
                gallery = CreateAndPersistEmptyProfileGallery();
                gallery.OwnerId = photoResourceDto.UserDefinableOwnerId;
                photoResourceDto.UserDefinableId = gallery.Id;
                automaticCoverPhotoSet = true;
            }
            PhotoThumbnailInfo photoThumbnailInfo = PhotoThumbnailInfoProvider.GetDefault(photoResourceDto.OwnerType);
            photoResourceDto.Path = string.Format(photoThumbnailInfo.Path, photoResourceDto.UserDefinableOwnerId, photoResourceDto.UserDefinableId);
            photoResourceDto = base.Persist(photoResourceDto);
            if(automaticCoverPhotoSet)
            {
                gallery.CoverPhotoId = photoResourceDto.Id;
                _genericDao.Persist<Gallery>(gallery);
            }
            return photoResourceDto;
        }

        public void Crop(PhotoCropDto photoCropDto)
        {
            PhotoSupportService photoSupportService = new PhotoSupportService(_unitOfWork);
            PhotoResourceDto photoResourceDto = Read(photoCropDto.Id);
            if (photoResourceDto != null)
            {
                photoSupportService.Crop(photoCropDto);
            }
        }

        public IList<PhotoResourceDto> ReadAdministrationAll(ResourceFilterDto resourceFilterDto)
        {
            return _resourceDao.FindAll(resourceFilterDto);
        }

        public override void Delete(Guid id)
        {
            Resource resource = _genericDao.Find<Resource>(id);
            Gallery gallery = _genericDao.Find<Gallery>(resource.UserDefinableId);
            SetNewCoverPhotoIfNecessarry(resource, gallery);
            base.Delete(id);
            RemoveGalleryIfNecessarry(resource, gallery);
        }

        private Gallery CreateAndPersistEmptyProfileGallery()
        {
            Gallery gallery = new ProfileGallery()
            {
                Name = Guid.NewGuid().ToString()
            };
            return _genericDao.Persist<Gallery>(gallery);
        }

        private void SetNewCoverPhotoIfNecessarry(Resource resource, Gallery gallery)
        {
            Guid? coverPhotoId = null;
            if (resource.Id.Equals(gallery.CoverPhotoId))
            {
                if (gallery.Resources.Count > 1)
                {
                    coverPhotoId = gallery.Resources.Where(x => x.Id != resource.Id).FirstOrDefault().Id;
                }
                else
                {
                    coverPhotoId = null;
                }
                gallery.CoverPhotoId = coverPhotoId;
                _genericDao.Persist<Gallery>(gallery);
            }
        }

        private void RemoveGalleryIfNecessarry(Resource resource, Gallery gallery)
        {
            if(gallery.Resources.Count == 0)
            {
                _genericDao.Delete<Gallery>(gallery);
            }
        }

        //private void SetCoverPhoto(Guid? newCoverPhotoId, Guid galleryId)
        //{
        //    Gallery gallery = _genericDao.Find<Gallery>(galleryId);
        //    gallery.CoverPhotoId = newCoverPhotoId;
        //    _genericDao.Persist<Gallery>(gallery);
        //}
    }
}
