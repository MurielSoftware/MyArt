INSERT INTO [dbo].[ROLE] ([Id], [Name], [UserCreation], [RoleCreation], [MenuCreation], [CreateUpdateDeleteAll], [CreatedDate], [UpdatedDate]) VALUES(N'90cfe19d-7bdc-49d1-bf6c-a5358506d99e', N'SuperAdmin', 1, 1, 1, 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)

/* USER */
INSERT INTO [dbo].[USER_DEFINABLE] ([Id], [Public], [CreatedDate], [UpdatedDate]) VALUES(N'1783fb6f-8b1c-4250-9500-ebfe43630e38', 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
INSERT INTO [dbo].[USER] ([Id], [FirstName], [Surname], [RoleId], [Email], [Description], [Password]) VALUES (N'1783fb6f-8b1c-4250-9500-ebfe43630e38', N'Jaromír', N'Krpec', N'90cfe19d-7bdc-49d1-bf6c-a5358506d99e', N'krpec.jaromir@seznam.cz', NULL, N'1000:kuRr/u5EQZuW7efGctOXAH4XHgRVw+bM:bdtUlqOfAdedpjBBvgCWH2392dAjd/aA')

