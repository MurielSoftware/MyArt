using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonDialogResult : JsonResult
    {
        public string AfterAction { get; private set; }
        public string Message { get; private set; }

        protected JsonDialogResult(bool success, object targetElementId, JsonRefreshMode refreshMode, string message, string afterAction)
            : base(success, targetElementId, refreshMode)
        {
            Message = message;
            AfterAction = afterAction;
        }

        public static JsonDialogResult CreateSuccess(object targetElementId, string message)
        {
            return new JsonDialogResult(true, targetElementId, JsonRefreshMode.REFRESH_AFTER_DIALOG_CLOSE, message, null);
        }

        public static JsonDialogResult CreateSuccess(object targetElementId, string message, string afterAction)
        {
            return new JsonDialogResult(true, targetElementId, JsonRefreshMode.REFRESH_AFTER_DIALOG_CLOSE, message, afterAction);
        }

        public static JsonDialogResult CreateFail(object targetElementId, string message)
        {
            return new JsonDialogResult(false, targetElementId, JsonRefreshMode.NONE, message, null);
        }

        //public JsonDialogResult(bool success, JsonRefreshMode refreshMode)
        //{
        //    Success = success;
        //    RefreshMode = refreshMode;
        //}

        //public JsonDialogResult(bool success, object targetId, string action, JsonRefreshMode refreshMode)
        //    : this(success, refreshMode)
        //{
        //    TargetId = targetId;
        //    Action = action;
        //}

        //public JsonDialogResult(bool success, object targetId, string message)
        //    : this(success, JsonRefreshMode.NONE)
        //{
        //    TargetId = targetId;
        //    Message = message;
        //}
    }
}
