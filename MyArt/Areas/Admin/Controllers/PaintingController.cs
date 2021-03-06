﻿using Client.Core.AfterSaves;
using Client.Core.Constants;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Json;
using Shared.Core.Messages;
using Shared.Dtos.Paintings;
using Shared.I18n.Constants;
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
    public class PaintingController : WizardCRUDController<PaintingDto, IPaintingCRUDService>
    {
        public ActionResult DeleteConfirmed(DeletionDto deletionDto)
        {
            return DoDeleteConfirmed(AfterDeleteParam.Create(deletionDto, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_DELETED_SUCCESS_MESSAGE), WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_PAINTING, null, HtmlConstants.PAGED_LIST_PAINTING));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Details(PaintingDto paintingDto)
        {
            return base.DoCreate(paintingDto, AfterSuccessSaveParam.Create(paintingDto, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_SAVE_SUCCESS_MESSAGE), WebConstants.VIEW_DETAILS, WebConstants.CONTROLLER_PAINTING, new { id = paintingDto.Id }));
        }

        public ActionResult PagedList(PaintingFilterDto paintingFilterDto)
        {
            ViewBag.FilterDto = paintingFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(paintingFilterDto));
        }

        public ActionResult List(PaintingFilterDto paintingFilterDto)
        {
            return PartialView(GetService().ReadAdministrationAll(paintingFilterDto));
        }

        protected override ActionResult DoNext(PaintingDto paintingDto, int currentStep)
        {
            switch(currentStep)
            {
                case 0:
                    return RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam.Create(paintingDto.Id, null, null, null, null, null, currentStep));
                case 1:
                    return DoCreate(paintingDto, AfterSuccessSaveParam.Create(paintingDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_PAINTING, null, HtmlConstants.PAGED_LIST_PAINTING, currentStep)); //null, null, null, currentStep.ToString()));
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
            return View(afterSuccessSaveParam.Action, afterSuccessSaveParam.Dto);
        }

        protected override ActionResult Finish(PaintingDto paintingDto, int currentStep)
        {
            return Json(JsonDialogResult.CreateSuccess(null, null, Url.Action(WebConstants.VIEW_DETAILS, new { id = paintingDto.Id })));
        }

        protected override int GetWizardStepsCount()
        {
            return 3;
        }
    }
}