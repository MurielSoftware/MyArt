using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonUploadResult : JsonResult
    {
        public Guid Id { get; set; }
        public virtual string AbsoluteFilePath { get; set; }
        public virtual string RelativeFilePath { get; set; }

        public JsonUploadResult(bool success, Guid id, object targetElementId, JsonRefreshMode refreshMode, string absoluteFilePath, string relativeFilePath) 
            : base(success, targetElementId, refreshMode)
        {
            Id = id;
            AbsoluteFilePath = absoluteFilePath;
            RelativeFilePath = relativeFilePath;
        }

        public static JsonUploadResult CreateSuccess(Guid id, object targetElementId, JsonRefreshMode refreshMode, string absoluteFilePath, string relativeFilePath)
        {
            return new JsonUploadResult(true, id, targetElementId, refreshMode, absoluteFilePath, relativeFilePath);
        }
    }
}
