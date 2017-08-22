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

        public IPagedList<PaintingDto> ReadAdministrationPaged(BaseFilterDto baseFilterDto)
        {
            return _paintingDao.FindPaged(baseFilterDto);
        }

        public List<PaintingCheckedDto> ReadCheckedDto()
        {
            return _paintingDao.FindPaintingsForCheck();
        }
    }
}
