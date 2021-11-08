DECLARE @Data XML;

SELECT @Data = CONVERT(XML, BulkColumn)
-- Substitute with the correct absolute path before executing
FROM OPENROWSET(BULK 'C:\projects\MediAR-DDD\src\Database\MediAR.Database\PermissionMapping.xml', SINGLE_BLOB) AS x;

MERGE INTO [membership].[Roles] [Role]
USING @Data.nodes('root/role') X(y)
	ON X.y.value('@name', 'VARCHAR(256)') = [Role].[Name]
WHEN NOT MATCHED
	THEN
		INSERT (
			[Id]
			,[Name]
			,[Description]
			)
		VALUES (
			NEWID()
			,X.y.value('@name', 'VARCHAR(256)')
			,X.y.value('@description', 'VARCHAR(512)')
			)
WHEN MATCHED
	THEN
		UPDATE
		SET [Role].[Description] = X.y.value('@description', 'VARCHAR(512)');

WITH CTE
AS (
	SELECT DISTINCT X.y.value('@name', 'VARCHAR(256)') AS [Name]
		,X.y.value('@description', 'VARCHAR(512)') AS [Description]
	FROM @Data.nodes('//permission') X(y)
	)
MERGE INTO [membership].[Permissions] [Permission]
USING CTE
	ON CTE.[Name] = [Permission].[Name]
WHEN NOT MATCHED
	THEN
		INSERT (
			[Id]
			,[Name]
			,[Description]
			)
		VALUES (
			NEWID()
			,CTE.[Name]
			,CTE.[Description]
			)
WHEN MATCHED
	THEN
		UPDATE
		SET [Permission].[Description] = CTE.[Description];

DELETE
FROM [membership].[RolesToPermissions];

INSERT INTO [membership].[RolesToPermissions] (
	RoleId
	,PermissionId
	)
SELECT R.Id
	,P.Id
FROM @Data.nodes('//permission') X(y)
JOIN [membership].[Roles] R ON X.y.value('../@name', 'VARCHAR(256)') = R.[Name]
JOIN [membership].[Permissions] P ON X.y.value('@name', 'VARCHAR(256)') = P.[Name]
LEFT JOIN [membership].[RolesToPermissions] [existing] ON [existing].[PermissionId] = P.[Id]
	AND [existing].[RoleId] = R.[Id]
WHERE [existing].[RoleId] IS NULL;
