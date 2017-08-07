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
        public override ActionResult Create(Guid? id)
        {
            if (!id.HasValue)
            {
                SaveToTempAndView(TempDataConstants.PRECREATED_DTO);
                if (ViewData[TempDataConstants.PRECREATED_DTO] == null)
                {
                    return PartialView(Activator.CreateInstance<T>());
                }
                if (!(ViewData[TempDataConstants.PRECREATED_DTO] is T))
                {
                    return PartialView(Activator.CreateInstance<T>());
                }
                return PartialView(ViewData[TempDataConstants.PRECREATED_DTO]);
            }
            T entity = GetService().Read(id.Value);
            return PartialView(entity);
        }

        protected override ActionResult RedirectToActionAfterClientFailCreate(T dto, string validationResult)
        {
            return Json(new JsonDialogResult(false, HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(ModelState).ToString()));
        }

        protected override ActionResult RedirectToActionAfterServerFailCreate(T dto, string validationResult)
        {
            return Json(new JsonDialogResult(false, HtmlConstants.DIALOG_VALIDATION_SUMMARY, validationResult));
        }

        protected override ActionResult RedirectToActionAfterSuccessCreate(Guid id, string actionName, string controllerName, object routeValues, string targetHtmlId, JsonRefreshMode refreshMode)
        {
            return Json(new JsonDialogResult(true, targetHtmlId, Url.Action(actionName, controllerName, routeValues), JsonRefreshMode.FULL));
        }
    }
}
