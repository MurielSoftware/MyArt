using Server.Model;
using Shared.Dtos.Paintings;
using Shared.Services.Paintings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using PagedList;
using Shared.Core.Dtos;
using Server.Daos;
using Shared.Core.Exceptions;
using Shared.I18n.Constants;

namespace Server.Services.Paintings
{
    public class PaintingCRUDService : GenericCRUDService<PaintingDto, Painting>, IPaintingCRUDService
    {
        private PaintingDao _paintingDao;

        public PaintingCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _paintingDao = new PaintingDao(unitOfWork);
        }

        public IPagedList<PaintingDto> ReadAdministrationPaged(PaintingFilterDto paintingFilterDto)
        {
            return _paintingDao.FindPaged(paintingFilterDto);
        }

        public IList<PaintingDto> ReadAdministrationAll(PaintingFilterDto paintingFilterDto)
        {
            return _paintingDao.FindAll(paintingFilterDto);
        }

        public List<PaintingCheckedDto> ReadCheckedDto(Guid exhibitionId)
        {
            return _paintingDao.FindPaintingsForCheck(exhibitionId);
        }

        protected override void ValidationBeforePersist(PaintingDto paintingDto)
        {
            base.ValidationBeforePersist(paintingDto);
            if (_genericDao.Exists<Painting>(x => x.Title == paintingDto.Title && x.Id != paintingDto.Id))
            {
                throw new ValidationException(MessageKeyConstants.VALIDATION_OBJECT_WITH_VALUE_ALREADY_EXISTS_MESSAGE, paintingDto.Title);
            }
        }
    }
}
