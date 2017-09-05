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
using Server.Services.Paintings;
using System;
using System.Collections.Generic;
using Shared.Core.Constants;

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

        protected override void DoDelete(DeletionDto deletionDto, Collection collection)
        {
            CollectionDeletionDto collectionDeletionDto = (CollectionDeletionDto)deletionDto;
            if (collectionDeletionDto.DeleteAllPaintings)
            {
                DeleteAllPaintingsForCollection(_unitOfWork, collection.Id);
            }
            else
            {
                SetDefaultCollectionToPaintingsInRemovedCollection(_unitOfWork, collection.Id);
            }
            base.DoDelete(deletionDto, collection);
        }

        private static void DeleteAllPaintingsForCollection(IUnitOfWork unitOfWork, Guid collectionId)
        {
            GenericDao genericDao = new GenericDao(unitOfWork);
            PaintingCRUDService paintingCRUDService = new PaintingCRUDService(unitOfWork);

            genericDao.FindIds<Painting>(x => x.CollectionId == collectionId).ForEach(x => paintingCRUDService.Delete(new DeletionDto() { Id = collectionId }));
        }

        private static void SetDefaultCollectionToPaintingsInRemovedCollection(IUnitOfWork unitOfWork, Guid collectionId)
        {
            GenericDao genericDao = new GenericDao(unitOfWork);
            PaintingCRUDService paintingCRUDService = new PaintingCRUDService(unitOfWork);
            IList<Painting> paintingsToUpdate = genericDao.Find<Painting>(x => x.CollectionId == collectionId);
            foreach(Painting paintingToUpdate in paintingsToUpdate)
            {
                paintingToUpdate.CollectionId = GuidConstants.DEFAULT_COLLECTION_ID;
            }
            genericDao.Persist<Painting>(paintingsToUpdate);
        }
    }
}
