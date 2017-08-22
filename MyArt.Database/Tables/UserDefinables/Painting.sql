﻿CREATE TABLE [dbo].[PAINTING]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Title] NVARCHAR(MAX) NOT NULL,
	[Date] DATETIME NOT NULL,
	[Surface] INT NOT NULL,
	[Technique] INT NOT NULL,
	[Width] FLOAT NULL,
	[Height] FLOAT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Info] NVARCHAR(MAX) NULL,
	[Owner] NVARCHAR(MAX) NULL,
	[CollectionId] UNIQUEIDENTIFIER NULL,
)