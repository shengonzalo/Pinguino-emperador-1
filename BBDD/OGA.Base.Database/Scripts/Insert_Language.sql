GO
SET IDENTITY_INSERT [dbo].[Language] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Language] WHERE [LanguageId] = 1)
INSERT [dbo].[Language] ([LanguageId], [Name], [Code], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'INGLES', 'en', 'Ingles', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Language] WHERE [LanguageId] = 2)
INSERT [dbo].[Language] ([LanguageId], [Name], [Code], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'ESPAÑOL', 'es', 'Español', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Language] WHERE [LanguageId] = 3)
INSERT [dbo].[Language] ([LanguageId], [Name], [Code], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (3, 'ALEMAN', 'de', 'Aleman', 0, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Language] WHERE [LanguageId] = 4)
INSERT [dbo].[Language] ([LanguageId], [Name], [Code], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (4, 'FRANCES', 'fr', 'Frances', 0, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Language] WHERE [LanguageId] = 5)
INSERT [dbo].[Language] ([LanguageId], [Name], [Code], [Description], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (5, 'ITALIANO', 'it', 'Italiano', 0, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Language] OFF
GO