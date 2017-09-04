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
using Shared.Core.Dtos.References;
using Shared.Core.Utils;

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

        public override ExhibitionDto Persist(ExhibitionDto exhibitionDto)
        {
            if (exhibitionDto.PaintingsCheckedDto != null)
            {
                List<PaintingCheckedDto> paintingCheckedDtos = exhibitionDto.PaintingsCheckedDto.Where(x => x.Checked == true).Select(x => x).ToList();
                exhibitionDto.PaintingsReference = ReferenceString.Create<PaintingCheckedDto>(paintingCheckedDtos);
            }
            return base.Persist(exhibitionDto);
        }

        public IPagedList<ExhibitionDto> ReadAdministrationPaged(ExhibitionFilterDto exhibitionFilterDto)
        {
            return _exhibitionDao.FindPaged(exhibitionFilterDto);
        }

        public IList<ExhibitionDto> ReadAdministrationAll(ExhibitionFilterDto exhibitionFilterDto)
        {
            return _exhibitionDao.FindAll(exhibitionFilterDto);
        }

        protected override Exhibition CreateEntity(ExhibitionDto exhibitionDto)
        {
            Exhibition exhibition = base.CreateEntity(exhibitionDto);
            DateTime? timeStart = StringUtils.ParseTime(exhibitionDto.TimeStart);
            DateTime? timeEnd = StringUtils.ParseTime(exhibitionDto.TimeEnd);
            if (timeStart.HasValue)
            {
                exhibition.Start = new DateTime(exhibitionDto.Start.Year, exhibitionDto.Start.Month, exhibitionDto.Start.Day, timeStart.Value.Hour, timeStart.Value.Minute, 0);
            }
            if (timeEnd.HasValue)
            {
                exhibition.End = new DateTime(exhibitionDto.End.Year, exhibitionDto.End.Month, exhibitionDto.End.Day, timeEnd.Value.Hour, timeEnd.Value.Minute, 0);
            }
            return exhibition;
        }

        protected override ExhibitionDto CreateDto(Exhibition exhibition)
        {
            ExhibitionDto exhibitionDto = base.CreateDto(exhibition);
            exhibitionDto.TimeStart = StringUtils.ParseTime(exhibitionDto.Start);
            exhibitionDto.TimeEnd = StringUtils.ParseTime(exhibitionDto.End);
            return exhibitionDto;
        }
    }
}
