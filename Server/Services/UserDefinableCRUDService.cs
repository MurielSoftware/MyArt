using Server.Model;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;

namespace Server.Services
{
    public class UserDefinableCRUDService<T, U> : GenericCRUDService<T, U>
        where T : UserDefinableDto
        where U : UserDefinable
    {
        public UserDefinableCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
