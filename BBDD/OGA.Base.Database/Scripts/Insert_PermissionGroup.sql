GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 1 AND [PermissionId] = 1)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 2 AND [PermissionId] = 2)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 2, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 2 AND [PermissionId] = 3)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 3, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 2 AND [PermissionId] = 4)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 4, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 2 AND [PermissionId] = 5)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 5, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 2 AND [PermissionId] = 6)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 6, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 3 AND [PermissionId] = 7)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 4 AND [PermissionId] = 8)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 8, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 4 AND [PermissionId] = 9)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 9, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 4 AND [PermissionId] = 10)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 10, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 4 AND [PermissionId] = 11)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 11, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 4 AND [PermissionId] = 12)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 12, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 5 AND [PermissionId] = 13)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (5, 13, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 6 AND [PermissionId] = 14)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (6, 14, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 6 AND [PermissionId] = 15)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (6, 15, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 6 AND [PermissionId] = 16)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (6, 16, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 7 AND [PermissionId] = 17)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (7, 17, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 8 AND [PermissionId] = 18)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (8, 18, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionGroup] WHERE [GroupId] = 9 AND [PermissionId] = 19)
INSERT [dbo].[PermissionGroup] ([GroupId], [PermissionId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (9, 19, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO