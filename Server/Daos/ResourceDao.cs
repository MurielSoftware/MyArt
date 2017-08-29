using Server.Model;
using Shared.Core.Context;
using Shared.Core.Context.Expressions;
using Shared.Core.Dtos;
using Shared.Core.Dtos.Resources;
using Shared.Dtos.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Daos
{
    public class ResourceDao : BaseDao
    {
        public ResourceDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IList<PhotoResourceDto> FindAll(ResourceFilterDto resourceFilterDto)
        {
            if (resourceFilterDto.UserDefinableOwnerId.HasValue)
            {
                return FindAllToUserDefinableOwner(resourceFilterDto.UserDefinableOwnerId.Value);
            }
            return _modelContext.Set<Resource>()
                .Where(ExpressionBuilder.BuildWhere<Resource>(resourceFilterDto))
                .OrderBy(x => x.CreatedDate)
                .Select(x => new PhotoResourceDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Path = x.Path
                })
                .ToList();
        }

        private IList<PhotoResourceDto> FindAllToUserDefinableOwner(Guid userDefinableOwnerId)
        {
            return _modelContext.Set<Resource>()
                .Where(x => x.UserDefinableId == _modelContext.Set<Gallery>().Where(y => y.OwnerId == userDefinableOwnerId).Select(y => y.Id).FirstOrDefault())
                .OrderBy(x => x.CreatedDate)
                .Select(x => new PhotoResourceDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Path = x.Path
                })
                .ToList();
        }
    }
}
