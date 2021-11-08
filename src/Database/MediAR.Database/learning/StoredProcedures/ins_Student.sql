CREATE PROCEDURE [learning].[ins_Student] @UserId UNIQUEIDENTIFIER
	,@UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Students]
		WHERE [Id] = @UserId
			AND [TenantId] = @TenantId
			OR [UserName] = @UserName
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Students] (
		[Id]
		,[UserName]
		,[Email]
		,[FirstName]
		,[LastName]
		,[TenantId]
		)
	VALUES (
		@UserId
		,@UserName
		,@Email
		,@FirstName
		,@LastName
		,@TenantId
		);
ELSE
	THROW 6000
		,'Student already exists'
		,5;

RETURN 0
