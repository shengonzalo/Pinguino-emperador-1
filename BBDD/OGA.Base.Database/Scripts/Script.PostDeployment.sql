/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\Insert_PermissionType.sql
:r .\Insert_IdentifierType.sql
:r .\Insert_Language.sql
:r .\Insert_Menu.sql
:r .\Insert_User.sql
:r .\Insert_Role.sql
:r .\Insert_UserRole.sql
:r .\Insert_Group.sql
:r .\Insert_Resource.sql
:r .\Insert_Permission.sql
:r .\Insert_PermissionGroup.sql
:r .\Insert_RolePermissionGroup.sql