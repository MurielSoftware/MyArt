using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Client.Core.HtmlHelpers
{
    public static class DateExtensions
    {
        /// <summary>
        /// Creates the editable label with the date picker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expression">The expression with the date</param>
        /// <param name="htmlAttributes">The hml attributes</param>
        /// <returns>Returns the editable label with date picker</returns>
        public static MvcHtmlString DateFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='input-group date'>");
            sb.Append(InputExtensions.TextBoxFor(htmlHelper, expression, "{0:dd.MM.yyyy}", new { @class = "form-control", @data_provide = "datepicker", @data_date_today_highlight = "true", @data_date_format = "dd.mm.yyyy" }));
            sb.Append(@"<span class='input-group-addon'><i class='fa fa-calendar'></i></span>");
            sb.Append(@"</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Creates the editable date picker range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expressionFrom">The expression from date</param>
        /// <param name="expressionTo">The expression to date</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>Returns the date range picker</returns>
        public static MvcHtmlString DateRangeFor<T, U, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expressionFrom, Expression<Func<T, V>> expressionTo, object htmlAttributes = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='input-group input-daterange' data-provide='datepicker' data-date-today-highlight='true' data-date-format='dd.mm.yyyy'>");
            sb.Append(InputExtensions.TextBoxFor(htmlHelper, expressionFrom, "{0:dd.MM.yyyy}", new { @class = "form-control" }));
            sb.Append(@"<span class='input-group-addon'>-</span>");
            sb.Append(InputExtensions.TextBoxFor(htmlHelper, expressionTo, "{0:dd.MM.yyyy}", new { @class = "form-control" }));
            sb.Append(@"</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Editable label with daterange label.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expressionStart">The expression of the start date</param>
        /// <param name="expressionEnd">The expression of the end date</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns></returns>
        public static MvcHtmlString EditableDateRangeLabelFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expressionStart, Expression<Func<T, U>> expressionEnd, object htmlAttributes = null)
        {
            ModelMetadata modelMetadataStart = ModelMetadata.FromLambdaExpression(expressionStart, htmlHelper.ViewData);
            ModelMetadata modelMetadataEnd = ModelMetadata.FromLambdaExpression(expressionEnd, htmlHelper.ViewData);
            string textToDisplay = string.Empty;

            if (modelMetadataStart.Model != null && modelMetadataEnd.Model != null)
            {
                textToDisplay = ((DateTime)modelMetadataStart.Model).ToShortDateString() + " - " + ((DateTime)modelMetadataEnd.Model).ToShortDateString();
            }

            return DivExtensions.CreateSpan(htmlHelper, textToDisplay, htmlAttributes);
            //return ExtensionsUtil.CreateLabel(modelMetadataStart.PropertyName, textToDisplay, htmlAttributes);
        }

        /// <summary>
        /// Displays the label for time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expression">The expression represents the time</param>
        /// <returns>Returns the time label</returns>
        public static MvcHtmlString DisplayTimeFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (!(metadata.Model is DateTime))
            {
                return MvcHtmlString.Create(string.Empty);
            }
            return MvcHtmlString.Create(((DateTime)metadata.Model).ToShortTimeString());
        }
    }
}
