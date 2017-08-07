/* User */
CREATE NONCLUSTERED INDEX [IX_User_RoleId] ON [dbo].[USER] ([RoleId])
GO

/* UserDefinable */
CREATE NONCLUSTERED INDEX [IX_UserDefinable_User] ON [dbo].[USER_DEFINABLE] ([UserCreatorId])
GO

/* MenuItem */
CREATE NONCLUSTERED INDEX [IX_MenuItem_ParentMenuItem] ON [dbo].[MENU_ITEM] ([ParentMenuItemId])
GO
CREATE NONCLUSTERED INDEX [IX_MenuItem_UserDefinable] ON [dbo].[MENU_ITEM] ([UserDefinableId])
GO