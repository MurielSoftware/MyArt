using Server.Model;
using Shared.Dtos.Users;
using Shared.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using PagedList;
using Server.Daos;
using Shared.Core.Dtos;
using Shared.Core.Utils;

namespace Server.Services.Users
{
    public class UserCRUDService : UserDefinableCRUDService<UserDto, User>, IUserCRUDService
    {
        private UserDao _userDao;
        private OrderDao _orderDao;

        public UserCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _userDao = new UserDao(unitOfWork);
            _orderDao = new OrderDao(unitOfWork);
        }

        public List<ReferenceDto> GetByPrefix(UserFilterDto userFilterDto)
        {
            return _userDao.FindByPrefix(userFilterDto.Surname, x => new ReferenceDto() { Id = x.Id, Label = x.FirstName + " " + x.Surname });
        }

        public IPagedList<UserDto> ReadAdministrationPaged(UserFilterDto userFilterDto)
        {
            return _userDao.FindPaged(userFilterDto);
        }

        //public void ChangeUserPositionUp(Guid id)
        //{
        //    User sourceUser = _genericDao.FindTracking<User>(id);
        //    _orderDao.ChangeOrder(sourceUser, _userDao.FindUserWithLowerOrder(id, sourceUser.Order));
        //}

        //public void ChangeUserPositionDown(Guid id)
        //{
        //    User sourceUser = _genericDao.FindTracking<User>(id);
        //    _orderDao.ChangeOrder(sourceUser, _userDao.FindUserWithGreaterOrder(id, sourceUser.Order));
        //}

        protected override User CreateEntity(UserDto userDto)
        {
            User user = base.CreateEntity(userDto);
            if(!EntityExists(userDto))
            {
                user.Password = PasswordUtils.ComputeHash(PasswordUtils.CreateRandomPassword());
                //user.Order = _userDao.FindMaxOrderValue() + 1;
            }
            return user;
        }
    }
}
