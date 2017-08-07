using Shared.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos
{
    /// <summary>
    /// The base filter DTO. All search DTOs should extend it.
    /// </summary>
    public class BaseFilterDto
    {
        public virtual int Page { get; set; }
        public virtual int PageSize { get; set; }

        public BaseFilterDto()
        {
            Page = 1;
            PageSize = SharedConstants.PAGE_SIZE;
        }
    }
}
