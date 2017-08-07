using Shared.Core.Dtos;
using Shared.Core.Dtos.Roles;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Roles
{
    public interface IRoleCRUDService : ICRUDService<RoleDto>, IListReferenceService<BaseFilterDto>, IPagedListAdministrationReaderService<RoleDto, BaseFilterDto>
    {
    }
}
