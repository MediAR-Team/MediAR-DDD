CREATE TABLE [learning].[StudentSubmissions]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[TenantId] uniqueidentifier NOT NULL,
	[StudentId] uniqueidentifier NOT NULL,
	[EntryId] int NOT NULL,
	[Data] XML NOT NULL,
	[CreatedAt] datetime NOT NULL,
	[ChangedAt] datetime,
	CONSTRAINT [PK_StudentSubmissions] PRIMARY KEY ([TenantId], [Id]),
	CONSTRAINT [FK_StudentSubmissions_Students] FOREIGN KEY ([StudentId], [TenantId]) REFERENCES [learning].[Students] ([Id], [TenantId]),
	CONSTRAINT [FK_StudentSubmissions_ContentEntries] FOREIGN KEY ([EntryId], [TenantId]) REFERENCES [learning].[ContentEntries] ([Id], [TenantId])
)
