using Client.Core.AfterSaves;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Json;
using Shared.Dtos.Paintings;
using Shared.Services.Paintings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyArt.Areas.Admin.Controllers
{
    public class PaintingController : WizardCRUDController<PaintingDto, IPaintingCRUDService>
    {
        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(PaintingDto paintingDto)
        {
            return null;
        }

        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            return DoDeleteConfirmed(AfterSuccessSaveParam.Create(dialogDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_PAINTING, null, HtmlConstants.PAGED_LIST_PAINTING));
        }

        public ActionResult PagedList(BaseFilterDto baseFilterDto)
        {
            ViewBag.FilterDto = baseFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(baseFilterDto));
        }

        public ActionResult Details(Guid id)
        {
            return View(GetService().Read(id));
        }

        protected override ActionResult DoNext(PaintingDto paintingDto, int currentStep)
        {
            switch(currentStep)
            {
                case 0:
                    return DoCreate(paintingDto, AfterSuccessSaveParam.Create(paintingDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_PAINTING, null, HtmlConstants.PAGED_LIST_PAINTING, currentStep)); //null, null, null, currentStep.ToString()));
            }
            return null;
        }

        protected override ActionResult RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam afterSuccessSaveParam)
        {
            switch (afterSuccessSaveParam.NextStep)
            {
                case 0:
                    return Json(JsonWizardResult.CreateSuccess(afterSuccessSaveParam.Id, afterSuccessSaveParam.TargetHtmlId, GetNextStep(afterSuccessSaveParam.NextStep), afterSuccessSaveParam.GetAction()));
            }
            return null;
        }

        protected override ActionResult Finish(PaintingDto paintingDto, int currentStep)
        {
            return Json(JsonDialogResult.CreateSuccess(null, null, Url.Action(WebConstants.VIEW_DETAILS, new { id = paintingDto.Id })));
        }

        protected override int GetWizardStepsCount()
        {
            return 2;
        }
    }
}