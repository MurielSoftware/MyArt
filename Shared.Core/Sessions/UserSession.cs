using Shared.Core.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Sessions
{
    public class UserSession : AbstractSession
    {
        public const string SESSION_NAME = "UserSession";

        public UserAuthenticationDto UserAuthenticationDto { private set; get; }

        public UserSession(UserAuthenticationDto userAuthenticationDto)
            : base(SESSION_NAME)
        {
            UserAuthenticationDto = userAuthenticationDto;
        }
    }
}
