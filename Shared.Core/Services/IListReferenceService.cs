using Shared.Core.Dtos;
using Shared.Core.Dtos.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    public interface IListReferenceService<T> where T : BaseFilterDto
    {
        ListReferenceDto GetAllReferences(T baseFilterDto);
    }
}
