using Shared.Core.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonFileSelectDialogResult : JsonDialogResult
    {
        public string ThumbnailPath { get; set; }
        public string Path { get; set; }
         
        public JsonFileSelectDialogResult(bool success, string thumbnailPath, string path, JsonRefreshMode refreshMode)
            : base(success, refreshMode)
        {
            ThumbnailPath = thumbnailPath;
            Path = path;
        }
    }
}
