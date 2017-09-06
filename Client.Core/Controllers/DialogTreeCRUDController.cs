using Client.Core.AfterSaves;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Dtos.MenuItems;
using Shared.Core.Json;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Core.Controllers
{
    public abstract class DialogTreeCRUDController<T, U> : DialogCRUDController<T, U>
        where T : BaseDto
        where U : ITreeCRUDService<T>
    {
        protected override ActionResult RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam afterSuccessSaveParam)
        {
            Guid? parentId = GetService().GetParentId(afterSuccessSaveParam.Id);
            if(parentId.HasValue)
            {
                return Json(JsonTreeResult.CreateTreeSuccess(parentId.Value, afterSuccessSaveParam.TargetHtmlId, null, afterSuccessSaveParam.Action));
                //return base.RedirectToActionAfterSuccessCreate(id, actionName, controllerName, routeValues, parentId.Value.ToString(), JsonRefreshMode.REFRESH_TREE_AFTER_DIALOG_CLOSE);
            }
            return base.RedirectToActionAfterSuccessCreate(afterSuccessSaveParam);
        }

        protected override ActionResult RedirectToActionAfterSuccessDelete(AfterDeleteParam afterDeleteParam)
        {
            MenuDeletionDto menuDeletionDto = (MenuDeletionDto)afterDeleteParam.DeletionDto;
            if (menuDeletionDto.ParentMenuItemId.HasValue)
            {
                return Json(JsonTreeResult.CreateTreeSuccess(menuDeletionDto.ParentMenuItemId.Value, afterDeleteParam.TargetHtml, null, afterDeleteParam.Action));
            }

            //if(afterDeleteParam.DeletionDto == null)
            return Json(JsonTreeResult.CreateTreeSuccess(null, afterDeleteParam.TargetHtml, null, afterDeleteParam.Action));
            //if (affectedId.HasValue)
            //{
            //    return base.RedirectToActionAfterSuccessDelete(affectedId, affectedId.ToString(), action, routeValues, JsonRefreshMode.TREE);
            //}
            //return base.RedirectToActionAfterSuccessDelete(affectedId, targetId, action, routeValues, JsonRefreshMode.FULL);
            //if(!Guid.Empty.Equals(id))
            //{
            //    return RedirectToActionAfterSuccessDelete(id, actionName, controllerName, targetId, routeValues);
            //}
            //return RedirectToActionAfterSuccessDelete(id, actionName, controllerName, targetId, routeValues);
        }

        public ActionResult TreeNodeChangePosition(Guid sourceId, Guid targetId)
        {
            GetService().TreeNodeChangePosition(sourceId, targetId);
            return RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam.Create(sourceId, null, WebConstants.VIEW_LIST, null, null, HtmlConstants.TREE_MENU_ITEM));
            //return RedirectToActionAfterSuccessCreate(sourceId, WebConstants.VIEW_LIST, null, null, HtmlConstants.TREE_MENU_ITEM);
        }
    }
}
