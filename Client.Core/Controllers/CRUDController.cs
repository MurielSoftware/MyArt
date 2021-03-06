﻿using Client.Core.AfterSaves;
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
                GetTempDataManager().GetTempDataWithoutRemove(TempDataConstants.PRECREATED_DTO);
                if (GetTempDataManager().GetViewData(TempDataConstants.PRECREATED_DTO) == null)
                {
                    return RedirectCreate(Activator.CreateInstance<T>());
                }
                if (!(GetTempDataManager().GetViewData(TempDataConstants.PRECREATED_DTO) is T))
                {
                    return RedirectCreate(Activator.CreateInstance<T>());
                }
                return RedirectCreate(GetTempDataManager().GetViewData<T>(TempDataConstants.PRECREATED_DTO));
            }
            return RedirectCreate(GetService().Read(id.Value));
        }

        protected virtual ActionResult RedirectCreate(T dto)
        {
            return View(dto);
        }

        public virtual ActionResult Details(Guid id)
        {
            return View(GetService().Read(id));
        }

        /// <summary>
        /// Gets the view for the creation of the entity with the predefined values.
        /// </summary>
        /// <param name="precreatedDto">Precreated DTO</param>
        /// <returns>The create view with the predefined DTO</returns>
        public virtual ActionResult CreatePredefined(T precreatedDto)
        {
            GetTempDataManager().SetTempData(TempDataConstants.PRECREATED_DTO, precreatedDto);
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
        public virtual ActionResult DoCreate(T dto, AfterSuccessSaveParam afterSuccessSaveParam)
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

                afterSuccessSaveParam.Id = dto.Id;

                GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, afterSuccessSaveParam.Message);
                GetTempDataManager().SetTempData(TempDataConstants.DTO, dto);
            }
            catch (ValidationException ex)
            {
                return RedirectToActionAfterServerFailCreate(dto, AfterFailSaveParam.Create(ex.GetValidationResults()));
            }
            return RedirectToActionAfterSuccessCreate(afterSuccessSaveParam);
        }

        protected virtual ActionResult RedirectToActionAfterClientFailCreate(T dto, AfterFailSaveParam afterFailSaveParam)
        {
            return View(dto);
        }

        protected virtual ActionResult RedirectToActionAfterServerFailCreate(T dto, AfterFailSaveParam afterFailSaveParam)
        {
            ModelState.AddModelError(TempDataConstants.SERVER_VALIDATION_ERROR, afterFailSaveParam.ValidationMessage);
            return View(dto);
        }

        protected virtual ActionResult RedirectToActionAfterSuccessCreate(AfterSuccessSaveParam afterSuccessSaveParam)
        {
            return RedirectToAction(afterSuccessSaveParam.Action, afterSuccessSaveParam.Controller, afterSuccessSaveParam.RouteValues);
        }

        public virtual ActionResult DialogDeleteConfirmation(DeletionDto deletionDto)
        {
            return RedirectDialogDeleteConfirmation("_ConfirmationDialog", deletionDto);
        }

        protected ActionResult RedirectDialogDeleteConfirmation(string view, DeletionDto deletionDto)
        {
            return PartialView(view, deletionDto);
        }

        /// <summary>
        /// Delets the object after the confirmation.
        /// </summary>
        /// <param name="id">The ID of the object to delete</param>
        /// <param name="actionName">The name of the action to call after success deletion</param>
        /// <param name="controllerName">The name of the controller of the action to call after success deletion</param>
        /// <param name="message">The message ti display after deletion</param>
        /// <returns>The validation summary if the deletion is not possible or appropriate refreshed view</returns>
        public virtual ActionResult DoDeleteConfirmed(AfterDeleteParam afterDeleteParam)
        {
            try
            {
                GetUnitOfWork().StartTransaction();
                GetService().Delete(afterDeleteParam.DeletionDto);
                GetUnitOfWork().EndTransaction();
                GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, afterDeleteParam.Message);
            }
            catch(ValidationException ex)
            {
                return RedirectToActionAfterFailDelete(AfterFailSaveParam.Create(ex.GetValidationResults()));
            }
            return RedirectToActionAfterSuccessDelete(afterDeleteParam);
        }

        protected virtual ActionResult RedirectToActionAfterFailDelete(AfterFailSaveParam afterFailSaveParam)
        {
            return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(afterFailSaveParam.ValidationMessage).ToString()));
        }

        protected virtual ActionResult RedirectToActionAfterSuccessDelete(AfterDeleteParam afterDeleteParam)
        {
            return Json(JsonDialogResult.CreateSuccess(afterDeleteParam.TargetHtml, null, afterDeleteParam.GetAction()));
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
       /// public abstract ActionResult DeleteConfirmed(DeletionDto deletionDto);
    }
}
