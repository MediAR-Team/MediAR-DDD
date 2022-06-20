

CREATE PROCEDURE [learning].[add_Group_to_Course]
	@GroupId INT,
	@CourseId INT,
	@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (SELECT 1
FROM learning.Groups G
WHERE TenantId = @TenantId
	AND Id = @GroupId)
BEGIN
	;
	THROW 60000,'Group does not exist',5;
END;

IF NOT EXISTS (
		SELECT 1
FROM learning.Courses
WHERE TenantId = @TenantId
	AND Id = @CourseId
	AND OwnerStudentId IS NULL)
BEGIN
	;

	THROW 60000,'Course does not exist',5;
END;

IF EXISTS ( SELECT 1
FROM learning.GroupToCourse
WHERE TenantId = @TenantId
	AND GroupId = @GroupId
	AND CourseId = @CourseId
		)
BEGIN
	;

	THROW 60000,'Already binded',5;
END;

INSERT learning.GroupToCourse
	(
	CourseId
	,GroupId
	,TenantId
	)
SELECT @CourseId
	, @GroupId
	, @TenantId;

