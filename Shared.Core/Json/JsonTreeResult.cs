using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonTreeResult : JsonDialogResult
    {
        public Guid? ParentId { get; private set; }

        protected JsonTreeResult(bool success, Guid? parentId, object targetElementId, JsonRefreshMode refreshMode, string message, string afterAction)
            : base(success, targetElementId, refreshMode, message, afterAction)
        {
            ParentId = parentId;
        }

        public static JsonDialogResult CreateTreeSuccess(Guid? parentId, object targetElementId, string message, string afterAction)
        {
            return new JsonTreeResult(true, parentId, targetElementId, JsonRefreshMode.REFRESH_TREE_AFTER_DIALOG_CLOSE, message, afterAction);
        }
    }
}
