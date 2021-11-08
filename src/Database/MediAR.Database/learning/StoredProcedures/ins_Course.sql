CREATE PROCEDURE [learning].[ins_Course] @Name VARCHAR(256)
	,@Description VARCHAR(1024)
	,@BackgroundImage VARCHAR(2048)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Courses]
		WHERE [Name] = @Name
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Courses] (
		[Name]
		,[Description]
		,[BackgroundImageUrl]
		,[TenantId]
		)
	VALUES (
		@Name
		,@Description
		,@BackgroundImage
		,@TenantId
		);
ELSE
	THROW 6000
		,'Course with same name exists'
		,5;

RETURN 0
