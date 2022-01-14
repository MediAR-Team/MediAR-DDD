

CREATE TABLE [learning].[Modules] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[CourseId] INT NOT NULL
	,[Ordinal] INT NOT NULL
	,CONSTRAINT [PK_Modules] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	,CONSTRAINT [Fk_Modules_Courses] FOREIGN KEY (
		[CourseId]
		,[TenantId]
		) REFERENCES [learning].[Courses]([Id], [TenantId])
	,CONSTRAINT [UQ_Modules_CounrseOrdinals] UNIQUE (
		[TenantId]
		,[CourseId]
		,[Ordinal]
		)
	)

