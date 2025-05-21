CREATE TABLE [dbo].[RolePermissionGroup] (
    [GroupId]   INT           NOT NULL,
    [RoleId]    INT           NOT NULL,
    [Enabled]   BIT           DEFAULT ((1)) NOT NULL,
    [IUser]     VARCHAR (25)  NOT NULL,
    [IDate]     DATETIME      NOT NULL,
    [IComments] VARCHAR (256) NOT NULL,
    [UUser]     VARCHAR (25)  NULL,
    [UDate]     DATETIME      NULL,
    [UComments] VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([GroupId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_Group_TO_RolePermissionGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([GroupId]),
    CONSTRAINT [FK_Role_TO_RolePermissionGroup] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena las relaciones entre los grupos y los roles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del grupo de permisos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'RoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RolePermissionGroup', @level2type = N'COLUMN', @level2name = N'UComments';

