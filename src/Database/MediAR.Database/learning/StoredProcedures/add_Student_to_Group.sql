CREATE PROCEDURE [learning].[add_Student_to_Group] @StudentId UNIQUEIDENTIFIER
	,@GroupId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF EXISTS (
		SELECT 1
		FROM [learning].[Students]
		WHERE [Id] = @StudentId
			AND [TenantId] = @TenantId
		)
	AND EXISTS (
		SELECT 1
		FROM [learning].[Groups]
		WHERE [Id] = @GroupId
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[StudentToGroup] (
		[StudentId]
		,[GroupId]
		,[TenantId]
		)
	VALUES (
		@StudentId
		,@GroupId
		,@TenantId
		);
ELSE
	THROW 6000
		,'No student or group with such id'
		,5;

RETURN 0
