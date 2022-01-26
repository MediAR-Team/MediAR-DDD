PRINT N'Altering Table [learning].[Courses]...';


GO
ALTER TABLE [learning].[Courses]
    ADD [OwnerStudentId] UNIQUEIDENTIFIER NULL;


GO
PRINT N'Creating Foreign Key [learning].[FK_Courses_OwnerStudent]...';


GO
ALTER TABLE [learning].[Courses] WITH NOCHECK
    ADD CONSTRAINT [FK_Courses_OwnerStudent] FOREIGN KEY ([OwnerStudentId], [TenantId]) REFERENCES [learning].[Students] ([Id], [TenantId]);


GO
PRINT N'Refreshing View [learning].[v_CourseAggregate]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[v_CourseAggregate]';


GO
PRINT N'Refreshing View [learning].[v_GroupCourses]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[v_GroupCourses]';


GO
PRINT N'Altering View [learning].[v_StudentCourses]...';


GO


ALTER VIEW [learning].[v_StudentCourses]
AS
SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,STG.StudentId AS StudentId
FROM [learning].[Courses] C
JOIN [learning].[GroupToCourse] GTC ON GTC.TenantId = C.TenantId
	AND GTC.CourseId = C.Id
JOIN [learning].[Groups] g ON G.TenantId = GTC.TenantId
	AND G.Id = GTC.GroupId
JOIN [learning].[StudentToGroup] STG ON STG.TenantId = G.TenantId
	AND STG.GroupId = G.Id
WHERE C.OwnerStudentId IS NULL

UNION ALL

SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,C.OwnerStudentId AS StudentId
FROM [learning].[Courses] C
WHERE OwnerStudentId IS NOT NULL
GO
PRINT N'Altering Procedure [learning].[add_Group_to_Course]...';


GO


ALTER PROCEDURE [learning].[add_Group_to_Course] @GroupId INT
	,@CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM learning.Groups G
		WHERE TenantId = @TenantId
			AND Id = @GroupId
		)
BEGIN
		;

	THROW 60000
		,'Group does not exist'
		,5;
END;

IF NOT EXISTS (
		SELECT 1
		FROM learning.Courses
		WHERE TenantId = @TenantId
			AND Id = @CourseId
			AND OwnerStudentId IS NULL
		)
BEGIN
		;

	THROW 60000
		,'Group does not exist'
		,5;
END;

INSERT learning.GroupToCourse (
	CourseId
	,GroupId
	,TenantId
	)
SELECT @CourseId
	,@GroupId
	,@TenantId;
GO
PRINT N'Altering Procedure [learning].[ins_Course]...';


GO


ALTER PROCEDURE [learning].[ins_Course] @Name VARCHAR(256)
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
GO
PRINT N'Refreshing Procedure [learning].[del_Course]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[del_Course]';

