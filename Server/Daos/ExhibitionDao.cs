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

namespace Server.Daos
{
    public class ExhibitionDao : BaseDao
    {
        public ExhibitionDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IPagedList<ExhibitionDto> FindPaged(BaseFilterDto baseFilterDto)
        {
            return _modelContext.Set<Exhibition>()
                .Where(ExpressionQueryBuilder.BuildWhere<Exhibition>(baseFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new ExhibitionDto() { Id = x.Id, Name = x.Name, Start = x.Start, End = x.End })
                .ToPagedList(baseFilterDto.Page, baseFilterDto.PageSize);
        }
    }
}
