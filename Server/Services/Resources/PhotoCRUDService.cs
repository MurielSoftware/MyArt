using Server.Model;
using Shared.Dtos.Resources;
using Shared.Services.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;

namespace Server.Services.Resources
{
    public class PhotoCRUDService : GenericCRUDService<PhotoResourceDto, Resource>, IPhotoCRUDService
    {
        public PhotoCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
