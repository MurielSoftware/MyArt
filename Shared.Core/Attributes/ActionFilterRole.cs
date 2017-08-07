using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Attributes
{
    /// <summary>
    /// Class for check if the use has the rights to execute the action.
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class ActionFilterRole : ActionFilterAttribute
    //{
    //    private RoleType roleType;

    //    public ActionFilterRole(RoleType roleType)
    //    {
    //        this.roleType = roleType;
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (!UserSessionProvider.IsLoggedUserWithRights(roleType))
    //        {
    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = SharedConstants.CONTROLLER_HOME, action = SharedConstants.VIEW_LOGIN, area = SharedConstants.AREA_ADMIN }));
    //        }
    //    }
    //}
}
