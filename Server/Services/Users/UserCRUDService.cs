using Server.Model;
using Shared.Dtos.Users;
using Shared.Services.Users;
using System.Collections.Generic;
using Shared.Core.Context;
using PagedList;
using Server.Daos;
using Shared.Core.Utils;
using Shared.Core.Dtos.References;
using Shared.Core.Dtos;
using Server.Services.Galleries;
using System;

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

        public bool IsEmailUnique(string email)
        {
            return !_genericDao.Exists<User>(x => x.Email == email);
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

        protected override void DoDelete(DeletionDto deletionDto, User user)
        {
            DeleteParts(_unitOfWork, user.Id);
            base.DoDelete(deletionDto, user);
        }

        private static void DeleteParts(IUnitOfWork unitOfWork, Guid userId)
        {
            GalleryCRUDService galleryCRUDService = new GalleryCRUDService(unitOfWork);
            GenericDao genericDao = new GenericDao(unitOfWork);

            genericDao.FindIds<Gallery>(x => x.OwnerId == userId).ForEach(x => galleryCRUDService.Delete(new DeletionDto() { Id = x }));
        }
    }
}
