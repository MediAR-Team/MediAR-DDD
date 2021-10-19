
GO

PRINT N'Creating Schema [learning]...';
GO

CREATE SCHEMA [learning] AUTHORIZATION [dbo];
GO

PRINT N'Creating Table [learning].[ResourceToCourse]...';
GO

CREATE TABLE [learning].[ResourceToCourse] (
	[ResourceId] INT NOT NULL
	,[CourseId] INT NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_ResourceToCourse] PRIMARY KEY CLUSTERED (
		[ResourceId] ASC
		,[CourseId] ASC
		,[TenantId] ASC
		)
	);
GO

PRINT N'Creating Table [learning].[StudentToGroup]...';
GO

CREATE TABLE [learning].[StudentToGroup] (
	[StudentId] UNIQUEIDENTIFIER NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[GroupId] INT NOT NULL
	);
GO

PRINT N'Creating Index [learning].[StudentToGroup].[IX_StudentToGroup_StudentId_GroupId]...';
GO

CREATE NONCLUSTERED INDEX [IX_StudentToGroup_StudentId_GroupId] ON [learning].[StudentToGroup] (
	[StudentId] ASC
	,[GroupId] ASC
	);
GO

PRINT N'Creating Table [learning].[ContentEntries]...';
GO

CREATE TABLE [learning].[ContentEntries] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[TypeId] INT NOT NULL
	,[Configuration] XML NOT NULL
	,[Data] XML NOT NULL
	,CONSTRAINT [PK_ContentEntries] PRIMARY KEY CLUSTERED (
		[Id] ASC
		,[TenantId] ASC
		)
	);
GO

PRINT N'Creating Table [learning].[Courses]...';
GO

CREATE TABLE [learning].[Courses] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[Description] VARCHAR(1024) NOT NULL
	,[BackgroundImageUrl] VARCHAR(2048) NULL
	,CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED (
		[Id] ASC
		,[TenantId] ASC
		)
	);
GO

PRINT N'Creating Table [learning].[EntryTypes]...';
GO

CREATE TABLE [learning].[EntryTypes] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[HandlerClass] VARCHAR(256) NOT NULL
	,PRIMARY KEY CLUSTERED ([Id] ASC)
	);
GO

PRINT N'Creating Table [learning].[Groups]...';
GO

CREATE TABLE [learning].[Groups] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED (
		[Id] ASC
		,[TenantId] ASC
		)
	);
GO

PRINT N'Creating Table [learning].[Modules]...';
GO

CREATE TABLE [learning].[Modules] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[CourseId] INT NOT NULL
	,CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED (
		[Id] ASC
		,[TenantId] ASC
		)
	);
GO

PRINT N'Creating Table [learning].[Resources]...';
GO

CREATE TABLE [learning].[Resources] (
	[Id] INT IDENTITY(1, 1) NOT NULL
	,[Name] VARCHAR(256) NOT NULL
	,[Author] VARCHAR(256) NOT NULL
	,PRIMARY KEY CLUSTERED ([Id] ASC)
	);
GO

PRINT N'Creating Foreign Key unnamed constraint on [learning].[ResourceToCourse]...';
GO

ALTER TABLE [learning].[ResourceToCourse]
	WITH NOCHECK ADD FOREIGN KEY ([ResourceId]) REFERENCES [learning].[Resources]([Id]);
GO

PRINT N'Creating Foreign Key unnamed constraint on [learning].[ResourceToCourse]...';
GO

ALTER TABLE [learning].[ResourceToCourse]
	WITH NOCHECK ADD FOREIGN KEY (
			[CourseId]
			,[TenantId]
			) REFERENCES [learning].[Courses]([Id], [TenantId]);
GO

PRINT N'Creating Foreign Key unnamed constraint on [learning].[StudentToGroup]...';
GO

ALTER TABLE [learning].[StudentToGroup]
	WITH NOCHECK ADD FOREIGN KEY (
			[GroupId]
			,[TenantId]
			) REFERENCES [learning].[Groups]([Id], [TenantId]);
GO

PRINT N'Creating Foreign Key unnamed constraint on [learning].[ContentEntries]...';
GO

ALTER TABLE [learning].[ContentEntries]
	WITH NOCHECK ADD FOREIGN KEY ([TypeId]) REFERENCES [learning].[EntryTypes]([Id]);
GO

PRINT N'Creating Foreign Key unnamed constraint on [learning].[Modules]...';
GO

ALTER TABLE [learning].[Modules]
	WITH NOCHECK ADD FOREIGN KEY (
			[CourseId]
			,[TenantId]
			) REFERENCES [learning].[Courses]([Id], [TenantId]);
GO


