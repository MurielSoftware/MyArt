using Client.Core.AfterSaves;
using Client.Core.Constants;
using Client.Core.HtmlHelpers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Json;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Client.Core.Controllers
{
    public abstract class DialogCRUDController<T, U> : CRUDController<T, U>
        where T : BaseDto
        where U : ICRUDService<T>
    {
        protected override ActionResult RedirectCreate(T dto)
        {
            return PartialView(dto);
        }

        protected override ActionResult RedirectToActionAfterClientFailCreate(T dto, AfterFailSaveParam afterFailSaveParam)
        {
            return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(ModelState).ToString()));
        }

        protected override ActionResult RedirectToActionAfterServerFailCreate(T dto, AfterFailSaveParam afterFailSaveParam)
        {
            return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, afterFailSaveParam.ValidationMessage));
        }

        protected override ActionResult RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam afterSuccessSaveParam)
        {
            return Json(JsonDialogResult.CreateSuccess(afterSuccessSaveParam.TargetHtmlId, null, afterSuccessSaveParam.GetAction()));
        }
    }
}
