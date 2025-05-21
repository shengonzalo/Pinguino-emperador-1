CREATE TABLE [dbo].[TokenConfig] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Minute]       INT NULL,
    [Enabled] bit NOT NULL DEFAULT 1,
    [IUser] nvarchar(25) NULL,
    [IDate] datetime2 NOT NULL,
    [IComments] nvarchar(256) NULL,
    [UUser] nvarchar(25) NULL,
    [UDate] datetime2 NULL,
    [UComments] nvarchar(256) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Minutos para que el token sea invalido y te saque de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'Minute';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'Enabled';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'IUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'IDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'IComments';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'UUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'UDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TokenConfig', @level2type = N'COLUMN', @level2name = N'UComments';