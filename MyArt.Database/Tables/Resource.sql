﻿CREATE TABLE [dbo].[RESOURCE]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[UpdatedDate] DATETIME NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Path] NVARCHAR(MAX) NOT NULL
)