using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Dtos;
using Server.Model;
using Shared.Core.Dtos.Roles;
using PagedList;
using Shared.Core.Context.Expressions;

namespace Server.Daos
{
    public class RoleDao : BaseDao
    {
        public RoleDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        internal ListReferenceDto FindAllReferences(BaseFilterDto baseFilterDto)
        {
            Dictionary<Guid, string> references = _modelContext.Set<Role>()
                .Where(ExpressionBuilder.BuildWhere<Role>(baseFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new { Id = x.Id, Name = x.Name })
                .ToDictionary(x => x.Id, x => x.Name);
            return new ListReferenceDto() { References = references };
        }

        /// <summary>
        /// Finds paged users.
        /// </summary>
        /// <param name="userFilterDto">The filtering DTO</param>
        /// <returns>The User DTO page</returns>
        internal IPagedList<RoleDto> FindPaged(BaseFilterDto baseFilterDto)
        {
            return _modelContext.Set<Role>()
                .OrderBy(x => x.Name)
                .Select(x => new RoleDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreateUpdateDeleteAll = x.CreateUpdateDeleteAll,
                    MenuCreation = x.MenuCreation,
                    RoleCreation = x.RoleCreation,
                    UserCreation = x.UserCreation
                })
                .ToPagedList(baseFilterDto.Page, baseFilterDto.PageSize);
        }
    }
}
