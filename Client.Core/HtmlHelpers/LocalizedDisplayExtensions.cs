using Shared.Core.Dtos;
using Shared.I18n.Utils;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Client.Core.HtmlHelpers
{
    public static class LocalizedDisplayExtensions
    {
        public static MvcHtmlString LocalizedDisplayFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, string defaultValue = null)
        {
            ModelMetadata expressionModelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (expressionModelMetadata.Model == null)
            {
                return MvcHtmlString.Create(defaultValue);
            }
            if (expressionModelMetadata.Model is ReferenceString)
            {
                ReferenceString referenceString = expressionModelMetadata.Model as ReferenceString;
                return MvcHtmlString.Create(referenceString.GetValue());
            }
            return DisplayExtensions.DisplayFor(htmlHelper, expression);
        }

        public static MvcHtmlString LocalizedDisplay<T>(this HtmlHelper<T> htmlHelper, string resourceKey, string defaultValue = null)
        {
            if(string.IsNullOrEmpty(resourceKey))
            {
                return MvcHtmlString.Create(defaultValue);
            }
            return MvcHtmlString.Create(ResourceUtils.GetString(resourceKey));
        }
    }
}
