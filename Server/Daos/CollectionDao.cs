using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Server.Model;
using PagedList;
using Shared.Dtos.Collections;
using Shared.Core.Dtos;

namespace Server.Daos
{
    public class CollectionDao : BaseDao
    {
        public CollectionDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        internal ListReferenceDto FindAllReferences(BaseFilterDto baseFilterDto)
        {
            Dictionary<Guid, string> references = _modelContext.Set<Collection>()
                .Where(ExpressionQueryBuilder.BuildWhere<Collection>(baseFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new { Id = x.Id, Name = x.Name })
                .ToDictionary(x => x.Id, x => x.Name);
            return new ListReferenceDto() { References = references };
        }

        public IPagedList<CollectionDto> FindPaged(BaseFilterDto baseFilterDto)
        {
            return _modelContext.Set<Collection>()
                .Where(ExpressionQueryBuilder.BuildWhere<Collection>(baseFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new CollectionDto() { Id = x.Id, Name = x.Name })
                .ToPagedList(baseFilterDto.Page, baseFilterDto.PageSize);
        }
    }
}
