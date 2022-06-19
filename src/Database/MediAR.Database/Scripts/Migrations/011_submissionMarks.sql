GO
/*
The column [learning].[SubmissionMarks].[InstructorId] on table [learning].[SubmissionMarks] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [learning].[SubmissionMarks])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping SqlForeignKeyConstraint [learning].[FK_SubmissionMarks_Submissions]...';


GO
ALTER TABLE [learning].[SubmissionMarks] DROP CONSTRAINT [FK_SubmissionMarks_Submissions];


GO
PRINT N'Starting rebuilding table [learning].[SubmissionMarks]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [learning].[tmp_ms_xx_SubmissionMarks] (
    [SubmissionId] INT              NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [InstructorId] UNIQUEIDENTIFIER NOT NULL,
    [MarkValue]    INT              NOT NULL,
    [Comment]      VARCHAR (512)    NULL,
    [CreatedAt]    DATETIME         NOT NULL,
    [ChangedAt]    DATETIME         NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_SubmissionMarks1] PRIMARY KEY CLUSTERED ([TenantId] ASC, [SubmissionId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [learning].[SubmissionMarks])
    BEGIN
        INSERT INTO [learning].[tmp_ms_xx_SubmissionMarks] ([TenantId], [SubmissionId], [MarkValue], [CreatedAt], [ChangedAt])
        SELECT   [TenantId],
                 [SubmissionId],
                 [MarkValue],
                 [CreatedAt],
                 [ChangedAt]
        FROM     [learning].[SubmissionMarks]
        ORDER BY [TenantId] ASC, [SubmissionId] ASC;
    END

DROP TABLE [learning].[SubmissionMarks];

EXECUTE sp_rename N'[learning].[tmp_ms_xx_SubmissionMarks]', N'SubmissionMarks';

EXECUTE sp_rename N'[learning].[tmp_ms_xx_constraint_PK_SubmissionMarks1]', N'PK_SubmissionMarks', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Creating SqlForeignKeyConstraint [learning].[FK_SubmissionMarks_Submissions]...';


GO
ALTER TABLE [learning].[SubmissionMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_SubmissionMarks_Submissions] FOREIGN KEY ([TenantId], [SubmissionId]) REFERENCES [learning].[StudentSubmissions] ([TenantId], [Id]);


GO
PRINT N'Creating SqlForeignKeyConstraint [learning].[FK_SubmissionMarks_Instructors]...';


GO
ALTER TABLE [learning].[SubmissionMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_SubmissionMarks_Instructors] FOREIGN KEY ([InstructorId], [TenantId]) REFERENCES [learning].[Instructors] ([Id], [TenantId]);


GO
PRINT N'Creating SqlProcedure [learning].[ins_SubmissionMark]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
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
) AS Parsed;

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