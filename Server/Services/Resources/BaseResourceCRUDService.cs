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
    public abstract class BaseResourceCRUDService<T, U> : GenericCRUDService<T, U>
        where T : ResourceDto
        where U : Resource
    {
        public BaseResourceCRUDService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public abstract BaseResourceUploadService<T> GetUploadResourceService();

        public override T Persist(T resourceDto)
        {
            resourceDto.Name = Guid.NewGuid().ToString() + resourceDto.Extension;
            T persistedResourceDto = base.Persist(resourceDto);
            GetUploadResourceService().Upload(resourceDto);
            return persistedResourceDto;
        }

        public override void Delete(Guid id)
        {
            GetUploadResourceService().Delete(Read(id));
            base.Delete(id);
        }
    }
}
