using Shared.Core.Dtos.MenuItems;
using Shared.Core.Dtos.References;
using System;
using System.Collections.Generic;

namespace Shared.Core.Services
{
    public interface IMenuCRUDService : ITreeCRUDService<MenuItemDto>
    {
        /// <summary>
        /// Gets menu items to the parent item. If parent ID is null, then the root items are returned.
        /// </summary>
        /// <param name="parentMenuItemId">The ID of the parent menu item</param>
        /// <returns>The list of the menu items.</returns>
        List<MenuItemDto> GetMenuItems(Guid? parentMenuItemId);

        /// <summary>
        /// Gets the entities by prefix.
        /// </summary>
        /// <param name="entityType">The type of the entity</param>
        /// <param name="prefix">TThe prefix of the entity</param>
        /// <returns>The list of the entities to the appropriate parameters</returns>
        List<ReferenceDto> GetByPrefix(MenuItemEntityType entityType, string prefix);
    }
}
