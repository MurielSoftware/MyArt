using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Core.HtmlHelpers
{
    public static class DivExtensions
    {
        /// <summary>
        /// Creates only labels of the references.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="metadata">The metadata of the current property</param>
        /// <param name="referencies">The referencies contained IDs and labels</param>
        /// <param name="htmlAttributes">The html attributres</param>
        /// <returns>Returns the created texts</returns>
        public static MvcHtmlString CreateDiv<T>(this HtmlHelper<T> htmlHelper, string text, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.InnerHtml = text;
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString CreateSpan<T>(this HtmlHelper<T> htmlHelper, string text, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("span");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.InnerHtml = text;
            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}
