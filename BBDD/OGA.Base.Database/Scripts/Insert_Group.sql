GO
SET IDENTITY_INSERT [dbo].[Group] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 1)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'base_menu_read_group', '[MENÚ] Permisos básicos de visualización sobre elementos del menú', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 2)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'base_menu_admin_group', '[MENÚ] Permisos avanzados de visualización sobre elementos del menú', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 3)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 'base_users_read_group', '[USUARIOS] Permisos solo lectura sobre la pantalla de usuarios', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 4)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 'base_users_admin_group', '[USUARIOS] Permisos administración sobre la pantalla de usuarios', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 5)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (5, 'base_roles_read_group', '[ROLES] Permisos solo lectura sobre la pantalla de roles', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 6)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (6, 'base_roles_admin_group', '[ROLES] Permisos administración sobre la pantalla de roles', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 7)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (7, 'base_perms_read_group', '[PERMS] Permisos solo lectura sobre la pantalla de permisos', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 8)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (8, 'base_perms_admin_group', '[PERMS] Permisos administración sobre la pantalla de permisos', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 9)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (9, 'base_logs_read_group', '[LOGS] Permisos solo lectura sobre la pantalla de logs de eventos', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Group] WHERE [GroupId] = 10)
INSERT [dbo].[Group] ([GroupId], [Name], [Description], [Allowed], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (10, 'base_logs_admin_group', '[LOGS] Permisos administración sobre la pantalla de logs de eventos', 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Group] OFF
GO