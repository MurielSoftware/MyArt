using Shared.Core.Dtos.Resources;
using System;
using System.IO;

namespace Shared.Core.Services
{
    public interface IUploadFileService : IService
    {
        /// <summary>
        /// Uploads the standalone file to the article.
        /// </summary>
        /// <param name="userDefinableId">The ID of the UserDefinable.</param>
        /// <param name="ownerType">The type of the UserDefinable</param>
        /// <param name="fileName">The name of the uploaded file</param>
        /// <param name="stream">The stream of the uploaded file</param>
        /// <returns>The uploaded photo</returns>
        ResourceDto UploadFile(Guid userDefinableId, Type ownerType, string fileName, Stream stream);
    }
}
