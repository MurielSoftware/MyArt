using Server.Model;
using Shared.Dtos.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Services.Galleries;
using Shared.Core.Dtos;
using Server.Services.Resources;

namespace Server.Services.Galleries
{
    public class GalleryCRUDService : GenericCRUDService<GalleryDto, Gallery>, IGalleryCRUDService
    {
        private PhotoCRUDService _photoCRUDService;

        public GalleryCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _photoCRUDService = new PhotoCRUDService(unitOfWork);
        }

        protected override void DoDelete(DeletionDto deletionDto, Gallery gallery)
        {
            List<Guid> resourceIds = gallery.Resources.Select(x => x.Id).ToList();
            resourceIds.ForEach(x => _photoCRUDService.Delete(new DeletionDto() { Id = x }));
            base.DoDelete(deletionDto, gallery);
        }
    }
}
