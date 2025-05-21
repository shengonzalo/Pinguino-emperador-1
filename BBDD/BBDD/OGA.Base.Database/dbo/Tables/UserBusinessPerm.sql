CREATE TABLE [dbo].[UserBusinessPerm] (
	[UserId]    		INT         	NOT NULL,
	[BusinessEntityId]  INT				NOT NULL,
	[Identifier]		INT				NOT NULL,
	[Enabled]      		BIT         	DEFAULT ((1)) NOT NULL,
    [IUser]        		VARCHAR (25)  	NOT NULL,
    [IDate]        		DATETIME      	NOT NULL,
    [IComments]    		VARCHAR (256) 	NOT NULL,
    [UUser]        		VARCHAR (25)  	NULL,
    [UDate]        		DATETIME      	NULL,
    [UComments]    		VARCHAR (256) 	NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [BusinessEntityId] ASC, [Identifier] ASC),
	CONSTRAINT [FK_User_TO_UserBusinessPerm] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
	CONSTRAINT [FK_BusinessEntity_TO_UserBusinessPerm] FOREIGN KEY ([BusinessEntityId]) REFERENCES [dbo].[BusinessEntity] ([BusinessEntityId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena los permisos de los usuarios sobre las entidades de negocio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la entidad de negocio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'BusinessEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'Identifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro está habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'IUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'IDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'IComments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'UUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'UDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoría comentario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserBusinessPerm', @level2type = N'COLUMN', @level2name = N'UComments';