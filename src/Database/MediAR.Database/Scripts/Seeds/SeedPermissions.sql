-- TODO: fix so that instead of truncating tables we just append the new roles/permissions
DELETE FROM [membership].[RolesToPermissions];
DELETE FROM [membership].[Roles];
DELETE FROM [membership].[Permissions];

DECLARE @Data XML;
SELECT @Data = CONVERT(XML, BulkColumn)
-- Substitute with the correct absolute path before executing
FROM OPENROWSET(BULK 'C:\Users\vsabo\source\repos\MediAR-DDD\src\Database\MediAR.Database\PermissionMapping.xml', SINGLE_BLOB) as x;

INSERT INTO [membership].[Roles] ([Id], [Name], [Description])
SELECT NEWID(), X.y.value('@name', 'VARCHAR(256)'), X.y.value('@description', 'VARCHAR(512)')
FROM @Data.nodes('root/role') X(y);

WITH CTE AS (
SELECT  X.y.value('@name', 'VARCHAR(256)') AS [Name], X.y.value('@description', 'VARCHAR(512)') AS [Description]
FROM @Data.nodes('//permission') X(y)
)
INSERT INTO [membership].[Permissions] ([Id], [Name], [Description])
SELECT NEWID(), [Name], [Description]
FROM CTE
GROUP BY [Name], [Description];

INSERT INTO [membership].[RolesToPermissions] (RoleId, PermissionId)
SELECT R.Id, P.Id
FROM @Data.nodes('//permission') X(y)
JOIN [membership].[Roles] R ON X.y.value('../@name', 'VARCHAR(256)') = R.[Name]
JOIN [membership].[Permissions] P ON X.y.value('@name', 'VARCHAR(256)') = P.[Name];