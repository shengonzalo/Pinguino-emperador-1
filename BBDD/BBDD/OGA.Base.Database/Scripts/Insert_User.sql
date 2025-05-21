GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[User] WHERE [UserId] = 1)
INSERT [dbo].[User] ([UserId], [Name], [IdentifierTypeId], [Identifier], [Surname], [SecondSurname], [PhoneNumber], [LanguageId], [ActiveChk], [Email], [PasswordHash], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (1, 'Admin', 1, '98765067P', 'Admin', 'Admin', '999999999', 2, 1, 'admin@company.com', 'AQAAAAEAACcQAAAAENaFF6oIC+426DSZjbybhxmMNtpt+TqlGWESUQ5+l27ZiO+R7ExXQFFGadH9jH+eUQ==', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[User] WHERE [UserId] = 2)
INSERT [dbo].[User] ([UserId], [Name], [IdentifierTypeId], [Identifier], [Surname], [SecondSurname], [PhoneNumber], [LanguageId], [ActiveChk], [Email], [PasswordHash], [Enabled], [IUser], [IDate], [IComments], [UUser], [UDate], [UComments]) 
VALUES (2, 'Postman', 1, '98765067P', 'Postman', 'Postman', '999999999', 2, 1, 'postman@company.com', 'AQAAAAEAACcQAAAAENaFF6oIC+426DSZjbybhxmMNtpt+TqlGWESUQ5+l27ZiO+R7ExXQFFGadH9jH+eUQ==', 1, '1', GETDATE(), 'Carga inicial', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO