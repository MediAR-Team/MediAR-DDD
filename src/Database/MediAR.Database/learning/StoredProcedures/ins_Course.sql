

CREATE PROCEDURE [learning].[ins_Course] @Name VARCHAR(256)
	,@Description VARCHAR(1024)
	,@BackgroundImage VARCHAR(2048)
	,@TenantId UNIQUEIDENTIFIER
	,@UserId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Courses]
		WHERE [Name] = @Name
			AND [TenantId] = @TenantId
		)
BEGIN
	DECLARE @OwnerStudentId UNIQUEIDENTIFIER;

	SELECT @OwnerStudentId = Id
	FROM [learning].[Students]
	WHERE TenantId = @TenantId
		AND Id = @UserId;

	INSERT INTO [learning].[Courses] (
		[Name]
		,[Description]
		,[BackgroundImageUrl]
		,[TenantId]
		,[OwnerStudentId]
		)
	VALUES (
		@Name
		,@Description
		,@BackgroundImage
		,@TenantId
		,@OwnerStudentId
		);
END
ELSE
	THROW 60000
		,'Course with same name exists'
		,5;

