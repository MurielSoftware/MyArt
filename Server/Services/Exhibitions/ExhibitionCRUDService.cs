using Server.Model;
using Shared.Dtos.Exhibitions;
using Shared.Services.Exhibitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Server.Daos;
using PagedList;
using Shared.Core.Dtos;
using Shared.Dtos.Paintings;

namespace Server.Services.Exhibitions
{
    public class ExhibitionCRUDService : GenericCRUDService<ExhibitionDto, Exhibition>, IExhibitionCRUDService
    {
        private ExhibitionDao _exhibitionDao;

        public ExhibitionCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _exhibitionDao = new ExhibitionDao(unitOfWork);
        }

        public IPagedList<ExhibitionDto> ReadAdministrationPaged(BaseFilterDto baseFilterDto)
        {
            return _exhibitionDao.FindPaged(baseFilterDto);
        }

        public override ExhibitionDto Persist(ExhibitionDto exhibitionDto)
        {
            if (exhibitionDto.PaintingsCheckedDto != null)
            {
                List<PaintingCheckedDto> paintingCheckedDtos = exhibitionDto.PaintingsCheckedDto.Where(x => x.Checked == true).Select(x => x).ToList();
                exhibitionDto.PaintingsReference = ReferenceString.Create<PaintingCheckedDto>(paintingCheckedDtos);
            }
            return base.Persist(exhibitionDto);
        }
    }
}
