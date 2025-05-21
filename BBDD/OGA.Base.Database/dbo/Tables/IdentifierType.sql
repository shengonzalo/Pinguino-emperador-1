CREATE TABLE [dbo].[IdentifierType] (
	[IdentifierTypeId]	INT           IDENTITY (1, 1) NOT NULL,
	[Description] 		VARCHAR (256) NOT NULL,
    [Enabled]     		BIT           DEFAULT ((1)) NOT NULL,
    [IUser]       		VARCHAR (25)  NOT NULL,
    [IDate]       		DATETIME      NOT NULL,
    [IComments]   		VARCHAR (256) NOT NULL,
    [UUser]       		VARCHAR (25)  NULL,
    [UDate]       		DATETIME      NULL,
    [UComments]   		VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([IdentifierTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los idiomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'IdentifierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IdentifierType', @level2type = N'COLUMN', @level2name = N'UComments';