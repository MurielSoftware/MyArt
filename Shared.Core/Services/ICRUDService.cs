using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    /// <summary>
    /// Interface for all CRUD Services.
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    public interface ICRUDService<T>  : IService
        where T : BaseDto
    {
        /// <summary>
        /// Reads the entity.
        /// </summary>
        /// <param name="id">The ID of the entity</param>
        /// <returns>The found entity</returns>
        T Read(Guid id);

        /// <summary>
        /// Persists the entity to the database.
        /// </summary>
        /// <param name="entity">The entity to persist</param>
        /// <returns>The persisted entity</returns>
        T Persist(T entity);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        void Delete(Guid id);

        /// <summary>
        /// Disposes the database.
        /// </summary>
        void Dispose();
    }
}
