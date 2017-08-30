using Server.Model;
using Shared.Dtos.Collections;
using Shared.Services.Collections;
using Shared.Core.Context;
using PagedList;
using Shared.Core.Dtos;
using Server.Daos;
using Shared.Core.Dtos.References;

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
    }
}
