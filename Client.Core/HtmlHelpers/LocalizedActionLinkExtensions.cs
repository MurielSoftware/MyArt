using Shared.Core.Dtos;
using Shared.Core.Dtos.References;
using Shared.I18n.Utils;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Client.Core.HtmlHelpers
{
    public static class LocalizedActionLinkExtensions
    {
        public static MvcHtmlString LocalizedActionLinkFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, string action = "#", string controller = "", object routeValues = null, object htmlAttributes = null)
        {
            ModelMetadata expressionModelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (expressionModelMetadata.Model == null)
            {
                return null;
            }
            string resourceKey = expressionModelMetadata.Model.ToString();
            if (expressionModelMetadata.Model is ReferenceString)
            {
                resourceKey = (expressionModelMetadata.Model as ReferenceString).GetValue();
            }
            return LocalizedActionLink(htmlHelper, resourceKey, action, controller, routeValues, htmlAttributes);
        }

        public static MvcHtmlString LocalizedActionLink<T>(this HtmlHelper<T> htmlHelper, string resourceKey, string action = "#", string controller = "", object routeValues = null, object htmlAttributes = null)
        {
            string tempName = Guid.NewGuid().ToString();
            if (routeValues == null && action != "Index" && action != "#")
            {
                action += "/";
            }
            string actionLinkMvcHtmlString = LinkExtensions.ActionLink(htmlHelper, tempName, action, controller, routeValues, htmlAttributes).ToHtmlString();
            actionLinkMvcHtmlString = actionLinkMvcHtmlString.Replace(tempName, ResourceUtils.GetString(resourceKey));
            return MvcHtmlString.Create(actionLinkMvcHtmlString);
        }

        /// <summary>
        /// Creates the localized link for open the dialog.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="resourceKey">The resource key to be localized</param>
        /// <param name="targetDialog">The html id of the dialog to be opened</param>
        /// <param name="action">The action to execute</param>
        /// <param name="controller">The controller of the action</param>
        /// <param name="routeValues">The route values for the action</param>
        /// <param name="htmlAttributes">The additional html attributes for the link</param>
        /// <returns></returns>
        public static MvcHtmlString DialogActionLink<T>(this HtmlHelper<T> htmlHelper, string resourceKey, string targetDialog, string action, string controller, object routeValues = null, object htmlAttributes = null)
        {
            string tempName = Guid.NewGuid().ToString();
            RouteValueDictionary attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-toggle", "modal");
            attributes.Add("data-target", targetDialog);
            attributes.Add("data-placement", "top");
            attributes["class"] += " show-dialog tooltips";
            if (attributes.ContainsKey("title"))
            {
                attributes["title"] = ResourceUtils.GetString(attributes["title"].ToString());
            }
            return MvcHtmlString.Create(LinkExtensions.ActionLink(htmlHelper, tempName, action, controller, HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues), attributes).ToHtmlString().Replace(tempName, ResourceUtils.GetString(resourceKey)));
        }
    }
}
