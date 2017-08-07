CREATE TABLE [dbo].[ROLE]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[UpdatedDate] DATETIME NOT NULL,
	[Name] NVARCHAR (MAX) NOT NULL,
	[UserCreation] BIT NOT NULL,
	[RoleCreation] BIT NOT NULL,
	[MenuCreation] BIT NOT NULL,
	[CreateUpdateDeleteAll] BIT NOT NULL
)