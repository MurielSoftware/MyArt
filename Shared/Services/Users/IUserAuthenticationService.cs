using Shared.Core.Dtos.Users;
using Shared.Core.Services;
using Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Users
{
    public interface IUserAuthenticationService : IService
    {
        UserAuthenticationDto Login(UserAuthenticationDto userAuthenticationDto);
        bool ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
