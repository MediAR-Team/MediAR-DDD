PRINT N'Creating Table [learning].[StudentSubmissions]...';


GO
CREATE TABLE [learning].[StudentSubmissions] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [TenantId]  UNIQUEIDENTIFIER NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [EntryId]   INT              NOT NULL,
    [Data]      XML              NOT NULL,
    [CreatedAt] DATETIME         NOT NULL,
    [ChangedAt] DATETIME         NULL,
    CONSTRAINT [PK_StudentSubmissions] PRIMARY KEY CLUSTERED ([TenantId] ASC, [Id] ASC)
);


GO
PRINT N'Creating Table [learning].[SubmissionMarks]...';


GO
CREATE TABLE [learning].[SubmissionMarks] (
    [SubmissionId] INT              NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [MarkValue]    INT              NOT NULL,
    [CreatedAt]    DATETIME         NOT NULL,
    [ChangedAt]    DATETIME         NULL,
    CONSTRAINT [PK_SubmissionMarks] PRIMARY KEY CLUSTERED ([TenantId] ASC, [SubmissionId] ASC)
);


GO
PRINT N'Creating Foreign Key [learning].[FK_StudentSubmissions_Students]...';


GO
ALTER TABLE [learning].[StudentSubmissions] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentSubmissions_Students] FOREIGN KEY ([StudentId], [TenantId]) REFERENCES [learning].[Students] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_StudentSubmissions_ContentEntries]...';


GO
ALTER TABLE [learning].[StudentSubmissions] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentSubmissions_ContentEntries] FOREIGN KEY ([EntryId], [TenantId]) REFERENCES [learning].[ContentEntries] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_SubmissionMarks_Submissions]...';


GO
ALTER TABLE [learning].[SubmissionMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_SubmissionMarks_Submissions] FOREIGN KEY ([TenantId], [SubmissionId]) REFERENCES [learning].[StudentSubmissions] ([TenantId], [Id]);


GO
PRINT N'Altering View [learning].[v_EntryTypes]...';


GO
ALTER VIEW [learning].[v_EntryTypes]
AS
SELECT [Id]
	,[Name]
	,[HandlerClass]
FROM [learning].[EntryTypes]
GO
PRINT N'Creating View [learning].[v_StudentSubmissions]...';


GO
CREATE VIEW [learning].[v_StudentSubmissions] AS
SELECT ss.Id,
	ss.EntryId,
	ss.StudentId,
	ss.TenantId,
	ss.[Data],
	ss.CreatedAt,
	ss.ChangedAt,
	ce.TypeId
FROM learning.StudentSubmissions ss
	JOIN learning.ContentEntries ce
		ON ce.Id = ss.EntryId AND ce.TenantId = ss.TenantId
GO
PRINT N'Creating Procedure [learning].[ups_StudentSubmission]...';


GO


CREATE PROCEDURE [learning].[ups_StudentSubmission] @TenantId UNIQUEIDENTIFIER
	,@StudentId UNIQUEIDENTIFIER
	,@EntryId INT
	,@Data XML
	,@ContentEntryType INT
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
	INSERT learning.StudentSubmissions (
		StudentId
		,TenantId
		,EntryId
		,[Data]
		,CreatedAt
		)
	SELECT @StudentId
		,@TenantId
		,@EntryId
		,@Data
		,GETUTCDATE();
GO
