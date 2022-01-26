CREATE TABLE [learning].[ContentEntries] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[TypeId] INT NOT NULL
	,[Configuration] XML NOT NULL
	,[Data] XML NOT NULL
	,[ModuleId] INT NOT NULL
	,[Ordinal] INT NOT NULL
	,[Title] VARCHAR(256) NOT NULL
	,CONSTRAINT [PK_ContentEntries] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	,CONSTRAINT [FK_ContentEntries_Modules] FOREIGN KEY (
		[ModuleId]
		,[TenantId]
		) REFERENCES [learning].[Modules]([Id], [TenantId])
	,CONSTRAINT [FK_ContentEntries_EntryTypes] FOREIGN KEY ([TypeId]) REFERENCES [learning].[EntryTypes]([Id])
	,CONSTRAINT [UQ_ContentEntries_ModuleOrdinals] UNIQUE (
		[TenantId]
		,[ModuleId]
		,[Ordinal]
		)
	);
