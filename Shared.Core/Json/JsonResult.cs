using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonResult
    {
        public virtual bool Success { get; private set; }
        public virtual object TargetElementId { get; private set; }
        public JsonRefreshMode RefreshMode { get; private set; }

        protected JsonResult(bool success, object targetElementId, JsonRefreshMode refreshMode)
        {
            Success = success;
            TargetElementId = targetElementId;
            RefreshMode = refreshMode;
        }

        public JsonResult CreateSuccess(object targetElementId, JsonRefreshMode refreshMode)
        {
            return new JsonResult(true, targetElementId, refreshMode);
        }

        public JsonResult CreateFail(object targetElementId, JsonRefreshMode refreshMode)
        {
            return new JsonResult(false, targetElementId, refreshMode);
        }
    }
}
