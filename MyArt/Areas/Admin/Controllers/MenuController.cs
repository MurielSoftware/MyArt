using Client.Core.AfterSaves;
using Client.Core.Constants;
using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Dtos.MenuItems;
using Shared.Core.Messages;
using Shared.Core.Services;
using Shared.I18n.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyArt.Areas.Admin.Controllers
{
    public class MenuController : DialogTreeCRUDController<MenuItemDto, IMenuCRUDService>
    {
        public override ActionResult CreatePredefined(MenuItemDto menuItemDto)
        {
            GetTempDataManager().SetTempData(TempDataConstants.DTO, menuItemDto);
            //TempData[TempDataConstants.MENU_ITEM_DTO] = menuItemDto;
            if (menuItemDto != null)
            {
                menuItemDto.ParentMenuItemId = menuItemDto.ParentMenuItemId;
                return PartialView(WebConstants.VIEW_CREATE, menuItemDto);
            }
            return PartialView(WebConstants.VIEW_CREATE, new MenuItemDto());
        }

        [HttpPost, ValidateInput(false)]
        public override ActionResult Create(MenuItemDto menuItemDto)
        {
            return DoCreate(menuItemDto, AfterSuccessSaveParam.Create(menuItemDto.Id, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_SAVE_SUCCESS_MESSAGE), WebConstants.VIEW_LIST, WebConstants.CONTROLLER_MENU, null, HtmlConstants.TREE_MENU_ITEM));
        }

        public ActionResult DeleteConfirmed(MenuDeletionDto menuDeletionDto)
        {
            menuDeletionDto.ParentMenuItemId = GetService().GetParentId(menuDeletionDto.Id);
            return DoDeleteConfirmed(AfterDeleteParam.Create(menuDeletionDto, Message.CreateSuccessMessage(MessageKeyConstants.INFO_OBJECT_DELETED_SUCCESS_MESSAGE), WebConstants.VIEW_LIST, WebConstants.CONTROLLER_MENU, null, HtmlConstants.TREE_MENU_ITEM));
        }

        public ActionResult List(BaseFilterDto baseFilterDto)
        {
            return PartialView(GetService().GetMenuItems(null));
        }

        public ActionResult ExpandTreeItem(Guid parentId)
        {
            return PartialView(WebConstants.VIEW_LIST, GetService().GetMenuItems(parentId));
        }

        public ActionResult OnInitDialog(Guid elementId)
        {
            MenuItemDto menuItemDto = GetService().Read(elementId);
            GetTempDataManager().SetTempData(TempDataConstants.DTO, menuItemDto);
            return AssociationTypeChanged(menuItemDto.AssociationType);
        }

        public ActionResult AssociationTypeChanged(MenuItemAssociationType currentValue)
        {
            MenuItemDto menuItemDto = GetTempDataManager().GetTempData<MenuItemDto>(TempDataConstants.DTO);
            menuItemDto.AssociationType = currentValue;
            GetTempDataManager().SetTempData(TempDataConstants.DTO, menuItemDto);
            string jsonMessage = string.Empty;
            switch (currentValue)
            {
                case MenuItemAssociationType.EMPTY_LINK:
                case MenuItemAssociationType.HOME:
                    break;
                case MenuItemAssociationType.LINK:
                    jsonMessage = "#linkFormGroup";
                    break;
                case MenuItemAssociationType.LINK_TO_LIST:
                    jsonMessage = "#entityTypeFormGroup";
                    break;
                case MenuItemAssociationType.LINK_TO_SPECIFIC:
                    jsonMessage = "#entityTypeFormGroup,#specificEntityFormGroup";
                    break;
            }
            //if (IsSelected(menuItemDto, MenuItemAssociationType.LINK_TO_LIST, MenuItemEntityType.BLOG))
            //{
            //    jsonMessage += ",#blogCategoryFormGroup";
            //}
            return Json(jsonMessage);
        }

        public ActionResult EntityTypeChanged(MenuItemEntityType currentValue)
        {
            string jsonMessage = string.Empty;
            MenuItemDto menuItemDto = GetTempDataManager().GetTempData<MenuItemDto>(TempDataConstants.DTO);
            menuItemDto.EntityType = currentValue;
            GetTempDataManager().SetTempData(TempDataConstants.DTO, menuItemDto);
            //if (IsSelected(menuItemDto, AssociationType.LINK_TO_LIST, MenuEntityType.BLOG))
            //{
            //    jsonMessage += "#blogCategoryFormGroup";
            //}
            return Json(jsonMessage);
        }

        public JsonResult GetByPrefix(string prefix)
        {
            MenuItemDto menuItemDto = GetTempDataManager().GetTempData<MenuItemDto>(TempDataConstants.DTO);
            if (menuItemDto == null)
            {
                menuItemDto = new MenuItemDto() { EntityType = MenuItemEntityType.USER };
            }
            GetTempDataManager().SetTempData(TempDataConstants.DTO, menuItemDto);
            return Json(GetService().GetByPrefix(menuItemDto.EntityType, prefix));
        }
    }
}