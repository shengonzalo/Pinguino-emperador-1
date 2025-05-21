GO
SET IDENTITY_INSERT [dbo].[PermissionType] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionType] WHERE [PermissionTypeId] = 1)
INSERT [dbo].[PermissionType] ([PermissionTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'Acceso', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionType] WHERE [PermissionTypeId] = 2)
INSERT [dbo].[PermissionType] ([PermissionTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'Visibilidad', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionType] WHERE [PermissionTypeId] = 3)
INSERT [dbo].[PermissionType] ([PermissionTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 'Habilitación', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[PermissionType] WHERE [PermissionTypeId] = 4)
INSERT [dbo].[PermissionType] ([PermissionTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 'Obligatoriedad', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PermissionType] OFF
GO