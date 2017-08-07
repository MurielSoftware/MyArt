﻿using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    /// <summary>
    /// Interface should be implemented by all services which use the referencing.
    /// </summary>
    public interface IReferenceService<T>
        where T : BaseFilterDto
    {
        /// <summary>
        /// Gets the objects by some prefix.
        /// </summary>
        /// <param name="baseFilterDto">The value for the filtering</param>
        /// <returns>Returns the list of the ReferencedDtos which was found</returns>
        List<ReferenceDto> GetByPrefix(T baseFilterDto);
    }
}
