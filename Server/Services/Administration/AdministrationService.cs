using Shared.Core.Services;
using Shared.Services.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Server.Daos;
using Shared.Dtos.Administration;
using Server.Model;

namespace Server.Services.Administration
{
    public class AdministrationService : BaseService, IAdministrationService
    {
        private GenericDao _genericDao;

        public AdministrationService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _genericDao = new GenericDao(unitOfWork);
        }

        public AdministrationDto Read()
        {
            AdministrationDto administrationDto = new AdministrationDto();
            administrationDto.ArtistCount = _genericDao.Count<User>();
            administrationDto.PaintingCount = _genericDao.Count<Painting>();
            administrationDto.CollectionCount = _genericDao.Count<Collection>();
            administrationDto.ExhibitionCount = _genericDao.Count<Exhibition>();
            return administrationDto;
        }
    }
}
