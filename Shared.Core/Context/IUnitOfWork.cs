using System;

namespace Shared.Core.Context
{
    /// <summary>
    /// Interface for Unit of Work with database. During the transaction all CRUD services or DAOs should share the same Unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the Context.
        /// </summary>
        /// <returns>The Database Context</returns>
        IDatabaseContext GetContext();

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        void StartTransaction();

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        void EndTransaction();
    }
}
