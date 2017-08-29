using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using PagedList;
using Shared.Dtos.Exhibitions;
using Server.Model;
using Shared.Core.Dtos;
using Shared.Core.Context.Expressions;

namespace Server.Daos
{
    public class ExhibitionDao : BaseDao
    {
        public ExhibitionDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IPagedList<ExhibitionDto> FindPaged(ExhibitionFilterDto exhibitionFilterDto)
        {
            return _modelContext.Set<Exhibition>()
                .Where(ExpressionBuilder.BuildWhere<Exhibition>(exhibitionFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new ExhibitionDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Start = x.Start,
                    End = x.End
                })
                .ToPagedList(exhibitionFilterDto.Page, exhibitionFilterDto.PageSize);
        }

        public IList<ExhibitionDto> FindAll(ExhibitionFilterDto exhibitionFilterDto)
        {
            //return _modelContext.Set<Exhibition>()
            //    .Where(ExpressionBuilder.BuildWhere<Exhibition>(exhibitionFilterDto))
            //    .OrderBy(x => x.Name)
            //    .Select(x => new ExhibitionDto()
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Start = x.Start,
            //        End = x.End
            //    })
            //    .ToList();

            return _modelContext.Set<Exhibition>()
                .Where(x => x.Paintings.Any(y => y.Users.Any(z => z.Id == exhibitionFilterDto.UserId)))
                .OrderBy(x => x.Start)
                .Select(x => new ExhibitionDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Start = x.Start,
                    End = x.End
                })
                .ToList();
        }
    }
}
