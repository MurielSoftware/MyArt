using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    public interface ICheckedListReaderService<T> where T : CheckedDto
    {
        List<T> ReadCheckedDto(Guid id);
    }
}
