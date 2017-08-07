using Shared.I18n.Constants;
using Shared.I18n.Utils;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Client.Core.HtmlHelpers
{
    public static class EditableControlExtensions
    {
        /// <summary>
        /// Editable label where you can click and it will be possible to edit it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expression">The expression</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>Returns the editable label</returns>
        public static MvcHtmlString EditableLabelFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string textDoDisplay = modelMetadata.Model == null ? " " : modelMetadata.Model.ToString();
            if (modelMetadata.Model != null && !string.IsNullOrEmpty(modelMetadata.DisplayFormatString))
            {
                textDoDisplay = string.Format(modelMetadata.DisplayFormatString, modelMetadata.Model);
            }
            return CreateLabel<T>(htmlHelper, modelMetadata.PropertyName, textDoDisplay, htmlAttributes);
        }

        /// <summary>
        /// Editable checkbox label where the use can click at it and edit the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expression">The expression</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>Returns the editable check box label</returns>
        public static MvcHtmlString EditableCheckLabelFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string value = string.Empty;

            if (modelMetadata.Model != null)
            {
                value = ((bool)modelMetadata.Model == true) ? ResourceUtils.GetString(MessageKeyConstants.LABEL_YES) : ResourceUtils.GetString(MessageKeyConstants.LABEL_NO);
            }

            return CreateLabel<T>(htmlHelper, modelMetadata.PropertyName, value, htmlAttributes);
        }

        /// <summary>
        /// Creates the inline editable control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="detailControl">The control which will be displayed in the non-editable mode</param>
        /// <param name="editControl">The control which will be displayed in the editable mode</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>Returns the editable control</returns>
        public static MvcHtmlString InlineEditableControl<T>(this HtmlHelper<T> htmlHelper, object detailControl, object editControl, object htmlAttributes = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<i class='fa fa-pencil form-control-editable-icon'></i>");
            sb.Append(detailControl.ToString());
            sb.Append("<div style='display:none'>");
            sb.Append(editControl.ToString());
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString CreateLabel<T>(this HtmlHelper<T> htmlHelper, string propertyName, string text, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("name", propertyName);
            tagBuilder.InnerHtml = text;
            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}
