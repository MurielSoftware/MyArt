
using Server.Daos;
using Server.Model;
using Shared.Core.Context;
using Shared.Core.Dtos.Resources;
using Shared.Core.Exceptions;
using Shared.I18n.Constants;
using Shared.Services.Galleries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Resources
{
    public class PhotoValidationService
    {
        private const int MAXIMUM_PHOTO_FILE_SIZE = 6000;

        private GenericDao _genericDao;

        public PhotoValidationService(IUnitOfWork unitOfWork)
        {
            _genericDao = new GenericDao(unitOfWork);
        }

        public IList<ValidationResult> CollectBeforePersistValidationResults(PhotoResourceDto photoResourceDto)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            validationResults.AddRange(IsFileOverlimit(photoResourceDto));
            validationResults.AddRange(IsMaximumPhotoLimitReached(photoResourceDto));
            validationResults.AddRange(IsImage(photoResourceDto));
            return validationResults;
        }

        private IList<ValidationResult> IsFileOverlimit(PhotoResourceDto photoResourceDto)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            if (MAXIMUM_PHOTO_FILE_SIZE < photoResourceDto.Stream.Length / 1024)
            {
                validationResults.Add(new ValidationResult(MessageKeyConstants.VALIDATION_FILE_IS_OVERSIZED_MESSAGE, photoResourceDto.Name, MAXIMUM_PHOTO_FILE_SIZE / 1024));
            }
            return validationResults;
        }

        private IList<ValidationResult> IsMaximumPhotoLimitReached(PhotoResourceDto photoResourceDto)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            int maximumPhotoLimit = PhotoThumbnailInfoProvider.GetDefault(photoResourceDto.OwnerType).MaximumPhotos;
            int currentPhotoCount = _genericDao.Count<Resource>(x => x.UserDefinableId == photoResourceDto.UserDefinableId);
            if(currentPhotoCount >= maximumPhotoLimit)
            {
                validationResults.Add(new ValidationResult(MessageKeyConstants.VALIDATION_MAXIMUM_RESOURCES_REACHED_MESSAGE, maximumPhotoLimit));
            }
            return validationResults;
        }

        private IList<ValidationResult> IsImage(PhotoResourceDto photoResourceDto)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            try
            {
                using (Image image = Image.FromStream(photoResourceDto.Stream, false, false))
                {
                    PhotoThumbnailInfo photoThumbnailInfo = PhotoThumbnailInfoProvider.GetDefault(photoResourceDto.OwnerType);
                    if(photoThumbnailInfo.MinimumHeight < image.Height || photoThumbnailInfo.MinimumWidth < image.Width)
                    {
                        validationResults.Add(new ValidationResult(MessageKeyConstants.VALIDATION_IMAGE_MINIMUM_SIZE_MESSAGE, image.Width, image.Height, photoThumbnailInfo.MinimumWidth, photoThumbnailInfo.MinimumHeight));
                    }
                    return validationResults;
                }
            }
            catch
            {
                validationResults.Add(new ValidationResult(MessageKeyConstants.VALIDATION_FILE_IS_IN_WRONG_FORMAT_MESSAGE, "Image"));
            }
            return validationResults;
        }
    }
}
