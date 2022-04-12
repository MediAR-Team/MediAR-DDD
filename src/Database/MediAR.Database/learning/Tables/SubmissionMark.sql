CREATE TABLE [learning].[SubmissionMarks]
(
	[SubmissionId] INT NOT NULL,
	[TenantId] uniqueidentifier NOT NULL,
	[InstructorId] uniqueidentifier NOT NULL,
	[MarkValue] int NOT NULL,
	[Comment] varchar(512) NULL,
	[CreatedAt] datetime NOT NULL,
	[ChangedAt] datetime,
	CONSTRAINT [PK_SubmissionMarks] PRIMARY KEY ([TenantId], [SubmissionId]),
	CONSTRAINT [FK_SubmissionMarks_Submissions] FOREIGN KEY ([TenantId], [SubmissionId]) REFERENCES learning.StudentSubmissions (TenantId, Id),
	CONSTRAINT [FK_SubmissionMarks_Instructors] FOREIGN KEY ([InstructorId], [TenantId]) REFERENCES learning.Instructors (Id, TenantId)
)
