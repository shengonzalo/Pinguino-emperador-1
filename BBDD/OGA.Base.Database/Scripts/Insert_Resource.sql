GO
SET IDENTITY_INSERT [dbo].[Resource] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 1)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (1, 'base_menu', 'Menú de la aplicación', 1, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 2)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (2, 'base_menu_admin', 'Menú Administración', 1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 3)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (3, 'base_menu_users', 'Menú Usuarios', 1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 4)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (4, 'base_menu_roles', 'Menú Roles', 1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 5)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (5, 'base_menu_permissions', 'Menú Permisos', 1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 6)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (6, 'base_menu_log', 'Menú Log eventos', 1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 7)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (7, 'base_users', 'Pantalla de usuarios', 7, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 8)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (8, 'base_users_create', 'Botón creación usuarios', 7, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 9)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (9, 'base_users_edit', 'Botón edición usuarios', 7, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 10)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (10, 'base_users_delete', 'Botón borrado usuarios', 7, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 11)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (11, 'base_users_perms_create', 'Botón creación permisos', 7, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 12)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (12, 'base_users_perms_delete', 'Botón borrado permisos', 7, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 13)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (13, 'base_roles', 'Pantalla de roles', 13, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 14)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (14, 'base_roles_create', 'Botón creación roles', 13, 13, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 15)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (15, 'base_roles_edit', 'Botón edición roles', 13, 13, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 16)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (16, 'base_roles_delete', 'Botón borrado roles', 13, 13, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 17)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (17, 'base_perms', 'Pantalla de permisos', 17, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 18)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (18, 'base_perms_edit', 'Botón edición permisos', 17, 17, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Resource] WHERE [ResourceId] = 19)
INSERT [dbo].[Resource] ([ResourceId], [Name], [Description], [RootId], [ParentId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (19, 'base_logs', 'Pantalla de logs de eventos', 19, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Resource] OFF
GO