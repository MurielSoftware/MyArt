using Server.Model;
using System;
using Shared.Core.Context;
using Shared.Core.Services;
using Server.Daos;
using Shared.Core.Dtos.Resources;
using Shared.Core.Dtos;

namespace Server.Services.Resources
{
    public abstract class BaseResourceCRUDService<T, U> : GenericCRUDService<T, U>
        where T : ResourceDto
        where U : Resource
    {
        protected ResourceDao _resourceDao;

        public BaseResourceCRUDService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _resourceDao = new ResourceDao(unitOfWork);
        }

        public abstract BaseResourceUploadService<T> GetUploadResourceService();

        public override T Persist(T resourceDto)
        {
            resourceDto.Name = Guid.NewGuid().ToString() + resourceDto.Extension;
            T persistedResourceDto = base.Persist(resourceDto);
            GetUploadResourceService().Upload(resourceDto);
            return persistedResourceDto;
        }

        public override void Delete(DeletionDto deletionDto)
        {
            GetUploadResourceService().Delete(Read(deletionDto.Id));
            base.Delete(deletionDto);
        }
    }
}
