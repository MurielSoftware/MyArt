using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonDialogResult
    {
        public bool Success { get; set; }
        public object TargetId { get; set; }
        public JsonRefreshMode RefreshMode { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }

        public JsonDialogResult(bool success, JsonRefreshMode refreshMode)
        {
            Success = success;
            RefreshMode = refreshMode;
        }

        public JsonDialogResult(bool success, object targetId, string action, JsonRefreshMode refreshMode)
            : this(success, refreshMode)
        {
            TargetId = targetId;
            Action = action;
        }

        public JsonDialogResult(bool success, object targetId, string message)
            : this(success, JsonRefreshMode.NONE)
        {
            TargetId = targetId;
            Message = message;
        }
    }
}
