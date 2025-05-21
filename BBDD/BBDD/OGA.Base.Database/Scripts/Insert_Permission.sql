GO
SET IDENTITY_INSERT [dbo].[Permission] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 1)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (1, 'base_menu_visible', 'Permiso de visibilidad del recurso base_menu', 1, 2, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 2)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (2, 'base_menu_admin_visible', 'Permiso de visibilidad del recurso base_menu_admin', 1, 2, 2, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 3)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (3, 'base_menu_users_visible', 'Permiso de visibilidad del recurso base_menu_users', 1, 2, 3, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 4)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (4, 'base_menu_roles_visible', 'Permiso de visibilidad del recurso base_menu_roles', 1, 2, 4, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 5)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (5, 'base_menu_permissions_visible', 'Permiso de visibilidad del recurso base_menu_permissions', 1, 2, 5, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 6)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (6, 'base_menu_log_visible', 'Permiso de visibilidad del recurso base_menu_log', 1, 2, 6, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 7)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (7, 'base_users_access', 'Permiso de acceso del recurso base_users', 1, 1, 7, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 8)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (8, 'base_users_create_visible', 'Permiso de visibilidad del recurso base_users_create', 1, 2, 8, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 9)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (9, 'base_users_edit_visible', 'Permiso de visibilidad del recurso base_users_edit', 1, 2, 9, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 10)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (10, 'base_users_delete_visible', 'Permiso de visibilidad del recurso base_users_delete', 1, 2, 10, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 11)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (11, 'base_users_perms_create_visible', 'Permiso de visibilidad del recurso base_users_perms_create', 1, 2, 11, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 12)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (12, 'base_users_perms_delete_visible', 'Permiso de visibilidad del recurso base_users_perms_delete', 1, 2, 12, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 13)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (13, 'base_roles_access', 'Permiso de acceso del recurso base_roles', 1, 1, 13, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 14)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (14, 'base_roles_create_visible', 'Permiso de visibilidad del recurso base_roles_create', 1, 2, 14, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 15)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (15, 'base_roles_edit_visible', 'Permiso de visibilidad del recurso base_roles_edit', 1, 2, 15, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 16)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (16, 'base_roles_delete_visible', 'Permiso de visibilidad del recurso base_roles_delete', 1, 2, 16, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 17)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (17, 'base_perms_access', 'Permiso de acceso del recurso base_perms', 1, 1, 17, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 18)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (18, 'base_perms_edit_visible', 'Permiso de visibilidad del recurso base_perms_edit', 1, 2, 18, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Permission] WHERE [PermissionId] = 19)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Description], [Allowed], [PermissionTypeId], [ResourceId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments])
VALUES (19, 'base_logs_access', 'Permiso de acceso del recurso base_logs', 1, 1, 19, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO