

CREATE TABLE [learning].[GroupToCourse] (
	[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[GroupId] INT NOT NULL
	,[CourseId] INT NOT NULL
	,CONSTRAINT [PK_GroupToCourse] PRIMARY KEY (
		[TenantId]
		,[GroupId]
		,[CourseId]
		)
	,CONSTRAINT [FK_GroupToCourse_Group] FOREIGN KEY (
		[TenantId]
		,[GroupId]
		) REFERENCES [learning].[Groups]([TenantId], [Id])
	,CONSTRAINT [FK_GroupToCourse_Course] FOREIGN KEY (
		[TenantId]
		,[CourseId]
		) REFERENCES [learning].[Courses]([TenantId], [Id])
	,
	)

	