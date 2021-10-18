GO
PRINT N'Creating Procedure [membership].[sel_Roles_with_Permissions]...';


GO
CREATE PROCEDURE [membership].[sel_Roles_with_Permissions] @Offset INT = 0
	,@Next INT
AS
SELECT [Role].[Id] AS [Id]
	,[Role].[Name] AS [Name]
	,[Role].[Description] AS [Description]
	,[Permission].[Name] AS [Name]
	,[Permission].[Description] AS [Description]
FROM (
	SELECT [Role].[Id]
		,[Role].[Name]
		,[Role].[Description]
	FROM [membership].[Roles] [Role]
	ORDER BY [Role].[Id] OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY
	) [Role]
JOIN [membership].[RolesToPermissions] [RTP] ON [RTP].[RoleId] = [Role].[Id]
JOIN [membership].[Permissions] [Permission] ON [Permission].[Id] = [RTP].[PermissionId]

RETURN 0
GO
PRINT N'Update complete.';


GO