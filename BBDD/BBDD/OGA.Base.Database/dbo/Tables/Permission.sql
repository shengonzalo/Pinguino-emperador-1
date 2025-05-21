CREATE TABLE [dbo].[Permission] (
    [PermissionId]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (256) NOT NULL,
    [Description]      VARCHAR (256) NOT NULL,
    [Allowed]          BIT           DEFAULT ((1)) NOT NULL,
    [PermissionTypeId] INT           NOT NULL,
    [ResourceId]       INT           NOT NULL,
    [Enabled]          BIT           DEFAULT ((1)) NOT NULL,
    [IUser]            VARCHAR (25)  NOT NULL,
    [IDate]            DATETIME      NOT NULL,
    [IComments]        VARCHAR (256) NOT NULL,
    [UUser]            VARCHAR (25)  NULL,
    [UDate]            DATETIME      NULL,
    [UComments]        VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([PermissionId] ASC),
    CONSTRAINT [FK_PermissionType_TO_Permission] FOREIGN KEY ([PermissionTypeId]) REFERENCES [dbo].[PermissionType] ([PermissionTypeId]),
    CONSTRAINT [FK_Resource_TO_Permission] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([ResourceId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los permisos a aplicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'PermissionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se permite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'Allowed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del tipo de permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'PermissionTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'ResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Permission', @level2type = N'COLUMN', @level2name = N'UComments';

