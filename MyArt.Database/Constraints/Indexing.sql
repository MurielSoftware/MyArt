/* User */
CREATE NONCLUSTERED INDEX [IX_User_RoleId] ON [dbo].[USER] ([RoleId])
GO

/* Painting */
CREATE NONCLUSTERED INDEX [IX_PAINTING_COLLECTION_ID] ON [dbo].[PAINTING] ([CollectionId])
GO

/* UserDefinable */
CREATE NONCLUSTERED INDEX [IX_UserDefinable_User] ON [dbo].[USER_DEFINABLE] ([UserCreatorId])
GO

/* PaintingExhibition */
CREATE NONCLUSTERED INDEX [IX_Join_Painting_Exhibition_PaintingId] ON [dbo].[JOIN_PAINTING_EXHIBITION] ([PaintingId])
GO
CREATE NONCLUSTERED INDEX [IX_Join_Painting_Exhibition_ExhibitionId] ON [dbo].[JOIN_PAINTING_EXHIBITION] ([ExhibitionId])
GO
/* UserPainting */
CREATE NONCLUSTERED INDEX [IX_Join_User_Painting_UserId] ON [dbo].[JOIN_USER_PAINTING] ([UserId])
GO
CREATE NONCLUSTERED INDEX [IX_Join_User_Painting_PaintingId] ON [dbo].[JOIN_USER_PAINTING] ([PaintingId])
GO

/* MenuItem */
CREATE NONCLUSTERED INDEX [IX_MenuItem_ParentMenuItem] ON [dbo].[MENU_ITEM] ([ParentMenuItemId])
GO
CREATE NONCLUSTERED INDEX [IX_MenuItem_UserDefinable] ON [dbo].[MENU_ITEM] ([UserDefinableId])
GO

/* Resources */
CREATE NONCLUSTERED INDEX [IX_GALLERY_USER_DEFINABLE] ON [dbo].[RESOURCE] ([UserDefinableId])
GO

/* Gallery */
CREATE NONCLUSTERED INDEX [IX_GALLERY_OWNER] ON [dbo].[GALLERY] ([OwnerId])
GO
CREATE NONCLUSTERED INDEX [IX_GALLERY_COVER_PHOTO] ON [dbo].[GALLERY] ([CoverPhotoId])
GO