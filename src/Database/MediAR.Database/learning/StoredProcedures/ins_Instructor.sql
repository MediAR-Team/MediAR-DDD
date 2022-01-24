

CREATE PROCEDURE [learning].[ins_Instructor] @UserId UNIQUEIDENTIFIER
	,@UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Instructors]
		WHERE [Id] = @UserId
			AND [TenantId] = @TenantId
			OR [UserName] = @UserName
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Instructors] (
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
BEGIN
		;

	THROW 60000
		,'Instructor already exists'
		,5;
END;

