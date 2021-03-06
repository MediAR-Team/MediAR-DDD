CREATE TABLE [learning].[Students] (
	[Id] UNIQUEIDENTIFIER NOT NULL
	,[UserName] VARCHAR(256) NOT NULL
	,[Email] VARCHAR(256) NOT NULL
	,[FirstName] VARCHAR(256) NOT NULL
	,[LastName] VARCHAR(256) NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_Students] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	)
