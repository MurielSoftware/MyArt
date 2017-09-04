using Client.Core.AfterSaves;
using Client.Core.Constants;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Json;
using Shared.Dtos.Users;
using Shared.Services.Resources;
using Shared.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyArt.Areas.Admin.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class UserController : WizardCRUDController<UserDto, IUserCRUDService>
    {
        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            return DoDeleteConfirmed(AfterSuccessSaveParam.Create(dialogDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_USER, null, HtmlConstants.PAGED_LIST_USER));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Details(UserDto userDto)
        {
            return base.DoCreate(userDto, AfterSuccessSaveParam.Create(userDto, WebConstants.VIEW_DETAILS, WebConstants.CONTROLLER_USER, new { id = userDto.Id }));
        }

        public ActionResult PagedList(UserFilterDto userFilterDto)
        {
            ViewBag.FilterDto = userFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(userFilterDto));
        }

        public ActionResult GetByPrefix(string prefix)
        {
            return Json(GetService().GetByPrefix(new UserFilterDto() { Surname = prefix }));
        }

        [HttpGet]
        public ActionResult IsEmailUnique(string email)
        {
            return Json(GetService().IsEmailUnique(email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profil(Guid id)
        {
            UserDto userDto = GetService().Read(id);
            GetTempDataManager().SetTempData(TempDataConstants.DTO, userDto);
            return View(userDto);
        }

        protected override ActionResult DoNext(UserDto userDto, int currentStep)
        {
            switch (currentStep)
            {
                case 0:
                    return RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam.Create(userDto.Id, null, null, null, null, null, currentStep));
                case 1:
                    return DoCreate(userDto, AfterSuccessSaveParam.Create(userDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_USER, null, HtmlConstants.PAGED_LIST_USER, currentStep)); //null, null, null, currentStep.ToString()));
            }
            return null;
        }

        protected override ActionResult RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam afterSuccessSaveParam)
        {
            switch (afterSuccessSaveParam.NextStep)
            {
                case 0:
                case 1:
                    return Json(JsonWizardResult.CreateSuccess(afterSuccessSaveParam.Id, afterSuccessSaveParam.TargetHtmlId, GetNextStep(afterSuccessSaveParam.NextStep), afterSuccessSaveParam.GetAction()));
            }
            return View(afterSuccessSaveParam.Model);
        }

        protected override ActionResult Finish(UserDto userDto, int currentStep)
        {
            IPhotoCRUDService photoCRUDService = GetServiceManager().Get<IPhotoCRUDService>();
            photoCRUDService.Crop(userDto.PhotoCropDto);
            return Json(JsonDialogResult.CreateSuccess(null, null, Url.Action(WebConstants.VIEW_PROFILE, new { id = userDto.Id })));
        }

        protected override int GetWizardStepsCount()
        {
            return 2;
        }
    }
}