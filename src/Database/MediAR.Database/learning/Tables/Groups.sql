CREATE TABLE [learning].[Groups] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,CONSTRAINT [PK_Groups] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	)
