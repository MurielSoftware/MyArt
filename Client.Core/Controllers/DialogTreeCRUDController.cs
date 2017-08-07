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

namespace Client.Core.Controllers
{
    public abstract class DialogTreeCRUDController<T, U> : DialogCRUDController<T, U>
        where T : BaseDto
        where U : ITreeCRUDService<T>
    {
        protected override ActionResult RedirectToActionAfterSuccessCreate(Guid id, string actionName, string controllerName, object routeValues, string targetHtmlId, JsonRefreshMode refreshMode)
        {
            Guid? parentId = GetService().GetParentId(id);
            if(parentId.HasValue)
            {
                return base.RedirectToActionAfterSuccessCreate(id, actionName, controllerName, routeValues, parentId.Value.ToString(), JsonRefreshMode.TREE);
            }
            return base.RedirectToActionAfterSuccessCreate(id, actionName, controllerName, routeValues, targetHtmlId, JsonRefreshMode.FULL);
        }

        protected override ActionResult RedirectToActionAfterSuccessDelete(Guid id, string actionName, string controllerName, string targetId, object routeValues, JsonRefreshMode refreshMode)
        {
            if(!Guid.Empty.Equals(id))
            {
                return RedirectToActionAfterSuccessDelete(id, actionName, controllerName, targetId, routeValues, JsonRefreshMode.TREE);
            }
            return RedirectToActionAfterSuccessDelete(id, actionName, controllerName, targetId, routeValues, JsonRefreshMode.FULL);
        }

        public ActionResult TreeNodeChangePosition(Guid sourceId, Guid targetId)
        {
            GetService().TreeNodeChangePosition(sourceId, targetId);
            return RedirectToActionAfterSuccessCreate(sourceId, WebConstants.VIEW_LIST, null, null, HtmlConstants.TREE_MENU_ITEM, JsonRefreshMode.FULL);
        }
    }
}
