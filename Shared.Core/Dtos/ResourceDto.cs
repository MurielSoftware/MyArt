using Shared.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos
{
    public class ResourceDto : BaseDto
    {
        public virtual string OriginalName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Path { get; set; }
        public virtual Guid UserDefinableId { get; set; }
        public virtual Guid UserDefinableOwnerId { get; set; }

        public virtual string Extension { get; set; }
        public virtual Stream Stream { get; set; }

        public string GetAbsolutePath()
        {
            return IOUtils.GetUploadRoot() + Path;
        }

        public string GetAbsoluteFilePath()
        {
            return IOUtils.GetUploadRoot() + GetRelativeFilePath();
        }

        public string GetRelativeFilePath()
        {
            return Path + Name;
        }
    }
}
