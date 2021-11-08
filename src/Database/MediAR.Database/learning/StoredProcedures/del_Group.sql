CREATE PROCEDURE [learning].[del_Group] @GroupId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF EXISTS (
		SELECT 1
		FROM [learning].[Groups]
		WHERE [Id] = @GroupId
			AND [TenantId] = @TenantId
		)
BEGIN
	DELETE
	FROM [learning].[StudentToGroup]
	WHERE [GroupId] = @GroupId
		AND [TenantId] = @TenantId;

	DELETE
	FROM [learning].[Groups]
	WHERE [Id] = @GroupId
		AND [TenantId] = @TenantId;
END
ELSE
	THROW 6000
		,'Group not found'
		,5;

RETURN 0
