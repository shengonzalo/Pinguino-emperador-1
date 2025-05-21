GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[UserRole] WHERE [UserId] = 1 AND [RoleId] = 1)
INSERT [dbo].[UserRole] ([UserId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[UserRole] WHERE [UserId] = 2 AND [RoleId] = 1)
INSERT [dbo].[UserRole] ([UserId], [RoleId], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 1, 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO