GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Role] WHERE [RoleId] = 1)
INSERT [dbo].[Role] ([RoleId], [Name], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'ADMINISTRADOR', 'Rol de administrador', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO