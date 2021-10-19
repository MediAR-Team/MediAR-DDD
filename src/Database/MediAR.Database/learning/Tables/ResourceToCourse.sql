CREATE TABLE [learning].[ResourceToCourse] (
	[ResourceId] INT NOT NULL FOREIGN KEY REFERENCES [learning].[Resources]([Id])
	,[CourseId] INT NOT NULL FOREIGN KEY (
		[CourseId]
		,[TenantId]
		) REFERENCES [learning].[Courses]([Id], [TenantId])
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_ResourceToCourse] PRIMARY KEY (
		[ResourceId]
		,[CourseId]
		,[TenantId]
		)
	);
