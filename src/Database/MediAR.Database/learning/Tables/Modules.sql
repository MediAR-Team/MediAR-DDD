CREATE TABLE [learning].[Modules] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[CourseId] INT NOT NULL FOREIGN KEY ([CourseId], [TenantId]) REFERENCES [learning].[Courses]([Id], [TenantId])
	,CONSTRAINT [PK_Modules] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	)
