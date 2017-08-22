using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Client.Core.HtmlHelpers
{
    public static class GoogleExtensions
    {
        private static string DEFAULT_COORDINATES = "(50.331436330838834, 14.9798583984375)";

        /// <summary>
        /// The Google map for the appropriate expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The Html helper</param>
        /// <param name="expression">The expression</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>The Google map</returns>
        public static MvcHtmlString GoogleMapFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string gps = metadata.Model == null ? DEFAULT_COORDINATES : metadata.Model.ToString();

            //StringBuilder sb = new StringBuilder();
            //sb.Append(string.Format(@"<div id='map-canvas' {0}></div>", htmlAttributes.ToString()));
            //sb.Append(InputExtensions.Hidden(htmlHelper, metadata.PropertyName, gps));
            //return MvcHtmlString.Create(sb.ToString());

            //TagBuilder hidden = new TagBuilder("input");

            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("id", "map-canvas");
            tagBuilder.InnerHtml = InputExtensions.Hidden(htmlHelper, metadata.PropertyName, gps).ToString();
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));

        }
    }
}
