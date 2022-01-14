CREATE PROCEDURE [tenants].[del_Tenant] @TenantId UNIQUEIDENTIFIER
AS
DECLARE @Count INT;

SELECT @Count = COUNT(*)
FROM [tenants].[Tenants]
WHERE [Id] = @TenantId;

IF @Count > 0
	DELETE
	FROM [tenants].[Tenants]
	WHERE [Id] = @TenantId;
ELSE
	THROW 60000
		,'No tenant with id'
		,5;

RETURN 0
