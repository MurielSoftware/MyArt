using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Client.Core.HtmlHelpers
{
    public static class RemoteExtensions
    {
        /// <summary>
        /// Creates the div to load the remote content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ajaxHelper">The ajax helper</param>
        /// <param name="action">The action</param>
        /// <param name="controller">The controller</param>
        /// <param name="area">The area</param>
        /// <param name="routeValues">The route values</param>
        /// <returns>Returns the div to load the remote content</returns>
        public static MvcHtmlString RemoteContent<T>(this AjaxHelper<T> ajaxHelper, string action, string controller, string area = "", object routeValues = null)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("remotecontent");
            tagBuilder.MergeAttribute("data-action", GetAction(action, controller, area, routeValues));
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        private static string GetAction(string action, string controller, string area, object routeValues = null)
        {
            if (!string.IsNullOrEmpty(area))
            {
                return string.Format("/{0}/{1}/{2}?{3}", area, controller, action, GetRouteValuesAsString(routeValues));
            }
            return string.Format("/{0}/{1}?{2}", controller, action, GetRouteValuesAsString(routeValues));
        }

        private static string GetRouteValuesAsString(object routeValues)
        {
            RouteValueDictionary routeValueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            if(routeValueDictionary.Count == 0)
            {
                return string.Empty;
            }
            string routeString = routeValueDictionary
                .Select(s => string.Format("{0}={1}", s.Key, s.Value))
                .Aggregate((current, next) => string.Format("{0}&{1}", current, next));
            return routeString;
            //routeValueDictionary.
            //StringBuilder sb = new StringBuilder("?");
            //foreach (KeyValuePair<string, object> routeValue in routeValueDictionary)
            //{
            //    if (routeValue.Value != null)
            //    {
            //        sb.AppendFormat("{0}={1}&", routeValue.Key, routeValue.Value.ToString());
            //    }
            //}
            //return sb.ToString();
        }
    }
}
