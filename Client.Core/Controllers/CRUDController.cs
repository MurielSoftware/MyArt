using Client.Core.Constants;
using Client.Core.HtmlHelpers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Exceptions;
using Shared.Core.Json;
using Shared.Core.Messages;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Core.Controllers
{
    public abstract class CRUDController<T, U> : ServiceController<U>
        where T : BaseDto
        where U : ICRUDService<T>
    {

        /// <summary>
        /// Gets the View for create the new entity.
        /// </summary>
        /// <returns>The view for create the new entity.</returns>
        public virtual ActionResult Create(Guid? id)
        {
            if (!id.HasValue)
            {
                SaveToTempAndView(TempDataConstants.PRECREATED_DTO);
                if (ViewData[TempDataConstants.PRECREATED_DTO] == null)
                {
                    return View(Activator.CreateInstance<T>());
                }
                if (!(ViewData[TempDataConstants.PRECREATED_DTO] is T))
                {
                    return View(Activator.CreateInstance<T>());
                }
                return View(ViewData[TempDataConstants.PRECREATED_DTO]);
            }
            T entity = GetService().Read(id.Value);
            return View(entity);
        }

        /// <summary>
        /// Gets the view for the creation of the entity with the predefined values.
        /// </summary>
        /// <param name="precreatedDto">Precreated DTO</param>
        /// <returns>The create view with the predefined DTO</returns>
        public virtual ActionResult CreatePredefined(T precreatedDto)
        {
            TempData[TempDataConstants.PRECREATED_DTO] = precreatedDto;
            return RedirectToAction(WebConstants.VIEW_CREATE, GetControllerName());
        }

        /// <summary>
        /// Calls the appropriate service and persists the DTO to the database.
        /// </summary>
        /// <param name="dto">The DTO to persist</param>
        /// <param name="message">The message to display to user after the operation is finished</param>
        /// <param name="actionName">The name of the action</param>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="routeValues">The route values for redirect</param>
        /// <returns>The apropriate View</returns>
        public virtual ActionResult DoCreate(T dto, Message message, string actionName, string controllerName, object routeValues = null, string targetHtmlId = null)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToActionAfterClientFailCreate(dto, null);
            }

            try
            {
                GetUnitOfWork().StartTransaction();
                dto = GetService().Persist(dto);
                GetUnitOfWork().EndTransaction();
                TempData[TempDataConstants.MESSAGE] = message;
            }
            catch (ValidationException ex)
            {
                return RedirectToActionAfterServerFailCreate(dto, ex.GetValidationResults());
            }
            return RedirectToActionAfterSuccessCreate(dto.Id, actionName, controllerName, routeValues, targetHtmlId, JsonRefreshMode.FULL);
        }

        protected virtual ActionResult RedirectToActionAfterClientFailCreate(T dto, string validationResult)
        {
            return View(dto);
        }

        protected virtual ActionResult RedirectToActionAfterServerFailCreate(T dto, string validationResult)
        {
            ModelState.AddModelError(TempDataConstants.SERVER_VALIDATION_ERROR, validationResult);
            return View(dto);
        }

        protected virtual ActionResult RedirectToActionAfterSuccessCreate(Guid id, string actionName, string controllerName, object routeValues, string targetHtmlId, JsonRefreshMode jsonRefreshMode)
        {
            return RedirectToAction(actionName, controllerName, routeValues);
        }

        /// <summary>
        /// Delets the object after the confirmation.
        /// </summary>
        /// <param name="id">The ID of the object to delete</param>
        /// <param name="actionName">The name of the action to call after success deletion</param>
        /// <param name="controllerName">The name of the controller of the action to call after success deletion</param>
        /// <param name="message">The message ti display after deletion</param>
        /// <returns>The validation summary if the deletion is not possible or appropriate refreshed view</returns>
        public virtual ActionResult DoDeleteConfirmed(Guid id, Message message, string actionName, string controllerName, object routeValues = null, string targetHtmlId = null)
        {
            try
            {
                GetUnitOfWork().StartTransaction();
                GetService().Delete(id);
                GetUnitOfWork().EndTransaction();
                TempData[TempDataConstants.MESSAGE] = message;
            }
            catch(ValidationException ex)
            {
                return RedirectToActionAfterFailDelete(ex.GetValidationResults());
            }
            return RedirectToActionAfterSuccessDelete(id, actionName, controllerName, targetHtmlId, routeValues, JsonRefreshMode.PARTIAL);
        }

        protected virtual ActionResult RedirectToActionAfterFailDelete(string validationResult)
        {
            return Json(new JsonDialogResult(false, HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(validationResult).ToString()));
        }

        protected virtual ActionResult RedirectToActionAfterSuccessDelete(Guid id, string actionName, string controllerName, string targetId, object routeValues, JsonRefreshMode refreshMode)
        {
            return Json(new JsonDialogResult(true, targetId, Url.Action(actionName, controllerName, routeValues), refreshMode));
        }

        /// <summary>
        /// Disposes the database.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            GetUnitOfWork().Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Creates the new entity from the view
        /// </summary>
        /// <param name="entity">The created entity</param>
        /// <returns>Returns the appropriate view</returns>
        public abstract ActionResult Create(T dto);

        /// <summary>
        /// Delete the entity after the confirmation.
        /// </summary>
        /// <param name="dialogDto">The DTO for dialog</param>
        /// <returns>Returns the appropriate view</returns>
        public abstract ActionResult DeleteConfirmed(DialogDto dialogDto);
    }
}
