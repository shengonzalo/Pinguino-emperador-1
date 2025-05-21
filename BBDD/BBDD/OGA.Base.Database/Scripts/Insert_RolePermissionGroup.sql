GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 1 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 2 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 3 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 4 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 5 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (5, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 6 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (6, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 7 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (7, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 8 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (8, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 9 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (9, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[RolePermissionGroup] WHERE [GroupId] = 10 AND [RoleId] = 1)
INSERT [dbo].[RolePermissionGroup] ([GroupId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (10, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO