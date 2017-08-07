using Shared.Core.Services;
using Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Users
{
    public interface IUserCRUDService : ICRUDService<UserDto>, IPagedListAdministrationReaderService<UserDto, UserFilterDto>, IReferenceService<UserFilterDto>
    {
        ///// <summary>
        ///// Changes the position of the User up.
        ///// </summary>
        ///// <param name="id">The ID of the User</param>
        //void ChangeUserPositionUp(Guid id);

        ///// <summary>
        ///// Changes the position of the User down.
        ///// </summary>
        ///// <param name="id">The ID of the User</param>
        //void ChangeUserPositionDown(Guid id);
    }
}
