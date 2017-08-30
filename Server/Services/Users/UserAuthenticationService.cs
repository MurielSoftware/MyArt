using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Services.Users;
using Shared.Core.Dtos.Users;
using Shared.Dtos.Users;
using Server.Daos;
using Shared.Core.Utils;
using Shared.Core.Sessions;
using Server.Model;

namespace Server.Services.Users
{
    public class UserAuthenticationService : BaseService, IUserAuthenticationService
    {
        private UserAuthenticationDao _userAuthenticationDao;
        private GenericDao _genericDao;

        public UserAuthenticationService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _userAuthenticationDao = new UserAuthenticationDao(unitOfWork);
            _genericDao = new GenericDao(unitOfWork);
        }

        public UserAuthenticationDto Login(UserAuthenticationDto userAuthenticationDto)
        {
            UserAuthenticationDto userAuthenticationToLoginDto = _userAuthenticationDao.Find(new UserFilterDto() { Email = userAuthenticationDto.Email });
            if(userAuthenticationToLoginDto == null)
            {
                return null;
            }
            if(!PasswordUtils.Verify(userAuthenticationToLoginDto.Password, userAuthenticationDto.Password))
            {
                return null;
            }
            SessionProvider.GetInstance().AddSession(new UserSession(userAuthenticationToLoginDto));
            return userAuthenticationToLoginDto;
        }

        public bool ChangePassword(ChangePasswordDto changePasswordDto)
        {
            User user = GetLoggedUser();
            if(user == null)
            {
                return false;
            }
            user.Password = PasswordUtils.ComputeHash(changePasswordDto.NewPassword);
            return _genericDao.Persist<User>(user) != null;
        }

        private User GetLoggedUser()
        {
            UserSession userSession = SessionProvider.GetInstance().GetSession<UserSession>(UserSession.SESSION_NAME);
            if(userSession == null)
            {
                return null;
            }
            return _genericDao.Find<User>(userSession.UserAuthenticationDto.Id);
        }
    }
}
