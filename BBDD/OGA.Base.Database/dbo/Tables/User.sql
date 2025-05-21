CREATE TABLE [dbo].[User] (
    [UserId]       INT           IDENTITY (1, 1) NOT NULL,
	[Name] 				VARCHAR(100) NOT NULL,
	[IdentifierTypeId] 	INT NOT NULL,
	[Identifier] 		VARCHAR(100) NOT NULL,
	[Surname] 			VARCHAR(100) NOT NULL,
	[SecondSurname] 	VARCHAR(100) NULL,
	[PhoneNumber] 		VARCHAR(100) NULL,
	[LanguageId] 		INT NOT NULL,
	[ActiveChk]      	BIT           DEFAULT ((1)) NOT NULL,
    [IsSystem]          BIT           DEFAULT ((0)) NOT NULL,
    [Email]        VARCHAR (256) NOT NULL,
    [PasswordHash] VARCHAR (MAX) NOT NULL,
    [ApiKey]        VARCHAR (MAX) NULL,
    [Enabled]      BIT           DEFAULT ((1)) NOT NULL,
    [IUser]        VARCHAR (25)  NOT NULL,
    [IDate]        DATETIME      NOT NULL,
    [IComments]    VARCHAR (256) NOT NULL,
    [UUser]        VARCHAR (25)  NULL,
    [UDate]        DATETIME      NULL,
    [UComments]    VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [FK_Language_TO_User] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([LanguageId]),
	CONSTRAINT [FK_IdentifierType_TO_User] FOREIGN KEY ([IdentifierTypeId]) REFERENCES [dbo].[IdentifierType] ([IdentifierTypeId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los usuarios de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IdentifierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de documento del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Identifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellido 1 del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Surname';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellido 2 del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'SecondSurname';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Idioma del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'LanguageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el usuario está activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ActiveChk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraseña encriptada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PasswordHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave de API para el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ApiKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UComments';

