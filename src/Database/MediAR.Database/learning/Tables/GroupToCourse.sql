

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
		[GroupId]
		,[TenantId]
		) REFERENCES [learning].[Groups]([Id], [TenantId])
	,CONSTRAINT [FK_GroupToCourse_Course] FOREIGN KEY (
		[CourseId]
		,[TenantId]
		) REFERENCES [learning].[Courses]([Id], [TenantId])
	,
	)

	