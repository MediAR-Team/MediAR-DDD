CREATE PROCEDURE [learning].[ins_SubmissionMark]
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
