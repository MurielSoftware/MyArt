using Client.Core.Controllers;
using Shared.Dtos.Resources;
using Shared.Services.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared.Core.Dtos;
using System.IO;
using Shared.Core.Exceptions;
using Client.Core.HtmlHelpers;
using Shared.Core.Constants;
using Shared.Core.Json;
using Client.Core.Constants;

namespace MyArt.Areas.Admin.Controllers
{
    public class PhotoController : CRUDController<PhotoResourceDto, IPhotoCRUDService>
    {
        public ActionResult Upload(PhotoResourceDto photoResourceDto, HttpPostedFileBase file)
        {
            try
            {
                PhotoResourceDto extendedPhotoResourceDto = CreatePhotoResourceDto(photoResourceDto, file);
                GetUnitOfWork().StartTransaction();
                extendedPhotoResourceDto = GetService().Persist(extendedPhotoResourceDto);
                GetUnitOfWork().EndTransaction();
                return Json(JsonUploadResult.CreateSuccess(extendedPhotoResourceDto.Id, null, JsonRefreshMode.NONE, extendedPhotoResourceDto.GetAbsoluteFilePath(), extendedPhotoResourceDto.GetRelativeFilePath()));
              //  return Json(JsonDialogResult.CreateSuccess(extendedPhotoResourceDto.Id, extendedPhotoResourceDto.GetRelativeThumbnailFile()));
            }
            catch(ValidationException ex)
            {
                return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(ex.Message).ToString()));
            }
        }

        public override ActionResult Create(PhotoResourceDto photoResourceDto)
        {
            //GetUnitOfWork().StartTransaction();
            //GetService().Persist(photoResourceDto);
            //GetUnitOfWork().EndTransaction();
            return null;
        }

        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            GetUnitOfWork().StartTransaction();
            GetService().Delete(dialogDto.Id);
            GetUnitOfWork().EndTransaction();

            return Json(JsonDialogResult.CreateSuccess(dialogDto.Id, null));
        }

        private PhotoResourceDto CreatePhotoResourceDto(PhotoResourceDto photoResourceDto, HttpPostedFileBase file)
        {
            IPhotoResourcableDto photoResourcableDto = ((IPhotoResourcableDto)GetFromTemp(TempDataConstants.DTO));
            PhotoResourceDto extendedPhotoResourceDto = photoResourcableDto.PhotoResourceDto;
            extendedPhotoResourceDto.OriginalName = file.FileName;
            extendedPhotoResourceDto.Stream = file.InputStream;
            extendedPhotoResourceDto.Extension = Path.GetExtension(file.FileName);
            extendedPhotoResourceDto.UserDefinableOwnerId = photoResourcableDto.Id;
            return extendedPhotoResourceDto;
        }
    }
}