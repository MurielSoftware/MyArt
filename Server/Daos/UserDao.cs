using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Dtos.Users;
using PagedList;
using Server.Model;
using System.Linq.Expressions;
using Shared.Core.Dtos;
using Shared.Core.Context.Expressions;

namespace Server.Daos
{
    internal class UserDao : BaseDao
    {
        internal UserDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Finds paged users.
        /// </summary>
        /// <param name="userFilterDto">The filtering DTO</param>
        /// <returns>The User DTO page</returns>
        internal IPagedList<UserDto> FindPaged(UserFilterDto userFilterDto)
        {
            return _modelContext.Set<User>()
                .Where(ExpressionBuilder.BuildWhere<User>(userFilterDto))
                .OrderBy(x => x.Surname)
                .Select(x => new UserDto() {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Email = x.Email,
                    PaintingsCount = x.Paintings.Count
                })
                .ToPagedList(userFilterDto.Page, userFilterDto.PageSize);
        }

        /// <summary>
        /// Finds the user by prefix which is 
        /// </summary>
        /// <param name="prefix">The prefix for search the users</param>
        /// <param name="selector">The specific selector</param>
        /// <returns>The list of the referenced DTOs</returns>
        internal List<ReferenceDto> FindByPrefix(string prefix, Expression<Func<User, ReferenceDto>> selector)
        {
            return _modelContext.Set<User>()
                .Where(x => x.FirstName.Contains(prefix) || x.Surname.Contains(prefix))
                .Select(selector)
                .ToList();
        }

        ///// <summary>
        ///// Finds max order value.
        ///// </summary>
        ///// <returns>The max value of the order column</returns>
        //internal int FindMaxOrderValue()
        //{
        //    return _modelContext.Set<User>()
        //        .Max(x => x.Order);
        //}

        ///// <summary>
        ///// Finds the User with the greater order than the specified ID.
        ///// </summary>
        ///// <param name="id">The ID of the User</param>
        ///// <param name="order">The order of the User</param>
        ///// <returns>The first User with the greater order.</returns>
        //internal User FindUserWithGreaterOrder(Guid id, int order)
        //{
        //    return _modelContext.Set<User>()
        //        .Where(x => x.Order > order)
        //        .OrderBy(x => x.Order)
        //        .FirstOrDefault();
        //}

        ///// <summary>
        ///// Finds the User with the lower order than the specified ID.
        ///// </summary>
        ///// <param name="id">The ID of the User</param>
        ///// <param name="order">The order of the User</param>
        ///// <returns>The first Use rwith the lower order</returns>
        //internal User FindUserWithLowerOrder(Guid id, int order)
        //{
        //    return _modelContext.Set<User>()
        //        .Where(x => x.Order < order)
        //        .OrderByDescending(x => x.Order)
        //        .FirstOrDefault();
        //}
    }
}
