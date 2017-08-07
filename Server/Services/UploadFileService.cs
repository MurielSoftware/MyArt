using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System.IO;

namespace Server.Services
{
    public class UploadFileService : BaseService, IUploadFileService
    {
        public UploadFileService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ResourceDto UploadFile(Guid userDefinableId, Type ownerType, string fileName, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
