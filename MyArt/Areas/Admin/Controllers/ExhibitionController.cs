using Client.Core.AfterSaves;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Json;
using Shared.Dtos.Exhibitions;
using Shared.Dtos.Paintings;
using Shared.Services.Exhibitions;
using Shared.Services.Paintings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyArt.Areas.Admin.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ExhibitionController : WizardCRUDController<ExhibitionDto, IExhibitionCRUDService>
    {
        public override ActionResult CreatePredefined(ExhibitionDto precreatedDto)
        {
            if(!Guid.Empty.Equals(precreatedDto.Id))
            {
                precreatedDto = GetService().Read(precreatedDto.Id);
            }
            precreatedDto.PaintingsCheckedDto = GetPaintings(precreatedDto.Id);
            return base.CreatePredefined(precreatedDto);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Details(ExhibitionDto exhibitionDto)
        {
            return base.DoCreate(exhibitionDto, AfterSuccessSaveParam.Create(exhibitionDto, WebConstants.VIEW_DETAILS, WebConstants.CONTROLLER_EXHIBITION, new { id = exhibitionDto.Id}));
        }

        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            return DoDeleteConfirmed(AfterSuccessSaveParam.Create(dialogDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_EXHIBITION, null, HtmlConstants.PAGED_LIST_EXHIBITION));
        }

        public ActionResult PagedList(ExhibitionFilterDto exhibitionFilterDto)
        {
            ViewBag.FilterDto = exhibitionFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(exhibitionFilterDto));
        }

        public ActionResult List(ExhibitionFilterDto exhibitionFilterDto)
        {
            return PartialView(GetService().ReadAdministrationAll(exhibitionFilterDto));
        }


        protected override ActionResult DoNext(ExhibitionDto exhibitionDto, int currentStep)
        {
            switch (currentStep)
            {
                case 0:
                    return RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam.Create(exhibitionDto.Id, null, null, null, null, null, currentStep));
                case 1:
                    return DoCreate(exhibitionDto, AfterSuccessSaveParam.Create(exhibitionDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_EXHIBITION, null, HtmlConstants.PAGED_LIST_EXHIBITION, currentStep)); //null, null, null, currentStep.ToString()));
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

        protected override ActionResult Finish(ExhibitionDto exhibitionDto, int currentStep)
        {
            GetUnitOfWork().StartTransaction();
            GetService().Persist(exhibitionDto);
            GetUnitOfWork().EndTransaction();
            return Json(JsonDialogResult.CreateSuccess(null, null, Url.Action(WebConstants.VIEW_DETAILS, new { id = exhibitionDto.Id })));
        }

        protected override int GetWizardStepsCount()
        {
            return 3;
        }

        private List<PaintingCheckedDto> GetPaintings(Guid exhibitionId)
        {
            IPaintingCRUDService paintingCRUDService = GetServiceManager().Get<IPaintingCRUDService>();
            return paintingCRUDService.ReadCheckedDto(exhibitionId);
        }
    }
}