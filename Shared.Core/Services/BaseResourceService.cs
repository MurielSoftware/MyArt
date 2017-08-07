using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Utils;
using System.IO;

namespace Shared.Core.Services
{
    /// <summary>
    /// Base upload service. It should be extended by all upload services.
    /// </summary>
    /// <typeparam name="T">The type of the Base Upload DTO</typeparam>
    public class BaseResourceService<T> : BaseService where T : ResourceDto
    {
        public BaseResourceService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Uploads the object.
        /// </summary>
        /// <param name="resourceDto">The Resource DTO with the all information needed to upload the file</param>
        /// <returns>Returns the base resource DTO with the filled file pathts</returns>
        public virtual T Upload(T resourceDto)
        {
            if(resourceDto.Stream == null)
            {
                return resourceDto;
            }

            ValidateBeforeUpload(resourceDto);

            IOUtils.CreateDirectories(resourceDto.GetAbsolutePath());

            using (FileStream fileStream = new FileStream(resourceDto.GetAbsoluteFilePath(), FileMode.Create, FileAccess.Write))
            {
                resourceDto.Stream.CopyTo(fileStream);
            }
            return resourceDto;
        }

        /// <summary>
        /// Deletes the file and the folder structure if it is needed.
        /// </summary>
        /// <param name="resourceDto">The Resource DTO with the all information needed to remove the file</param>
        public virtual void Delete(T resourceDto)
        {
            if(string.IsNullOrEmpty(resourceDto.GetRelativeFilePath()))
            {
                return;
            }

            IOUtils.Delete(resourceDto.GetAbsoluteFilePath());
            IOUtils.DeleteDirectoryIfNeeded(resourceDto.GetAbsolutePath());
        }

        protected void ValidateBeforeUpload(T resourceDto)
        {
        }
    }
}
