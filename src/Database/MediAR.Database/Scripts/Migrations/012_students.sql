PRINT N'Altering SqlView [learning].[v_StudentSubmissions]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
ALTER VIEW [learning].[v_StudentSubmissions] AS
SELECT ss.Id,
	ss.EntryId,
	ss.StudentId,
	ss.TenantId,
	ss.[Data],
	ss.CreatedAt,
	ss.ChangedAt,
	sm.MarkValue AS SubmissionMarkMarkValue,
	sm.Comment AS SubmissionMarkComment,
	sm.CreatedAt AS SubmissionMarkCreatedAt,
	sm.ChangedAt AS SubmissionMarkChangedAt,
	ce.TypeId
FROM learning.StudentSubmissions ss
	LEFT JOIN learning.SubmissionMarks sm ON sm.TenantId = ss.TenantId AND sm.SubmissionId = ss.Id
	JOIN learning.ContentEntries ce
		ON ce.Id = ss.EntryId AND ce.TenantId = ss.TenantId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Altering SqlView [membership].[v_UserRoles]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
ALTER VIEW [membership].[v_UserRoles]
WITH
	SCHEMABINDING
AS
	SELECT
		u.TenantId AS TenantId,
		u.Id AS UserId,
		u.UserName AS UserName,
		u.Email AS Email,
		r.[Name] AS RoleName
	FROM [membership].[Users] u
		JOIN [membership].[UsersToRoles] utr ON utr.UserId = u.Id AND utr.TenantId = u.TenantId
		JOIN [membership].[Roles] r ON r.Id = utr.RoleId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Altering SqlView [membership].[v_Users]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
ALTER VIEW [membership].[v_Users]
	WITH SCHEMABINDING
AS
SELECT u.Id AS Id
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,u.PasswordHash AS [PasswordHash]
	,u.FirstName AS [FirstName]
	,u.LastName AS [LastName]
	,u.TenantId [TenantId]
	,r.Name AS RoleName
FROM [membership].Users u
JOIN [membership].UsersToRoles utr ON utr.TenantId = u.TenantId AND utr.UserId = u.Id
JOIN membership.Roles r ON r.Id = utr.RoleId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Creating SqlView [learning].[v_Students]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE VIEW [learning].[v_Students]
AS
  SELECT
    s.Id,
    s.TenantId,
    s.FirstName,
    s.LastName,
    s.Email,
    s.UserName
  FROM [learning].Students s
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Altering SqlProcedure [learning].[add_Group_to_Course]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO


ALTER PROCEDURE [learning].[add_Group_to_Course]
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
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Altering SqlProcedure [learning].[add_Student_to_Group]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
ALTER PROCEDURE [learning].[add_Student_to_Group]
	@StudentId UNIQUEIDENTIFIER,
	@GroupId INT,
	@TenantId UNIQUEIDENTIFIER
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
	AND NOT EXISTS (
		SELECT 1
	FROM [learning].[StudentToGroup]
	WHERE TenantId = @TenantId
		AND GroupId = @GroupId
		AND StudentId = @StudentId
	)
	INSERT INTO [learning].[StudentToGroup]
	(
	[StudentId]
	,[GroupId]
	,[TenantId]
	)
VALUES
	(
		@StudentId
		, @GroupId
		, @TenantId
		);
ELSE
	THROW 60000,'No student or group with such id',5;
RETURN 0
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Altering SqlProcedure [learning].[ins_SubmissionMark]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
ALTER PROCEDURE [learning].[ins_SubmissionMark]
    @SubmissionId int,
    @TenantId uniqueidentifier,
    @InstructorId uniqueidentifier,
    @Mark int,
    @Comment varchar(512)
AS
IF NOT EXISTS
(
    SELECT 1
FROM learning.StudentSubmissions
WHERE TenantId = @TenantId
    AND Id = @SubmissionId
)
    THROW 60000, 'Submission does not exist', 5;

DECLARE @IsExists bit = 0;

SELECT @IsExists = 1
FROM learning.SubmissionMarks
WHERE TenantId = @TenantId
    AND SubmissionId = @SubmissionId;

DECLARE @MaxMark int;

SELECT @MaxMark = Parsed.MaxMark
FROM learning.StudentSubmissions ss
    JOIN learning.ContentEntries ce
    ON ce.TenantId = ss.TenantId
        AND ce.Id = ss.EntryId
    CROSS APPLY
(
    SELECT f.n.value('(//@maxmark)[1]', 'int') MaxMark
    FROM ce.[Configuration].nodes('/') f(n)
) AS Parsed
WHERE ss.Id = @SubmissionId;

IF (@Mark > @MaxMark OR @Mark < 0)
    THROW 60000, 'Mark is higher than max', 5;

INSERT learning.SubmissionMarks
    (
    TenantId,
    SubmissionId,
    MarkValue,
    InstructorId,
    CreatedAt,
    Comment
    )
SELECT @TenantId,
    @SubmissionId,
    @Mark,
    @InstructorId,
    GETUTCDATE(),
    @Comment
WHERE @IsExists = 0;

UPDATE learning.SubmissionMarks
SET MarkValue = @Mark,
    Comment = @Comment,
    @InstructorId = @InstructorId,
    ChangedAt = GETUTCDATE()
WHERE @IsExists = 1
    AND TenantId = @TenantId
    AND SubmissionId = @SubmissionId;
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Update complete.';
