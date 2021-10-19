CREATE TABLE [learning].[StudentToGroup] (
	[StudentId] UNIQUEIDENTIFIER NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[GroupId] INT NOT NULL FOREIGN KEY ([GroupId], [TenantId]) REFERENCES [learning].[Groups]([Id], [TenantId])
	)
