using Client.Core.AfterSaves;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Dtos.Collections;
using Shared.Services.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyArt.Areas.Admin.Controllers
{
    public class CollectionController : DialogCRUDController<CollectionDto, ICollectionCRUDService>
    {
        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(CollectionDto collectionDto)
        {
            return DoCreate(collectionDto, AfterSuccessSaveParam.Create(collectionDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_COLLECTION, null, HtmlConstants.PAGED_LIST_COLLECTION));
        }

        public override ActionResult DeleteConfirmed(DialogDto dialogDto)
        {
            return DoDeleteConfirmed(AfterSuccessSaveParam.Create(dialogDto.Id, null, WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_COLLECTION, null, HtmlConstants.PAGED_LIST_COLLECTION));
        }

        public ActionResult PagedList(BaseFilterDto baseFilterDto)
        {
            ViewBag.FilterDto = baseFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(baseFilterDto));
        }

        public ActionResult GetAllCollectionReferences(BaseFilterDto baseFilterDto)
        {
            return Json(GetService().GetAllReferences(baseFilterDto).References.ToList());
        }
    }
}