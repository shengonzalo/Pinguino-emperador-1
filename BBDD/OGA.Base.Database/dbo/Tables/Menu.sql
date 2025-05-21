CREATE TABLE [dbo].[Menu] (
    [MenuId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (256) NOT NULL,
    [Description] VARCHAR (256) NOT NULL,
    [Icon]        VARCHAR (25)  NOT NULL,
    [RootId]      INT           NOT NULL,
    [ParentId]    INT           NULL,
	[ElementId] 	VARCHAR (256) NOT NULL,
    [Enabled]     BIT           DEFAULT ((1)) NOT NULL,
	[Path]		VARCHAR (256),
	[Order]		INT           NOT NULL,
    [IUser]       VARCHAR (25)  NOT NULL,
    [IDate]       DATETIME      NOT NULL,
    [IComments]   VARCHAR (256) NOT NULL,
    [UUser]       VARCHAR (25)  NULL,
    [UDate]       DATETIME      NULL,
    [UComments]   VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([MenuId] ASC),
    CONSTRAINT [FK_MenuParent_TO_Menu] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Menu] ([MenuId]),
    CONSTRAINT [FK_MenuRoot_TO_Menu] FOREIGN KEY ([RootId]) REFERENCES [dbo].[Menu] ([MenuId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el menu a representar en la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del menú', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'MenuId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del menú', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del menú', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del menú raíz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'RootId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del menú padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del elemento HTML', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'ElementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta de la pantalla con la que enlaza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'Path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden que ocupa el elemento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'Order';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Menu', @level2type = N'COLUMN', @level2name = N'UComments';

