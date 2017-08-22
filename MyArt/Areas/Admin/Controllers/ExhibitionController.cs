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
            precreatedDto.PaintingsCheckedDto = GetPaintings();
            return base.CreatePredefined(precreatedDto);
        }

        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(ExhibitionDto dto)
        {
            return null;
        }

        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            return DoDeleteConfirmed(AfterSuccessSaveParam.Create(dialogDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_EXHIBITION, null, HtmlConstants.PAGED_LIST_EXHIBITION));
        }

        public ActionResult Details(Guid id)
        {
            return View(GetService().Read(id));
        }

        public ActionResult PagedList(BaseFilterDto baseFilterDto)
        {
            ViewBag.FilterDto = baseFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(baseFilterDto));
        }

        protected override ActionResult DoNext(ExhibitionDto exhibitionDto, int currentStep)
        {
            switch (currentStep)
            {
                case 0:
                    return DoCreate(exhibitionDto, AfterSuccessSaveParam.Create(exhibitionDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_EXHIBITION, null, HtmlConstants.PAGED_LIST_EXHIBITION, currentStep)); //null, null, null, currentStep.ToString()));
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

        protected override ActionResult Finish(ExhibitionDto exhibitionDto, int currentStep)
        {
            GetUnitOfWork().StartTransaction();
            GetService().Persist(exhibitionDto);
            GetUnitOfWork().EndTransaction();
            return Json(JsonDialogResult.CreateSuccess(null, null, Url.Action(WebConstants.VIEW_DETAILS, new { id = exhibitionDto.Id })));
        }

        protected override int GetWizardStepsCount()
        {
            return 2;
        }

        private List<PaintingCheckedDto> GetPaintings()
        {
            IPaintingCRUDService paintingCRUDService = GetServiceManager().Get<IPaintingCRUDService>();
            return paintingCRUDService.ReadCheckedDto();
        }
    }
}