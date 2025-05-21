GO
SET IDENTITY_INSERT [dbo].[Menu] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Menu] WHERE [MenuId] = 1)
INSERT [dbo].[Menu] ([MenuId], [Name], [Description], [Icon], [RootId], [ParentId], [ElementId], [Enabled], [Path], [Order], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'Administration', 'Menú Administración', 'settings', 1, NULL, 'base_menu_admin', 1, NULL, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Menu] WHERE [MenuId] = 2)
INSERT [dbo].[Menu] ([MenuId], [Name], [Description], [Icon], [RootId], [ParentId], [ElementId], [Enabled], [Path], [Order], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'Users', 'Menú Usuarios', 'supervisor_account', 1, 1, 'base_menu_users', 1, '/users', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Menu] WHERE [MenuId] = 3)
INSERT [dbo].[Menu] ([MenuId], [Name], [Description], [Icon], [RootId], [ParentId], [ElementId], [Enabled], [Path], [Order], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 'Roles', 'Menú Roles', 'widgets', 1, 1, 'base_menu_roles', 1, '/roles', 2, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Menu] WHERE [MenuId] = 4)
INSERT [dbo].[Menu] ([MenuId], [Name], [Description], [Icon], [RootId], [ParentId], [ElementId], [Enabled], [Path], [Order], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 'Permissions', 'Menú Permisos', 'lock', 1, 1, 'base_menu_permissions', 1, '/permissions', 3, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Menu] WHERE [MenuId] = 5)
INSERT [dbo].[Menu] ([MenuId], [Name], [Description], [Icon], [RootId], [ParentId], [ElementId], [Enabled], [Path], [Order], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (5, 'Event log', 'Menú Log eventos', 'description', 5, NULL, 'base_menu_log', 1, '/logs', 2, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO