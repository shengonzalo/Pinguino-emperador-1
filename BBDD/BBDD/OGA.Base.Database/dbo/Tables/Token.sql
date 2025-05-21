CREATE TABLE [dbo].[Token] (
    [TokenId]     INT           IDENTITY (1, 1) NOT NULL,
    [Email]       VARCHAR (256) NOT NULL,
    [AccessToken] VARCHAR (MAX) NOT NULL,
    [ExpiredDate] DATETIME      NOT NULL,
	[IsRefreshToken] BIT NOT NULL DEFAULT ((0)),
    [Enabled]     BIT           DEFAULT ((1)) NOT NULL,
    [IUser]       VARCHAR (25)  NOT NULL,
    [IDate]       DATETIME      NOT NULL,
    [IComments]   VARCHAR (256) NOT NULL,
    [UUser]       VARCHAR (25)  NULL,
    [UDate]       DATETIME      NULL,
    [UComments]   VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([TokenId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los tokens de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del token', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'TokenId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del token', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'AccessToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es un token de refresco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'IsRefreshToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Token', @level2type = N'COLUMN', @level2name = N'UComments';

