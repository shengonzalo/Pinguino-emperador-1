CREATE TABLE [dbo].[PermissionGroup] (
    [GroupId]      INT           NOT NULL,
    [PermissionId] INT           NOT NULL,
    [Enabled]      BIT           DEFAULT ((1)) NOT NULL,
    [IUser]        VARCHAR (25)  NOT NULL,
    [IDate]        DATETIME      NOT NULL,
    [IComments]    VARCHAR (256) NOT NULL,
    [UUser]        VARCHAR (25)  NULL,
    [UDate]        DATETIME      NULL,
    [UComments]    VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([GroupId] ASC, [PermissionId] ASC),
    CONSTRAINT [FK_Group_TO_PermissionGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([GroupId]),
    CONSTRAINT [FK_Permission_TO_PermissionGroup] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena las relaciones entre los grupos y los permisos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del grupo de permisos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'PermissionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PermissionGroup', @level2type = N'COLUMN', @level2name = N'UComments';

