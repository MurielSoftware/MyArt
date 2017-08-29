using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    public interface IListAdministrationReaderService<T, U>
        where T : BaseDto
        where U : BaseFilterDto
    {
        /// <summary>
        /// Reads the entities by default SQL query.
        /// </summary>
        /// <param name="baseFilterDto">Attribute for filtering</param>
        /// <returns>The found entities</returns>
        IList<T> ReadAdministrationAll(U baseFilterDto);
    }
}
