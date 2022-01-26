

CREATE TABLE [learning].[Courses] (
	[Id] INT IDENTITY(1, 1)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[Description] VARCHAR(1024) NOT NULL
	,[BackgroundImageUrl] VARCHAR(2048)
	,[OwnerStudentId] UNIQUEIDENTIFIER
	,CONSTRAINT [PK_Courses] PRIMARY KEY (
		[Id]
		,[TenantId]
		)
	,CONSTRAINT [FK_Courses_OwnerStudent] FOREIGN KEY (
		OwnerStudentId
		,TenantId
		) REFERENCES learning.Students(Id, TenantId)
	)

