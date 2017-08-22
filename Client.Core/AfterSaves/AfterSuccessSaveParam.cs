using Shared.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Core.AfterSaves
{
    public class AfterSuccessSaveParam
    {
        public virtual string Controller { get; private set; }
        public virtual string Action { get; private set; }
        public virtual object RouteValues { get; private set; }
        public virtual Guid Id { get; set; }
        public virtual string TargetHtmlId { get; private set; }
        public virtual Message Message { get; private set; }
        public virtual int NextStep { get; set; }

        private AfterSuccessSaveParam(Guid id, Message message, string action, string controller, object routeValues, string targetHtmlId, int nextStep)
        {
            Id = id;
            Message = message;
            Action = action;
            Controller = controller;
            RouteValues = routeValues;
            TargetHtmlId = targetHtmlId;
            NextStep = nextStep;
        }

        private AfterSuccessSaveParam(string action, string controller, object routeValues, string targetHtmlId)
        {
            Action = action;
            Controller = controller;
            RouteValues = routeValues;
            TargetHtmlId = targetHtmlId;
        }

        public string GetAction()
        {
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action(Action, Controller, RouteValues);
        }

        public static AfterSuccessSaveParam Create(string action, string controller = null, object routeValues = null, string targetHtmlId = null)
        {
            return new AfterSuccessSaveParam(action, controller, routeValues, targetHtmlId);
        }

        public static AfterSuccessSaveParam Create(Guid id, Message message, string action, string controller = null, object routeValues = null, string targetHtmlId = null, int nextStep = 0)
        {
            return new AfterSuccessSaveParam(id, message, action, controller, routeValues, targetHtmlId, nextStep);
        }
    }
}
