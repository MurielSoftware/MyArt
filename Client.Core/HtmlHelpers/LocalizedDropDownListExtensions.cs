using Shared.Core.Attributes;
using Shared.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Shared.I18n.Resources;
using Shared.I18n.Utils;

namespace Client.Core.HtmlHelpers
{
    public static class LocalizedDropDownListExtensions
    {
        /// <summary>
        /// Gets the DropDownList component for the Enum property.
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <typeparam name="U">The property</typeparam>
        /// <param name="htmlHelper">The HtmlHelper</param>
        /// <param name="expression">The expression for the property</param>
        /// <param name="htmlAttributes">The next html attributes for the component</param>
        /// <returns>The HTML string of the DropDownList with internationalized options</returns>
        public static MvcHtmlString LocalizedDropDownListFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);

            List<SelectListItem> selectListItem = CreateListItems<U>(typeof(T), Enum.GetValues(enumType).Cast<U>(), metadata);

            return SelectExtensions.DropDownListFor(htmlHelper, expression, selectListItem, htmlAttributes);
        }

        /// <summary>
        /// Creates the DropDownList component with the remote content.
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <typeparam name="U">The property</typeparam>
        /// <param name="htmlHelper">The HtmlHelper</param>
        /// <param name="expression">The expression for the propperty</param>
        /// <param name="action">The action name to get the content</param>
        /// <returns>Returns the DropDownList component with the remote content</returns>
        public static MvcHtmlString RemoteDropDownListFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, string action)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return SelectExtensions.DropDownList(htmlHelper, metadata.PropertyName, new List<SelectListItem>(), new { @class = "referencelist form-control", data_action = action, data_selected = metadata.Model });
        }

        /// <summary>
        /// Display the localized raw text of the enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="htmlHelper">The html helper</param>
        /// <param name="expression">The expression for the property</param>
        /// <param name="htmlAttributes">The html attributes</param>
        /// <returns>Returns the localized raw text of the selected value</returns>
        public static MvcHtmlString LocalizedDropDownDisplayFor<T, U>(this HtmlHelper<T> htmlHelper, Expression<Func<T, U>> expression, object htmlAttributes = null)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (modelMetadata == null || modelMetadata.Model == null)
            {
                return MvcHtmlString.Create(string.Empty);
            }
            List<SelectListItem> selectListItem = CreateListItems<U>(modelMetadata.ContainerType, Enum.GetValues(typeof(U)).Cast<U>(), modelMetadata);
            SelectListItem selectedValue = selectListItem.Where(f => f.Value.Equals(modelMetadata.Model.ToString())).FirstOrDefault();
            if (selectedValue == null)
            {
                //return EditableControlExtensions.CreateLabel(htmlHelper, modelMetadata.PropertyName, selectListItem.First().Text, htmlAttributes);
                return DivExtensions.CreateSpan<T>(htmlHelper, selectListItem.First().Text, htmlAttributes);
            }
            //return EditableControlExtensions.CreateLabel(htmlHelper, modelMetadata.PropertyName, selectedValue.Text, htmlAttributes);
            return DivExtensions.CreateSpan<T>(htmlHelper, selectedValue.Text, htmlAttributes);
        }

        /// <summary>
        /// Creates the list items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerType">The model</param>
        /// <param name="values">The values from the enum</param>
        /// <param name="modelMetadata">The model metadata of the current element</param>
        /// <returns>The created options for the list</returns>
        private static List<SelectListItem> CreateListItems<T>(Type containerType, IEnumerable<T> values, ModelMetadata modelMetadata)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (modelMetadata.IsNullableValueType)
            {
                selectListItems.Add(CreateListItem("", "", modelMetadata));
                selectListItems.First().Selected = true;
            }

            foreach (T value in values)
            {
                EnumAttribute enumAttribute = (EnumAttribute)value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(EnumAttribute), false).First();
                if (enumAttribute.DtoType == null || enumAttribute.DtoType.Equals(containerType))
                {
                    //if (!enumAttribute.RoleType.HasValue || (RoleExtensions.IsLoggedUserWithSpecificRights(enumAttribute.RoleType.Value)))
                    //{
                        selectListItems.Add(CreateListItem(enumAttribute.ResourceKey, value, modelMetadata.Model));
                    //}
                }
            }
            return selectListItems;
        }

        /// <summary>
        /// Creates the list item.
        /// </summary>
        /// <param name="resourceKey">The resource key</param>
        /// <param name="value">The value</param>
        /// <param name="model">The model of the current element</param>
        /// <returns>The list item</returns>
        private static SelectListItem CreateListItem(string resourceKey, object value, object model)
        {
            return new SelectListItem() { Text = ResourceUtils.GetString(resourceKey), Value = value.ToString(), Selected = value.Equals(model) };
        }

        /// <summary>
        /// Gets the non nullable model type.
        /// </summary>
        /// <param name="modelMetadata">The model metadata of the current poroperty</param>
        /// <returns>The non nullable type</returns>
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }
    }
}
