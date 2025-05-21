CREATE TABLE [dbo].[User2FACode] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [IdUser]        INT           NOT NULL,
    [Code]          VARCHAR (25)  NOT NULL,
    [EndDate]       DATETIME      NOT NULL,
    [Enabled]      BIT           DEFAULT ((1)) NOT NULL,
    [IUser]        VARCHAR (25)  NOT NULL,
    [IDate]        DATETIME      NOT NULL,
    [IComments]    VARCHAR (256) NOT NULL,
    [UUser]        VARCHAR (25)  NULL,
    [UDate]        DATETIME      NULL,
    [UComments]    VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User2FACode_User_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([UserId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los códigos 2FA para los usuarios de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de caducidad del código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User2FACode', @level2type = N'COLUMN', @level2name = N'UComments';

