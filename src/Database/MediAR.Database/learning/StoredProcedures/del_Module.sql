CREATE PROCEDURE [learning].[del_Module] @ModuleId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DELETE [learning].[ContentEntries]
WHERE ModuleId = @ModuleId
	AND TenantId = @TenantId

DELETE [learning].[Modules]
WHERE Id = @ModuleId
	AND TenantId = @TenantId
