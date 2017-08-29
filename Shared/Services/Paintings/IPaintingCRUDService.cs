using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Dtos.Paintings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Paintings
{
    public interface IPaintingCRUDService : ICRUDService<PaintingDto>, IPagedListAdministrationReaderService<PaintingDto, PaintingFilterDto>, IListAdministrationReaderService<PaintingDto, PaintingFilterDto>, ICheckedListReaderService<PaintingCheckedDto>
    {
    }
}
