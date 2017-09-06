using Client.Core.Controllers;
using Shared.Core.Dtos.Roles;
using Shared.Services.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Constants;
using Client.Core.AfterSaves;
using Shared.Core.Messages;
using Shared.I18n.Constants;

namespace MyArt.Areas.Admin.Controllers
{
    public class RoleController : DialogCRUDController<RoleDto, IRoleCRUDService>
    {
        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(RoleDto roleDto)
        {
            return DoCreate(roleDto, AfterSuccessSaveParam.Create(roleDto.Id, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_SAVE_SUCCESS_MESSAGE), WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_ROLE, null, HtmlConstants.PAGED_LIST_ROLE));
        }

        public ActionResult DeleteConfirmed(DeletionDto deletionDto)
        {
            return DoDeleteConfirmed(AfterDeleteParam.Create(deletionDto, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_DELETED_SUCCESS_MESSAGE), WebConstants.VIEW_PAGED_LIST, WebConstants.CONTROLLER_ROLE, null, HtmlConstants.PAGED_LIST_ROLE));
        }

        public ActionResult PagedList(BaseFilterDto baseFilterDto)
        {
            ViewBag.FilterDto = baseFilterDto;
            return PartialView(WebConstants.VIEW_PAGED_LIST, GetService().ReadAdministrationPaged(baseFilterDto));
        }

        public ActionResult GetAllRoleReferences(BaseFilterDto baseFilterDto)
        {
            return Json(GetService().GetAllReferences(baseFilterDto).References.ToList());
        }
    }
}