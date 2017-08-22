using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Dtos.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Collections
{
    public interface ICollectionCRUDService : ICRUDService<CollectionDto>, IPagedListAdministrationReaderService<CollectionDto, BaseFilterDto>, IListReferenceService<BaseFilterDto>
    {
    }
}
