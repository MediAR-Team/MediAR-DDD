CREATE TABLE [learning].[StudentToGroup] (
	[StudentId] UNIQUEIDENTIFIER NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[GroupId] INT NOT NULL
	,CONSTRAINT [PK_StudentToGroup] PRIMARY KEY ([StudentId], [TenantId], [GroupId])
	,CONSTRAINT [FK_StudentToGroup_Group] FOREIGN KEY (
		[GroupId]
		,[TenantId]
		) REFERENCES [learning].[Groups]([Id], [TenantId])
	,CONSTRAINT [FK_StudentToGroup_Student] FOREIGN KEY (
		[StudentId]
		,[TenantId]
		) REFERENCES [learning].[Students]([Id], [TenantId])
	)
