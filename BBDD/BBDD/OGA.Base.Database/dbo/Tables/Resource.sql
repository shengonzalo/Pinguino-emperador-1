CREATE TABLE [dbo].[Resource] (
    [ResourceId]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (256) NOT NULL,
    [Description] VARCHAR (256) NOT NULL,
    [RootId]      INT           NOT NULL,
    [ParentId]    INT           NULL,
    [Enabled]     BIT           DEFAULT ((1)) NOT NULL,
    [IUser]       VARCHAR (25)  NOT NULL,
    [IDate]       DATETIME      NOT NULL,
    [IComments]   VARCHAR (256) NOT NULL,
    [UUser]       VARCHAR (25)  NULL,
    [UDate]       DATETIME      NULL,
    [UComments]   VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([ResourceId] ASC),
    CONSTRAINT [FK_ResourceParent_TO_Resource] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Resource] ([ResourceId]),
    CONSTRAINT [FK_ResourceRoot_TO_Resource] FOREIGN KEY ([RootId]) REFERENCES [dbo].[Resource] ([ResourceId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los elementos a representar en las pantallas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'ResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del recurso raíz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'RootId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del recurso padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Resource', @level2type = N'COLUMN', @level2name = N'UComments';

