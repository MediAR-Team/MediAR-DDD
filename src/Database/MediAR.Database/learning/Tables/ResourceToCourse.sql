CREATE TABLE [learning].[ResourceToCourse] (
	[ResourceId] INT NOT NULL
	,[CourseId] INT NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_ResourceToCourse] PRIMARY KEY (
		[ResourceId]
		,[CourseId]
		,[TenantId]
		)
	,CONSTRAINT [FK_ResourceToCourse_Resources] FOREIGN KEY ([ResourceId]) REFERENCES [learning].[Resources]([Id])
	,CONSTRAINT [FK_ResourceToCourse_Courses] FOREIGN KEY (
		[CourseId]
		,[TenantId]
		) REFERENCES [learning].[Courses]([Id], [TenantId])
	);
