using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Core.Dtos.MenuItems;
using Server.Model;

namespace Server.Daos
{
    public class MenuItemDao : BaseDao
    {
        public MenuItemDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Finds the menu items to the parent ID
        /// </summary>
        /// <param name="parentMenuItemId">The ID of the parent menu item</param>
        /// <returns>Returns the child menu items or root items if parent menu item ID is null</returns>
        internal List<MenuItemDto> GetMenuItems(Guid? parentMenuItemId)
        {
            return _modelContext.Set<MenuItem>()
                .Where(x => x.ParentMenuItemId == parentMenuItemId)
                .OrderBy(x => x.Order)
                .Select(x => new MenuItemDto() { Id = x.Id, Name = x.Name, Url = x.Url, BuiltIn = x.BuiltIn, HasChildren = x.SubMenuItems.Count > 0, Level = x.Level, AssociationType = x.AssociationType })
                .ToList();
        }

        internal List<MenuItemDto> GetMenuItems(MenuItemAssociationType associationType, Guid? parentMenuItemId)
        {
            return _modelContext.Set<MenuItem>()
                .Where(x => x.ParentMenuItemId == parentMenuItemId && x.AssociationType == associationType)
                .OrderBy(x => x.Order)
                .Select(x => new MenuItemDto() { Id = x.Id, Name = x.Name, Url = x.Url, BuiltIn = x.BuiltIn, HasChildren = x.SubMenuItems.Count > 0, Level = x.Level, AssociationType = x.AssociationType })
                .ToList();
        }

        /// <summary>
        /// Finds the parent ID.
        /// </summary>
        /// <param name="id">ID of the menu item</param>
        /// <returns>Returns the parent ID or null if the parent does not exists</returns>
        internal Guid? FindParentId(Guid id)
        {
            return _modelContext.Set<MenuItem>()
                .Where(x => x.Id == id)
                .Select(x => x.ParentMenuItemId)
                .SingleOrDefault();
        }

        /// <summary>
        /// Finds max order value for the parent ID. It is usually used for the new item.
        /// </summary>
        /// <param name="parentMenuItemId">The parent ID</param>
        /// <returns>The max order value</returns>
        internal int FindMaxOrderValue(Guid? parentMenuItemId)
        {
            return _modelContext.Set<MenuItem>()
                .Where(x => x.ParentMenuItemId == parentMenuItemId)
                .Max(x => x.Order);
        }
    }
}
