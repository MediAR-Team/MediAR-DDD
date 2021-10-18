GO
PRINT N'Creating Procedure [tenants].[del_Tenant]...';


GO
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
	THROW 6000
		,'No tenant with id'
		,5;

RETURN 0
GO
PRINT N'Update complete.';


GO
