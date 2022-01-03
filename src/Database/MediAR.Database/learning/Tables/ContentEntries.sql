CREATE TABLE [learning].[ContentEntries] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[TypeId] INT NOT NULL FOREIGN KEY REFERENCES [learning].[EntryTypes]([Id])
	,[Configuration] XML NOT NULL
	,[Data] XML NOT NULL
	,[ModuleId] INT NOT NULL
	,CONSTRAINT [PK_ContentEntries] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	,CONSTRAINT [FK_ContentEntries_Modules] FOREIGN KEY ([ModuleId], [TenantId]) REFERENCES [learning].[Modules]([Id], [TenantId])
	);
