﻿ALTER TABLE [dbo].[USER] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[COLLECTION] ADD CONSTRAINT [PK_COLLECTION] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[ROLE] ADD CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[MENU_ITEM] ADD CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[RESOURCE] ADD CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[USER_DEFINABLE] ADD CONSTRAINT [PK_UserDefinable] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[EXHIBITION] ADD CONSTRAINT [PK_Exhibition] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[PAINTING] ADD CONSTRAINT [PK_Painting] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [dbo].[JOIN_PAINTING_EXHIBITION] ADD CONSTRAINT [PK_Join_Painting_Exhibition] PRIMARY KEY CLUSTERED ([PaintingId], [ExhibitionId] ASC)
GO
ALTER TABLE [dbo].[JOIN_USER_PAINTING] ADD CONSTRAINT [PK_Join_User_Painting] PRIMARY KEY CLUSTERED ([UserId], [PaintingId] ASC)
GO
ALTER TABLE [dbo].[GALLERY] ADD CONSTRAINT [PK_GALLERY] PRIMARY KEY CLUSTERED ([Id] ASC)
GO