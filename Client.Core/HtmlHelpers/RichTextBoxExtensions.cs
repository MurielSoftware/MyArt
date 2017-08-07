using Shared.Core.Constants;
using Shared.Core.Dtos;
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
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// Creates the RichTextBox component include the toolbar.
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <typeparam name="U">The property of the main language</typeparam>
        /// <param name="htmlHelper">The HtmlHelper</param>
        /// <param name="expression">The expression for the property of the main language</param>
        /// <returns>The RichTextBox component</returns>
        public static MvcHtmlString RichTextBoxFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, bool imageSupport = true)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "richtextbox", data_save_action = "/Admin/" + controllerName + "/Create" }));
            tagBuilder.InnerHtml = CreateToolbar(htmlHelper, metadata, imageSupport) + CreateTextArea<T, U>(htmlHelper, metadata, expression);
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        private static string CreateToolbar<T>(this HtmlHelper<T> htmlHelper, ModelMetadata metadata, bool imageSupport)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "richtextbox-toolbar btn-toolbar", role = "toolbar" }));
            tagBuilder.InnerHtml = CreateFormatButtonGroup() + CreateJustifyButtonGroup() + CreateListButtonGroup() + CreateExtendButtonGroup<T>(htmlHelper, metadata, imageSupport);
            return tagBuilder.ToString();
        }

        private static string CreateTextArea<T, U>(HtmlHelper<T> htmlHelper, ModelMetadata metadata, Expression<Func<T, U>> expression)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { id = metadata.PropertyName + "RichTextBox", @class = "richtextbox-textarea form-control", contenteditable = "true" }));
            tagBuilder.InnerHtml = InputExtensions.HiddenFor(htmlHelper, expression).ToString();
            return tagBuilder.ToString();
        }

        private static string CreateFormatButtonGroup()
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn-group btn-group-sm", role = "group" }));
            tagBuilder.InnerHtml = CreateFormatButtons();
            return tagBuilder.ToString();
        }

        private static string CreateJustifyButtonGroup()
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn-group btn-group-sm", role = "group" }));
            tagBuilder.InnerHtml = CreateJustifyButtons();
            return tagBuilder.ToString();
        }

        private static string CreateListButtonGroup()
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn-group btn-group-sm", role = "group" }));
            tagBuilder.InnerHtml = CreateListButtons();
            return tagBuilder.ToString();
        }

        private static string CreateExtendButtonGroup<T>(HtmlHelper<T> htmlHelper, ModelMetadata metadata, bool imageSupport)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn-group btn-group-sm", role = "group" }));
            tagBuilder.InnerHtml = CreateExtendButtons<T>(htmlHelper, metadata, imageSupport);
            return tagBuilder.ToString();
        }

        private static string CreateFormatButtons()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateButton("<i class='fa fa-bold'></i>", "bold"));
            sb.Append(CreateButton("<i class='fa fa-italic'></i>", "italic"));
            sb.Append(CreateButton("<i class='fa fa-underline'></i>", "underline"));
            sb.Append(CreateButton("<i class='fa fa-strikethrough'></i>", "strikeThrough"));
            sb.Append(CreateButton("<i class='fa fa-subscript'></i>", "subscript"));
            sb.Append(CreateButton("<i class='fa fa-superscript'></i>", "superscript"));
            sb.Append(CreateFontHeaderList());
            return sb.ToString();
        }

        private static string CreateJustifyButtons()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateButton("<i class='fa fa-align-left'></i>", "justifyleft"));
            sb.Append(CreateButton("<i class='fa fa-align-center'></i>", "justifycenter"));
            sb.Append(CreateButton("<i class='fa fa-align-right'></i>", "justifyright"));
            sb.Append(CreateButton("<i class='fa fa-align-justify'></i>", "justifyFull"));
            return sb.ToString();
        }

        private static string CreateListButtons()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateButton("<i class='fa fa-list-ol'></i>", "insertorderedlist"));
            sb.Append(CreateButton("<i class='fa fa-list-ul'></i>", "insertunorderedlist"));
            return sb.ToString();
        }

        private static string CreateExtendButtons<T>(this HtmlHelper<T> htmlHelper, ModelMetadata metadata, bool imageSupport)
        {
            string controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateButton("<i class='fa fa-link'></i>", "createLink"));
            sb.Append(CreateButton("<i class='fa fa-unlink'></i>", "unlink"));
            if (imageSupport)
            {
                sb.Append(LocalizedActionLinkExtensions.DialogActionLink(htmlHelper, "<i class='fa fa-image'></i>", HtmlConstants.DIALOG_FILE_REFERENCE, WebConstants.DIALOG_FILE_REFERENCE, controllerName, new { id = ((BaseDto)metadata.Container).Id }, new { @class = "btn btn-default" }));
                //sb.Append(LocalizedActionLinkExtensions.DialogActionLink(htmlHelper, "<i class='fa fa-image'></i>", HtmlConstants.DIALOG_FILE_REFERENCE, SharedConstants.DIALOG_FILE_REFERENCE, controllerName, new { elementId = ((BaseDto)metadata.Container).Id }, new { @class = "btn btn-default" }));
            }
            sb.Append(CreateButton("<i class='fa fa-eraser'></i>", "clear"));
            return sb.ToString();
        }

        private static string CreateFontHeaderList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateToggleButton("<i class='fa fa-header'></i> <span class='caret'></span>"));
            TagBuilder tagBuilder = new TagBuilder("ul");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "dropdown-menu" }));
            tagBuilder.InnerHtml = CreateFontHeaderListItems();
            sb.Append(tagBuilder.ToString());
            return sb.ToString();
        }

        private static string CreateFontHeaderListItems()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateFontHeaderListItem("H1", 18, "h1"));
            sb.Append(CreateFontHeaderListItem("H2", 16, "h2"));
            sb.Append(CreateFontHeaderListItem("H3", 14, "h3"));
            sb.Append(CreateFontHeaderListItem("H4", 12, "h4"));
            return sb.ToString();
        }

        private static string CreateFontHeaderListItem(string label, int fontSize, string header)
        {
            TagBuilder tagBuilder = new TagBuilder("li");
            tagBuilder.InnerHtml = CreateFontHeaderListItemLink(label, fontSize, header);
            return tagBuilder.ToString();
        }

        private static string CreateFontHeaderListItemLink(string label, int fontSize, string header)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { href = "#", @class = "btn", style = string.Format("font-size: {0}px", fontSize), data_command = "formatBlock", data_parameter = string.Format("<{0}>", header) }));
            return tagBuilder.ToString();
        }

        private static string CreateButton(string label, string command, string parameter = "")
        {
            TagBuilder tagBuilder = new TagBuilder("button");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn btn-default", type = "button", data_command = command, data_parameter = parameter }));
            tagBuilder.InnerHtml = label;
            return tagBuilder.ToString();
        }

        private static string CreateToggleButton(string label)
        {
            TagBuilder tagBuilder = new TagBuilder("button");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "btn btn-default dropdown-toggle", type = "button", data_toggle = "dropdown", aria_haspopup = "true", aria_expanded = "false" }));
            tagBuilder.InnerHtml = label;
            return tagBuilder.ToString();
        }
    }
}
