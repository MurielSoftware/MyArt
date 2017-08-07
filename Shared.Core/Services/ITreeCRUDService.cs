using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Services
{
    public interface ITreeCRUDService<T> : ICRUDService<T>
        where T : BaseDto
    {
        /// <summary>
        /// Gets the ID of the parent node to the child ID.
        /// </summary>
        /// <param name="id">The ID of the child node</param>
        /// <returns>The ID of the parent node or null if parent does not exist</returns>
        Guid? GetParentId(Guid id);

        /// <summary>
        /// Changes the positions of the two tree nodes.
        /// </summary>
        /// <param name="sourceId">Selected ID</param>
        /// <param name="targetId">Second ID to change the position</param>
        void TreeNodeChangePosition(Guid sourceId, Guid targetId);
    }
}
