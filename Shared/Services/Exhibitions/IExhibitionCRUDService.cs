using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Dtos.Exhibitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Exhibitions
{
    public interface IExhibitionCRUDService : ICRUDService<ExhibitionDto>, IPagedListAdministrationReaderService<ExhibitionDto, BaseFilterDto>
    {
    }
}
