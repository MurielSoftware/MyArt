using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Core.AfterSaves
{
    public abstract class BaseAfterParam
    {
        public virtual string Controller { get; private set; }
        public virtual string Action { get; private set; }
        public virtual object RouteValues { get; private set; }

        public BaseAfterParam(string action, string controller, object routeValues)
        {
            Action = action;
            Controller = controller;
            RouteValues = routeValues;
        }

        public string GetAction()
        {
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action(Action, Controller, RouteValues);
        }
    }
}
