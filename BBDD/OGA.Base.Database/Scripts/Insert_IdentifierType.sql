GO
SET IDENTITY_INSERT [dbo].[IdentifierType] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[IdentifierType] WHERE [IdentifierTypeId] = 1)
INSERT [dbo].[IdentifierType] ([IdentifierTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'NIF', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[IdentifierType] WHERE [IdentifierTypeId] = 2)
INSERT [dbo].[IdentifierType] ([IdentifierTypeId], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'NIE', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[IdentifierType] OFF
GO