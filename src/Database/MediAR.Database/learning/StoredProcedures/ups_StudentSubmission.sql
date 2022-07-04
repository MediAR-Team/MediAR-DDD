

CREATE PROCEDURE [learning].[ups_StudentSubmission]
	@TenantId UNIQUEIDENTIFIER,
	@StudentId UNIQUEIDENTIFIER,
	@EntryId INT,
	@Data XML,
	@ContentEntryType INT
AS
IF NOT EXISTS (
		SELECT 1
FROM learning.ContentEntries
WHERE Id = @EntryId
	AND TenantId = @TenantId
	AND TypeId = @ContentEntryType
		)
BEGIN
	;

	THROW 60000
		,'No content entry found or type mismatch'
		,5;
END;

IF NOT EXISTS (
		SELECT 1
FROM learning.Students
WHERE TenantId = @TenantId
	AND Id = @StudentId
		)
BEGIN
	;

	THROW 60000
		,'User is not a student'
		,5;
END;

IF NOT EXISTS (
	SELECT 1
FROM learning.ContentEntries ce
	JOIN learning.Modules m ON m.TenantId = ce.TenantId AND ce.ModuleId = m.Id
	JOIN learning.Courses c ON c.TenantId = m.TenantId AND m.CourseId = c.Id
	JOIN learning.GroupToCourse gtc ON gtc.TenantId = c.TenantId AND gtc.CourseId = c.Id
	JOIN StudentToGroup stg ON stg.GroupId = gtc.GroupId AND gtc.TenantId = stg.TenantId
WHERE ce.Id = @EntryId
	AND (c.OwnerStudentId = @StudentId OR stg.StudentId = @StudentId)
)
BEGIN
	;

	THROW 60000
		,'You have no permission to submit to this course'
		,5;
END;


IF EXISTS (
		SELECT 1
FROM learning.StudentSubmissions
WHERE EntryId = @EntryId
	AND TenantId = @TenantId
	AND StudentId = @StudentId
		)
	UPDATE learning.StudentSubmissions
	SET [Data] = @Data,
	ChangedAt = GETUTCDATE()
	WHERE EntryId = @EntryId
	AND TenantId = @TenantId
	AND StudentId = @StudentId;
ELSE
	INSERT learning.StudentSubmissions
	(
	StudentId
	,TenantId
	,EntryId
	,[Data]
	,CreatedAt
	)
SELECT @StudentId
		, @TenantId
		, @EntryId
		, @Data
		, GETUTCDATE();

