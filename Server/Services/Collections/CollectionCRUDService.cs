using Server.Model;
using Shared.Dtos.Collections;
using Shared.Services.Collections;
using Shared.Core.Context;
using PagedList;
using Shared.Core.Dtos;
using Server.Daos;
using Shared.Core.Dtos.References;
using Shared.Core.Exceptions;
using Shared.I18n.Constants;

namespace Server.Services.Collections
{
    public class CollectionCRUDService : GenericCRUDService<CollectionDto, Collection>, ICollectionCRUDService
    {
        private CollectionDao _collectionDao;

        public CollectionCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _collectionDao = new CollectionDao(unitOfWork);
        }

        public ListReferenceDto GetAllReferences(BaseFilterDto baseFilterDto)
        {
            return _collectionDao.FindAllReferences(baseFilterDto);
        }

        public IPagedList<CollectionDto> ReadAdministrationPaged(BaseFilterDto baseFilterDto)
        {
            return _collectionDao.FindPaged(baseFilterDto);
        }

        protected override void ValidationBeforePersist(CollectionDto collectionDto)
        {
            base.ValidationBeforePersist(collectionDto);
            if (_genericDao.Exists<Collection>(x => x.Name == collectionDto.Name && x.Id != collectionDto.Id))
            {
                throw new ValidationException(MessageKeyConstants.VALIDATION_OBJECT_WITH_VALUE_ALREADY_EXISTS_MESSAGE, collectionDto.Name);
            }
        }
    }
}
