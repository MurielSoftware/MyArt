using Client.Core.Constants;
using Client.Core.Controllers;
using Client.Core.HtmlHelpers;
using Shared.Core.Constants;
using Shared.Core.Dtos.Users;
using Shared.Core.Json;
using Shared.Core.Messages;
using Shared.Core.Sessions;
using Shared.Dtos.Users;
using Shared.I18n.Constants;
using Shared.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyArt.Areas.Admin.Controllers
{
    public class LoginController : ServiceController<IUserAuthenticationService>
    {
        public override ActionResult Index()
        {
            return View(new UserAuthenticationDto());
        }

        [HttpPost]
        public ActionResult Login(UserAuthenticationDto userAuthenticationDto)
        {
            if (ModelState.IsValid)
            {
                if (GetService().Login(userAuthenticationDto) != null)
                {
                    GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, Message.CreateSuccessMessage(MessageKeyConstants.LOGIN_SUCCESSFUL_MESSAGE));
                    return RedirectToAction(WebConstants.VIEW_INDEX, WebConstants.CONTROLLER_HOME);
                }
                GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, Message.CreateErrorMessage(MessageKeyConstants.LOGIN_FAILURE_MESSAGE));
            }
            return View("Index", userAuthenticationDto);
        }

        public ActionResult Logout()
        {
            SessionProvider.GetInstance().RemoveSession(UserSession.SESSION_NAME);
            GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, Message.CreateSuccessMessage(MessageKeyConstants.LOGOUT_SUCCESSFUL_MESSAGE));
            return View("Index");
        }

        public ActionResult ChangePassword(Guid id)
        {
            return PartialView(new ChangePasswordDto() { UserId = id });
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(JsonDialogResult.CreateFail(HtmlConstants.DIALOG_VALIDATION_SUMMARY, ValidationSummaryExtensions.CustomValidationSummary(ModelState).ToString()));
            }
            GetUnitOfWork().StartTransaction();
            GetService().ChangePassword(changePasswordDto);
            GetUnitOfWork().EndTransaction();
            GetTempDataManager().SetTempData(TempDataConstants.MESSAGE, Message.CreateSuccessMessage(MessageKeyConstants.CHANGE_PASSWORD_SUCCESSFUL_MESSAGE));
            return Json(JsonDialogResult.CreateSuccess(null, Url.Action(WebConstants.VIEW_PROFILE, WebConstants.CONTROLLER_USER, new { id = changePasswordDto.UserId })));
        }
    }
}