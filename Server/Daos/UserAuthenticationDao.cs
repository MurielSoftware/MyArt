using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Dtos.Users;
using Shared.Core.Dtos.Users;
using Server.Model;
using Shared.Core.Context.Expressions;
using Shared.Core.Dtos.Resources;

namespace Server.Daos
{
    public class UserAuthenticationDao : BaseDao
    {
        public UserAuthenticationDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        internal UserAuthenticationDto Find(UserFilterDto userFilterDto)
        {
            return _modelContext.Set<User>()
                .Where(ExpressionBuilder.BuildWhere<User>(userFilterDto))
                .Select(x => new UserAuthenticationDto() {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    RoleName = x.Role.Name,
                    Email = x.Email,
                    Password = x.Password,
                    PhotoResourceDto = x.Galleries.Where(y => (y is ProfileGallery)).Select(y => new PhotoResourceDto() {
                        Id = y.CoverPhoto.Id,
                        Name = y.CoverPhoto.Name,
                        Path = y.CoverPhoto.Path
                    }).FirstOrDefault()
                })
                .SingleOrDefault();
        }
    }
}
