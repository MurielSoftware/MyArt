using Server.Context;
using Server.Model;
using Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Daos
{
    public abstract class BaseDao
    {
        protected MyArtContext _modelContext;

        internal BaseDao(IUnitOfWork unitOfWork)
        {
            _modelContext = (MyArtContext)unitOfWork.GetContext();
        }

        protected MyArtContext GetModelContext()
        {
            return _modelContext;
        }
    }
}
