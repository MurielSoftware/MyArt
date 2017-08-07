/* User */
ALTER TABLE [dbo].[USER] ADD  CONSTRAINT [FK_User_UserDefinable] FOREIGN KEY([Id]) REFERENCES [dbo].[USER_DEFINABLE] ([Id])
GO

/* UserDefinable */
ALTER TABLE [dbo].[USER_DEFINABLE] ADD CONSTRAINT [FK_UserDefinable_User] FOREIGN KEY ([UserCreatorId]) REFERENCES [dbo].[USER] (Id)
GO

/* Menu Item */
ALTER TABLE [dbo].[MENU_ITEM] ADD CONSTRAINT [FK_MenuItem_ParentMenuItem] FOREIGN KEY ([ParentMenuItemId]) REFERENCES [dbo].[MENU_ITEM] (Id)
GO
ALTER TABLE [dbo].[MENU_ITEM] ADD CONSTRAINT [FK_MenuItem_UserDefinable] FOREIGN KEY ([UserDefinableId]) REFERENCES [dbo].[USER_DEFINABLE] (Id)
GO