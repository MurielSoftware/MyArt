using Server.Model;
using Shared.Core.Dtos.MenuItems;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using Shared.Core.Context;
using Server.Daos;
using Shared.Core.Constants;
using Shared.Core.Exceptions;
using Shared.I18n.Constants;
using Shared.Core.Dtos.References;
using Shared.Core.Dtos;
using Shared.Dtos.Exhibitions;
using Shared.Dtos.Paintings;

namespace Server.Services
{
    public class MenuCRUDService : GenericCRUDService<MenuItemDto, MenuItem>, IMenuCRUDService
    {
        private MenuDao _menuDao;
        private OrderDao _orderDao;
        private UserDao _userDao;

        public MenuCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _menuDao = new MenuDao(unitOfWork);
            _orderDao = new OrderDao(unitOfWork);
            _userDao = new UserDao(unitOfWork);
        }

        public List<ReferenceDto> GetByPrefix(MenuItemEntityType entityType, string prefix)
        {
            switch (entityType)
            {
                case MenuItemEntityType.USER:
                    return _userDao.FindByPrefix(prefix, x => new ReferenceDto() { Id = x.Id, Label = x.FirstName + " " + x.Surname });
                case MenuItemEntityType.EXHIBITION:
                    return _genericDao.FindByPrefix<Exhibition>(new ExhibitionFilterDto() { Name = prefix }, x => new ReferenceDto() { Id = x.Id, Label = x.Name });
                case MenuItemEntityType.PAINTING:
                    return _genericDao.FindByPrefix<Painting>(new PaintingFilterDto() { Title = prefix }, x => new ReferenceDto() { Id = x.Id, Label = x.Title });
            }
            return null;
        }

        public List<MenuItemDto> GetMenuItems(Guid? parentMenuItemId)
        {
            List<MenuItemDto> menuItemDtos = _menuDao.GetMenuItems(parentMenuItemId);
            foreach (MenuItemDto menuItemDto in menuItemDtos)
            {
                if (MenuItemAssociationType.SECTION_MENU.Equals(menuItemDto.AssociationType))
                {
                    menuItemDto.SectionSubmenu = _menuDao.GetMenuItems(menuItemDto.Id);
                }
            }
            return menuItemDtos;
        }

        public Guid? GetParentId(Guid id)
        {
            return _menuDao.FindParentId(id);
        }

        public void TreeNodeChangePosition(Guid sourceId, Guid targetId)
        {
            _orderDao.ChangeOrder<MenuItem>(sourceId, targetId);
        }

        //protected override void ValidationBeforePersist(MenuItemDto menuItemDto)
        //{
        //    //if (menuItemDto.ParentMenuItemId.HasValue)
        //    //{
        //    //    MenuItem parentMenuItem = _genericDao.FindTracking<MenuItem>(menuItemDto.ParentMenuItemId.Value);
        //    //    if (MenuItemAssociationType.SECTION_MENU.Equals(menuItemDto.AssociationType) && parentMenuItem.Level != 0)
        //    //    {
        //    //        throw new ValidationException(MessageKeyConstants.VALIDATION_SECTION_CAN_BE_PLACED_ONLY_UNDER_THE_MAIN_MENU_MESSAGE);
        //    //    }
        //    //    if (!MenuItemAssociationType.SECTION_MENU.Equals(menuItemDto.AssociationType) && parentMenuItem.Level == 0)
        //    //    {
        //    //        throw new ValidationException(MessageKeyConstants.VALIDATION_UNDER_THE_MAIN_MENU_THE_SECTION_MUST_BE_PLACED_MESSAGE);
        //    //    }
        //    //}
        //    //else
        //    //if()
        //    //{
        //    //    if (MenuItemAssociationType.SECTION_MENU.Equals(menuItemDto.AssociationType))
        //    //    {
        //    //        throw new ValidationException(MessageKeyConstants.VALIDATION_SECTION_CAN_BE_PLACED_ONLY_UNDER_THE_MAIN_MENU_MESSAGE);
        //    //    }
        //    //}
        //    List<MenuItemDto> sectionMenuItemsDto = _menuDao.GetMenuItems(MenuItemAssociationType.SECTION_MENU, menuItemDto.ParentMenuItemId);
        //    if (sectionMenuItemsDto.Count == 4)
        //    {
        //        throw new ValidationException(MessageKeyConstants.VALIDATION_ONLY_FOUR_SECTIONS_CAN_BE_UNDER_MAIN_MENU_MESSAGE);
        //    }

        //    base.ValidationBeforePersist(menuItemDto);
        //}

        protected override void DoDelete(DeletionDto deletionDto, MenuItem menuItem)
        {
            List<MenuItem> menuItemsToRemoved = DeleteAllIncludeChildren(menuItem);
            menuItemsToRemoved.ForEach(x => base.DoDelete(deletionDto, x));
        }

        private List<MenuItem> DeleteAllIncludeChildren(MenuItem parentMenuItem)
        {
            List<MenuItem> menuItemsToRemoved = new List<MenuItem>();
            if (parentMenuItem.SubMenuItems != null)
            {
                foreach (MenuItem menuItem in parentMenuItem.SubMenuItems)
                {
                    menuItemsToRemoved.AddRange(DeleteAllIncludeChildren(menuItem));
                }
            }
            menuItemsToRemoved.Add(parentMenuItem);
            return menuItemsToRemoved;
        }

        protected override MenuItem CreateEntity(MenuItemDto menuItemDto)
        {
            if (!EntityExists(menuItemDto))
            {
                menuItemDto.Order = _menuDao.FindMaxOrderValue(menuItemDto.ParentMenuItemId) + 1;
                if (menuItemDto.ParentMenuItemId.HasValue)
                {
                    menuItemDto.Level = _genericDao.FindTracking<MenuItem>(menuItemDto.ParentMenuItemId.Value).Level + 1;
                }
                else
                {
                    menuItemDto.Level = 0;
                }
            }
            menuItemDto.Url = GetLink(menuItemDto);
            menuItemDto.EntityType = GetEntityType(menuItemDto);
            menuItemDto.UserDefinableReference = GetUserDefinableReference(menuItemDto);
            menuItemDto.BlogCategoryId = null;
            return base.CreateEntity(menuItemDto);
        }

        private string GetLink(MenuItemDto menuItemDto)
        {
            switch (menuItemDto.AssociationType)
            {
                case MenuItemAssociationType.EMPTY_LINK:
                    return null;
                case MenuItemAssociationType.HOME:
                    return "/home";
                case MenuItemAssociationType.LINK:
                    return menuItemDto.Url;
                case MenuItemAssociationType.LINK_TO_LIST:
                    return GenerateLinkToList(menuItemDto.AssociationType, menuItemDto.EntityType, menuItemDto.BlogCategoryId);
                case MenuItemAssociationType.LINK_TO_SPECIFIC:
                    return GenerateLinkToSpecificLink(menuItemDto.EntityType, menuItemDto.UserDefinableReference);
            }
            return null;
        }

        private MenuItemEntityType GetEntityType(MenuItemDto menuItemDto)
        {
            switch (menuItemDto.AssociationType)
            {
                case MenuItemAssociationType.LINK_TO_LIST:
                case MenuItemAssociationType.LINK_TO_SPECIFIC:
                    return menuItemDto.EntityType;
            }
            return 0;
        }

        private string GenerateLinkToList(MenuItemAssociationType associationType, MenuItemEntityType menuEntityType, Guid? blogCategoryId)
        {
            return string.Format("/{0}/{1}", menuEntityType.ToString().ToLower(), WebConstants.VIEW_INDEX);
        }

        private string GenerateLinkToSpecificLink(MenuItemEntityType menuEntityType, ReferenceString userDefinableReference)
        {
            return string.Format("/{0}/{1}/{2}", menuEntityType.ToString().ToLower(), WebConstants.VIEW_DETAILS, userDefinableReference.GetId());
        }

        private ReferenceString GetUserDefinableReference(MenuItemDto menuItemDto)
        {
            switch (menuItemDto.AssociationType)
            {
                case MenuItemAssociationType.LINK_TO_SPECIFIC:
                    return menuItemDto.UserDefinableReference;
            }
            menuItemDto.UserDefinableId = null;
            return null;
        }
    }
}
