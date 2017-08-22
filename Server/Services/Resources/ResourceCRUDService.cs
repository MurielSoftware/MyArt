using Server.Model;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Services;

namespace Server.Services.Resources
{
    public class ResourceCRUDService : BaseResourceCRUDService<ResourceDto, Resource>
    {
        public ResourceCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override BaseResourceUploadService<ResourceDto> GetUploadResourceService()
        {
            return new BaseResourceUploadService<ResourceDto>(_unitOfWork);
        }
    }
}
