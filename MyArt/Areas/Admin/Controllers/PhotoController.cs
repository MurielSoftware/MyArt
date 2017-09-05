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
using Shared.Core.Dtos.Resources;

namespace MyArt.Areas.Admin.Controllers
{
    public class PhotoController : CRUDController<PhotoResourceDto, IPhotoCRUDService>
    {
        public ActionResult UploadDialog(Guid userDefinableOwnerId, string userDefinableOwnerType)
        {
            BaseDto dto = (BaseDto)Activator.CreateInstance(Type.GetType(userDefinableOwnerType +", Shared"));
            dto.Id = userDefinableOwnerId;
            GetTempDataManager().SetTempData(TempDataConstants.DTO, dto);
            return PartialView(GetService().ReadAdministrationAll(new ResourceFilterDto() { UserDefinableOwnerId = userDefinableOwnerId }));
        }

        public ActionResult Upload(PhotoResourceDto photoResourceDto, string returnFileType, HttpPostedFileBase file)
        {
            try
            {
                PhotoResourceDto extendedPhotoResourceDto = CreatePhotoResourceDto(file);
                GetUnitOfWork().StartTransaction();
                extendedPhotoResourceDto = GetService().Persist(extendedPhotoResourceDto);
                GetUnitOfWork().EndTransaction();
                if("thumbnail".Equals(returnFileType))
                {
                    return Json(JsonUploadResult.CreateSuccess(extendedPhotoResourceDto.Id, null, JsonRefreshMode.NONE, extendedPhotoResourceDto.GetAbsoluteThumbnailFilePath(), extendedPhotoResourceDto.GetRelativeThumbnailFile()));
                }
                return Json(JsonUploadResult.CreateSuccess(extendedPhotoResourceDto.Id, null, JsonRefreshMode.NONE, extendedPhotoResourceDto.GetAbsoluteFilePath(), extendedPhotoResourceDto.GetRelativeFilePath()));
            }
            catch(ValidationException ex)
            {
                return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(ex.Message).ToString()));
            }
        }
        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(PhotoResourceDto photoResourceDto)
        {
            //IPhotoResourcableDto photoResourcableDto = GetTempDataManager().GetTempDataWithoutRemove<IPhotoResourcableDto>(TempDataConstants.DTO);//((IPhotoResourcableDto)GetFromTemp(TempDataConstants.DTO));
            return Json(JsonDialogResult.CreateSuccess(null, null, Request.UrlReferrer.ToString()));
        }

        public ActionResult DeleteConfirmed(DeletionDto deletionDto)
        {
            GetUnitOfWork().StartTransaction();
            GetService().Delete(deletionDto);
            GetUnitOfWork().EndTransaction();

            return Json(JsonDialogResult.CreateSuccess(deletionDto.Id, null));
        }

        public ActionResult List(ResourceFilterDto resourceFilterDto)
        {
            return PartialView(GetService().ReadAdministrationAll(resourceFilterDto));
        }

        public ActionResult Profil(ResourceFilterDto resourceFilterDto)
        {
            return PartialView(GetService().ReadAdministrationAll(resourceFilterDto));
        }

        private PhotoResourceDto CreatePhotoResourceDto(HttpPostedFileBase file)
        {
            IPhotoResourcableDto photoResourcableDto = GetTempDataManager().GetTempDataWithoutRemove<IPhotoResourcableDto>(TempDataConstants.DTO);//((IPhotoResourcableDto)GetFromTemp(TempDataConstants.DTO));
            PhotoResourceDto extendedPhotoResourceDto = new PhotoResourceDto()
            {
                OwnerType = photoResourcableDto.GetType(),
                OriginalName = file.FileName,
                Stream = file.InputStream,
                Extension = Path.GetExtension(file.FileName),
                UserDefinableOwnerId = photoResourcableDto.Id
            };
            return extendedPhotoResourceDto;
        }
    }
}