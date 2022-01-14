GO
PRINT N'Creating Schema [learning]...';


GO
CREATE SCHEMA [learning]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating Schema [membership]...';


GO
CREATE SCHEMA [membership]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating Schema [tenants]...';


GO
CREATE SCHEMA [tenants]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating Table [learning].[Courses]...';


GO
CREATE TABLE [learning].[Courses] (
    [Id]                 INT              IDENTITY (1, 1) NOT NULL,
    [TenantId]           UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (256)    NOT NULL,
    [Description]        VARCHAR (1024)   NOT NULL,
    [BackgroundImageUrl] VARCHAR (2048)   NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [learning].[Groups]...';


GO
CREATE TABLE [learning].[Groups] (
    [Id]       INT              IDENTITY (1, 1) NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [Name]     VARCHAR (256)    NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [learning].[StudentToGroup]...';


GO
CREATE TABLE [learning].[StudentToGroup] (
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [TenantId]  UNIQUEIDENTIFIER NOT NULL,
    [GroupId]   INT              NOT NULL,
    CONSTRAINT [PK_StudentToGroup] PRIMARY KEY CLUSTERED ([StudentId] ASC, [TenantId] ASC, [GroupId] ASC)
);


GO
PRINT N'Creating Index [learning].[StudentToGroup].[IX_StudentToGroup_StudentId_GroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_StudentToGroup_StudentId_GroupId]
    ON [learning].[StudentToGroup]([StudentId] ASC, [GroupId] ASC);


GO
PRINT N'Creating Table [learning].[Students]...';


GO
CREATE TABLE [learning].[Students] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [UserName]  VARCHAR (256)    NOT NULL,
    [Email]     VARCHAR (256)    NOT NULL,
    [FirstName] VARCHAR (256)    NOT NULL,
    [LastName]  VARCHAR (256)    NOT NULL,
    [TenantId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [learning].[InternalCommands]...';


GO
CREATE TABLE [learning].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [learning].[InboxMessages]...';


GO
CREATE TABLE [learning].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [learning].[Resources]...';


GO
CREATE TABLE [learning].[Resources] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (256) NOT NULL,
    [Author] VARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [learning].[EntryTypes]...';


GO
CREATE TABLE [learning].[EntryTypes] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (256) NOT NULL,
    [HandlerClass] VARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [learning].[ContentEntries]...';


GO
CREATE TABLE [learning].[ContentEntries] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [TenantId]      UNIQUEIDENTIFIER NOT NULL,
    [TypeId]        INT              NOT NULL,
    [Configuration] XML              NOT NULL,
    [Data]          XML              NOT NULL,
    [ModuleId]      INT              NOT NULL,
    CONSTRAINT [PK_ContentEntries] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [learning].[Modules]...';


GO
CREATE TABLE [learning].[Modules] (
    [Id]       INT              IDENTITY (1, 1) NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [Name]     VARCHAR (256)    NOT NULL,
    [CourseId] INT              NOT NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [learning].[ResourceToCourse]...';


GO
CREATE TABLE [learning].[ResourceToCourse] (
    [ResourceId] INT              NOT NULL,
    [CourseId]   INT              NOT NULL,
    [TenantId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ResourceToCourse] PRIMARY KEY CLUSTERED ([ResourceId] ASC, [CourseId] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [membership].[Permissions]...';


GO
CREATE TABLE [membership].[Permissions] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (256)    NOT NULL,
    [Description] VARCHAR (512)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Index [membership].[Permissions].[IX_Permissions_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Permissions_Name]
    ON [membership].[Permissions]([Name] ASC);


GO
PRINT N'Creating Table [membership].[Roles]...';


GO
CREATE TABLE [membership].[Roles] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (256)    NOT NULL,
    [Description] VARCHAR (512)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Index [membership].[Roles].[IX_Roles_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Roles_Name]
    ON [membership].[Roles]([Name] ASC);


GO
PRINT N'Creating Table [membership].[InboxMessages]...';


GO
CREATE TABLE [membership].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [membership].[InternalCommands]...';


GO
CREATE TABLE [membership].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [membership].[UsersToRoles]...';


GO
CREATE TABLE [membership].[UsersToRoles] (
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [RoleId]   UNIQUEIDENTIFIER NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsersToRoles_UserId_RoleId] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Table [membership].[RolesToPermissions]...';


GO
CREATE TABLE [membership].[RolesToPermissions] (
    [RoleId]       UNIQUEIDENTIFIER NOT NULL,
    [PermissionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RolesToPermissions_RoleId_PermissionId] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC)
);


GO
PRINT N'Creating Table [membership].[Users]...';


GO
CREATE TABLE [membership].[Users] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserName]     VARCHAR (256)    NOT NULL,
    [Email]        VARCHAR (256)    NOT NULL,
    [PasswordHash] VARCHAR (512)    NOT NULL,
    [FirstName]    VARCHAR (256)    NULL,
    [LastName]     VARCHAR (256)    NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Index [membership].[Users].[IX_Users_TenantId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_TenantId]
    ON [membership].[Users]([TenantId] ASC);


GO
PRINT N'Creating Index [membership].[Users].[IX_Users_UserName]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_UserName]
    ON [membership].[Users]([UserName] ASC);


GO
PRINT N'Creating Index [membership].[Users].[IX_Users_Email]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_Email]
    ON [membership].[Users]([Email] ASC);


GO
PRINT N'Creating Table [tenants].[InternalCommands]...';


GO
CREATE TABLE [tenants].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [tenants].[InboxMessages]...';


GO
CREATE TABLE [tenants].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [tenants].[Tenants]...';


GO
CREATE TABLE [tenants].[Tenants] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             VARCHAR (256)    NOT NULL,
    [ConnectionString] VARCHAR (256)    NOT NULL,
    [ReferUrl]         VARCHAR (256)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [tenants].[Tenants]...';


GO
ALTER TABLE [tenants].[Tenants]
    ADD DEFAULT '' FOR [ReferUrl];


GO
PRINT N'Creating Foreign Key [learning].[FK_StudentToGroup_Group]...';


GO
ALTER TABLE [learning].[StudentToGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentToGroup_Group] FOREIGN KEY ([GroupId], [TenantId]) REFERENCES [learning].[Groups] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_StudentToGroup_Student]...';


GO
ALTER TABLE [learning].[StudentToGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentToGroup_Student] FOREIGN KEY ([StudentId], [TenantId]) REFERENCES [learning].[Students] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_ContentEntries_Modules]...';


GO
ALTER TABLE [learning].[ContentEntries] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentEntries_Modules] FOREIGN KEY ([ModuleId], [TenantId]) REFERENCES [learning].[Modules] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_ContentEntries_EntryTypes]...';


GO
ALTER TABLE [learning].[ContentEntries] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentEntries_EntryTypes] FOREIGN KEY ([TypeId]) REFERENCES [learning].[EntryTypes] ([Id]);


GO
PRINT N'Creating Foreign Key [learning].[Fk_Modules_Courses]...';


GO
ALTER TABLE [learning].[Modules] WITH NOCHECK
    ADD CONSTRAINT [Fk_Modules_Courses] FOREIGN KEY ([CourseId], [TenantId]) REFERENCES [learning].[Courses] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_ResourceToCourse_Resources]...';


GO
ALTER TABLE [learning].[ResourceToCourse] WITH NOCHECK
    ADD CONSTRAINT [FK_ResourceToCourse_Resources] FOREIGN KEY ([ResourceId]) REFERENCES [learning].[Resources] ([Id]);


GO
PRINT N'Creating Foreign Key [learning].[FK_ResourceToCourse_Courses]...';


GO
ALTER TABLE [learning].[ResourceToCourse] WITH NOCHECK
    ADD CONSTRAINT [FK_ResourceToCourse_Courses] FOREIGN KEY ([CourseId], [TenantId]) REFERENCES [learning].[Courses] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [membership].[FK_UsersToRoles_Users]...';


GO
ALTER TABLE [membership].[UsersToRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UsersToRoles_Users] FOREIGN KEY ([UserId], [TenantId]) REFERENCES [membership].[Users] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [membership].[FK_UsersToRoles_Roles]...';


GO
ALTER TABLE [membership].[UsersToRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UsersToRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [membership].[Roles] ([Id]);


GO
PRINT N'Creating Foreign Key [membership].[FK_RolesToPermissions_Roles]...';


GO
ALTER TABLE [membership].[RolesToPermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesToPermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [membership].[Roles] ([Id]);


GO
PRINT N'Creating Foreign Key [membership].[FK_RolesToPermissions_Permissions]...';


GO
ALTER TABLE [membership].[RolesToPermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesToPermissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [membership].[Permissions] ([Id]);


GO
PRINT N'Creating View [learning].[v_GroupMembers]...';


GO
CREATE VIEW [learning].[v_GroupMembers]
AS
SELECT G.Id AS [GroupId]
	,G.[Name] AS [GroupName]
	,S.Id AS [StudentId]
	,S.UserName AS [UserName]
	,S.Email AS [Email]
	,S.FirstName AS [FirstName]
	,S.LastName AS [LastName]
	,STG.TenantId AS [TenantId]
FROM [learning].[StudentToGroup] STG
JOIN [learning].[Students] S ON S.Id = STG.StudentId
	AND S.TenantId = STG.TenantId
JOIN [learning].[Groups] G ON G.Id = STG.GroupId
	AND G.TenantId = STG.TenantId
GO
PRINT N'Creating View [learning].[v_Groups]...';


GO
CREATE VIEW [learning].[v_Groups]
AS
SELECT [Id]
	,[Name]
	,[TenantId]
FROM [learning].[Groups];
GO
PRINT N'Creating View [learning].[v_EntryTypes]...';


GO
CREATE VIEW [learning].[v_EntryTypes]
AS
SELECT [Name]
	,[HandlerClass]
FROM [learning].[EntryTypes]
GO
PRINT N'Creating View [learning].[v_CourseAggregate]...';


GO
CREATE VIEW [learning].[v_CourseAggregate]
	AS SELECT C.[Id] AS [CourseId],
	C.[TenantId] AS [TenantId],
	C.[Name] AS [CourseName],
	C.[Description] AS [CourseDescription],
	C.[BackgroundImageUrl] AS [CourseBackgroundImageUrl],
	M.[Id] AS [ModuleId],
	M.[Name] AS [ModuleName],
	CE.[Id] AS [EntryId],
	ET.[Name] AS [EntryTypeName],
	CE.[Configuration] AS [EntryConfiguration],
	CE.[Data] AS [EntryData]
	FROM [learning].[Courses] C
	LEFT JOIN [learning].[Modules] M ON M.[CourseId] = C.[Id] AND M.[TenantId] = C.[TenantId]
	LEFT JOIN [learning].[ContentEntries] CE ON CE.[ModuleId] = M.[Id] AND CE.[TenantId] = M.[TenantId]
	LEFT JOIN [learning].[EntryTypes] ET ON ET.Id = CE.TypeId;
GO
PRINT N'Creating View [membership].[v_UserRoles]...';


GO
CREATE VIEW [membership].[v_UserRoles]
	WITH SCHEMABINDING
AS
SELECT u.Id AS UserId
	,u.UserName AS UserName
	,u.Email AS Email
	,r.[Name] AS RoleName
FROM [membership].[Users] u
JOIN [membership].[UsersToRoles] utr ON utr.UserId = u.Id
JOIN [membership].[Roles] r ON r.Id = utr.RoleId
GO
PRINT N'Creating View [membership].[v_Users]...';


GO
CREATE VIEW [membership].[v_Users]
	WITH SCHEMABINDING
AS
SELECT u.Id AS Id
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,u.PasswordHash AS [PasswordHash]
	,u.FirstName AS [FirstName]
	,u.LastName AS [LastName]
	,u.TenantId [TenantId]
FROM [membership].Users u
GO
PRINT N'Creating View [membership].[v_UserPermissions]...';


GO
CREATE VIEW [membership].[v_UserPermissions]
	WITH SCHEMABINDING
AS
SELECT u.Id AS [UserId]
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,p.Id AS [PermissionId]
	,p.[Name] AS [PermissionName]
FROM [membership].[Users] u
JOIN [membership].[UsersToRoles] utr ON utr.UserId = u.Id
JOIN [membership].[RolesToPermissions] rtp ON rtp.RoleId = utr.RoleId
JOIN [membership].[Permissions] p ON p.Id = rtp.PermissionId
GO
PRINT N'Creating View [tenants].[v_Tenants]...';


GO
CREATE VIEW [tenants].[v_Tenants]
AS
SELECT [Id]
	,[Name]
	,[ConnectionString]
	,[ReferUrl]
FROM [tenants].[Tenants]
GO
PRINT N'Creating Procedure [learning].[ins_Student]...';


GO
CREATE PROCEDURE [learning].[ins_Student] @UserId UNIQUEIDENTIFIER
	,@UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Students]
		WHERE [Id] = @UserId
			AND [TenantId] = @TenantId
			OR [UserName] = @UserName
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Students] (
		[Id]
		,[UserName]
		,[Email]
		,[FirstName]
		,[LastName]
		,[TenantId]
		)
	VALUES (
		@UserId
		,@UserName
		,@Email
		,@FirstName
		,@LastName
		,@TenantId
		);
ELSE
	THROW 60000
		,'Student already exists'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [learning].[del_Group]...';


GO
CREATE PROCEDURE [learning].[del_Group] @GroupId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF EXISTS (
		SELECT 1
		FROM [learning].[Groups]
		WHERE [Id] = @GroupId
			AND [TenantId] = @TenantId
		)
BEGIN
	DELETE
	FROM [learning].[StudentToGroup]
	WHERE [GroupId] = @GroupId
		AND [TenantId] = @TenantId;

	DELETE
	FROM [learning].[Groups]
	WHERE [Id] = @GroupId
		AND [TenantId] = @TenantId;
END
ELSE
	THROW 60000
		,'Group not found'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [learning].[ins_Course]...';


GO
CREATE PROCEDURE [learning].[ins_Course] @Name VARCHAR(256)
	,@Description VARCHAR(1024)
	,@BackgroundImage VARCHAR(2048)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Courses]
		WHERE [Name] = @Name
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Courses] (
		[Name]
		,[Description]
		,[BackgroundImageUrl]
		,[TenantId]
		)
	VALUES (
		@Name
		,@Description
		,@BackgroundImage
		,@TenantId
		);
ELSE
	THROW 60000
		,'Course with same name exists'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [learning].[add_Student_to_Group]...';


GO
CREATE PROCEDURE [learning].[add_Student_to_Group] @StudentId UNIQUEIDENTIFIER
	,@GroupId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF EXISTS (
		SELECT 1
		FROM [learning].[Students]
		WHERE [Id] = @StudentId
			AND [TenantId] = @TenantId
		)
	AND EXISTS (
		SELECT 1
		FROM [learning].[Groups]
		WHERE [Id] = @GroupId
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[StudentToGroup] (
		[StudentId]
		,[GroupId]
		,[TenantId]
		)
	VALUES (
		@StudentId
		,@GroupId
		,@TenantId
		);
ELSE
	THROW 60000
		,'No student or group with such id'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [learning].[del_Course]...';


GO
CREATE PROCEDURE [learning].[del_Course] @CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DELETE [Entry]
FROM [learning].[ContentEntries] [Entry]
JOIN [learning].[Modules] [Module] ON [Module].Id = [Entry].[ModuleId]
	AND [Module].TenantId = [Entry].TenantId
WHERE [Module].CourseId = @CourseId
	AND [Module].[TenantId] = @TenantId
	AND [Entry].[TenantId] = @TenantId

DELETE [learning].[Modules]
WHERE [CourseId] = @CourseId
	AND [TenantId] = @TenantId

DELETE [learning].[Courses]
WHERE [Id] = @CourseId
	AND [TenantId] = @TenantId
GO
PRINT N'Creating Procedure [learning].[del_Module]...';


GO
CREATE PROCEDURE [learning].[del_Module] @ModuleId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DELETE [learning].[ContentEntries]
WHERE ModuleId = @ModuleId
	AND TenantId = @TenantId

DELETE [learning].[Modules]
WHERE Id = @ModuleId
	AND TenantId = @TenantId
GO
PRINT N'Creating Procedure [learning].[ins_ContentEntry]...';


GO
CREATE PROCEDURE [learning].[ins_ContentEntry] @TenantId UNIQUEIDENTIFIER
	,@ModuleId INT
	,@TypeId INT
	,@Data XML
	,@Config XML
AS
INSERT [learning].[ContentEntries] (
	ModuleId
	,TypeId
	,TenantId
	,[Data]
	,[Configuration]
	)
SELECT @ModuleId
	,@TypeId
	,@TenantId
	,@Data
	,@Config;
GO
PRINT N'Creating Procedure [learning].[ins_Group]...';


GO
CREATE PROCEDURE [learning].[ins_Group] @Name VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
INSERT [learning].[Groups] (
	[Name]
	,[TenantId]
	)
SELECT @Name
	,@TenantId;
GO
PRINT N'Creating Procedure [learning].[ins_Module]...';


GO
CREATE PROCEDURE [learning].[ins_Module] @Name VARCHAR(256)
	,@CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
INSERT [learning].[Modules] (
	[Name]
	,[CourseId]
	,[TenantId]
	)
SELECT @Name
	,@CourseId
	,@TenantId;
GO
PRINT N'Creating Procedure [membership].[sel_Roles_with_Permissions]...';


GO
CREATE PROCEDURE [membership].[sel_Roles_with_Permissions] @Offset INT = 0
	,@Next INT
AS
SELECT [Role].[Id] AS [Id]
	,[Role].[Name] AS [Name]
	,[Role].[Description] AS [Description]
	,[Permission].[Name] AS [Name]
	,[Permission].[Description] AS [Description]
FROM (
	SELECT [Role].[Id]
		,[Role].[Name]
		,[Role].[Description]
	FROM [membership].[Roles] [Role]
	ORDER BY [Role].[Id] OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY
	) [Role]
JOIN [membership].[RolesToPermissions] [RTP] ON [RTP].[RoleId] = [Role].[Id]
JOIN [membership].[Permissions] [Permission] ON [Permission].[Id] = [RTP].[PermissionId]

RETURN 0
GO
PRINT N'Creating Procedure [membership].[ins_User]...';


GO
CREATE PROCEDURE [membership].[ins_User] @UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@PasswordHash VARCHAR(512)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
DECLARE @Count AS INT;

SELECT @Count = COUNT(*)
FROM [membership].[Users]
WHERE Email = @Email
	OR UserName = @UserName;

IF @Count = 0
	INSERT INTO [membership].[Users] (
		Id
		,UserName
		,Email
		,PasswordHash
		,FirstName
		,LastName
		,TenantId
		)
	VALUES (
		newid()
		,@UserName
		,@Email
		,@PasswordHash
		,@FirstName
		,@LastName
		,@TenantId
		);
ELSE
	THROW 60000
		,'User with UserName or Email already exists'
		,5;
GO
PRINT N'Creating Procedure [membership].[assign_Role_to_User_by_Email]...';


GO
CREATE PROCEDURE [membership].[assign_Role_to_User_by_Email] @Email VARCHAR(256)
	,@RoleName VARCHAR(256),
	@TenantId uniqueidentifier
AS
BEGIN
	DECLARE @UserCount INT
		,@RoleCount INT
		,@UserId UNIQUEIDENTIFIER
		,@RoleId UNIQUEIDENTIFIER;

	SELECT @UserCount = count(*)
		,@UserId = Id
	FROM [membership].[Users]
	WHERE Email = @Email
	AND TenantId = @TenantId
	GROUP BY Id

	SELECT @RoleCount = count(*)
		,@RoleId = Id
	FROM [membership].[Roles]
	WHERE [Name] = @RoleName
	GROUP BY Id

	IF (
			@UserCount = 0
			OR @RoleCount = 0
			)
	BEGIN
			;

		THROW 60000
			,'No user with Email or role does not exist'
			,5;
	END

	IF EXISTS (
			SELECT 1
			FROM [membership].[UsersToRoles]
			WHERE [UserId] = @UserId
				AND [RoleId] = @RoleId
			)
	BEGIN
			;

		THROW 60000
			,'User already added to role'
			,5;
	END

	INSERT INTO UsersToRoles (
		UserId
		,RoleId
		)
	VALUES (
		@UserId
		,@RoleId
		);

	RETURN 0
END
GO
PRINT N'Creating Procedure [membership].[assign_Role_to_User_by_UserName]...';


GO
CREATE PROCEDURE [membership].[assign_Role_to_User_by_UserName] @UserName VARCHAR(256)
	,@RoleName VARCHAR(256),
	@TenantId uniqueidentifier
AS
BEGIN
	DECLARE @UserCount INT
		,@RoleCount INT
		,@UserId UNIQUEIDENTIFIER
		,@RoleId UNIQUEIDENTIFIER;

	SELECT @UserCount = count(*)
		,@UserId = Id
	FROM [membership].[Users]
	WHERE UserName = @UserName
	AND TenantId = @TenantId
	GROUP BY Id

	SELECT @RoleCount = count(*)
		,@RoleId = Id
	FROM [membership].[Roles]
	WHERE [Name] = @RoleName
	GROUP BY Id
		IF (
			@UserCount = 0
			OR @RoleCount = 0
			)
	BEGIN
			;

		THROW 60000
			,'No user with username or role does not exist'
			,5;
	END

	IF EXISTS (
			SELECT 1
			FROM [membership].[UsersToRoles]
			WHERE [UserId] = @UserId
				AND [RoleId] = @RoleId
			)
	BEGIN
			;

		THROW 60000
			,'User already added to role'
			,5;
	END

	INSERT INTO UsersToRoles (
		UserId
		,RoleId
		)
	VALUES (
		@UserId
		,@RoleId
		);

	RETURN 0
END
GO
PRINT N'Creating Procedure [tenants].[del_Tenant]...';


GO
CREATE PROCEDURE [tenants].[del_Tenant] @TenantId UNIQUEIDENTIFIER
AS
DECLARE @Count INT;

SELECT @Count = COUNT(*)
FROM [tenants].[Tenants]
WHERE [Id] = @TenantId;

IF @Count > 0
	DELETE
	FROM [tenants].[Tenants]
	WHERE [Id] = @TenantId;
ELSE
	THROW 60000
		,'No tenant with id'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [tenants].[ins_Tenant]...';


GO
CREATE PROCEDURE [tenants].[ins_Tenant] @Name VARCHAR(256)
	,@ConnectionString VARCHAR(256) = ''
AS
DECLARE @SameNameCount INT;

SELECT @SameNameCount = COUNT(*)
FROM [tenants].[Tenants]
WHERE [Name] = @Name;

IF @SameNameCount = 0
BEGIN
	INSERT INTO [tenants].[Tenants] (
		[Id]
		,[Name]
		,[ConnectionString]
		)
	VALUES (
		NEWID()
		,@Name
		,@ConnectionString
		);

	SELECT @@IDENTITY;
END
ELSE
	THROW 60000
		,'Tenant with same name exists'
		,5;

RETURN 0
GO
PRINT N'Creating Procedure [learning].[upd_ContentEntry]...';


GO
CREATE PROCEDURE [learning].[upd_ContentEntry] @Id INT
	,@TenantId UNIQUEIDENTIFIER
	,@Data XML
	,@Config XML
	,@TypeId INT
AS
DECLARE @EntryExists BIT = 0
	,@CorrespondingType BIT = 0;

SELECT @EntryExists = 1
	,@CorrespondingType = CASE 
		WHEN CE.TypeId = @TypeId
			THEN 1
		ELSE 0
		END
FROM learning.ContentEntries CE
WHERE CE.Id = @Id
	AND CE.TenantId = @TenantId

IF @EntryExists = 0
	OR @CorrespondingType = 0
BEGIN
		;

	THROW 60000
		,'Entry does not exist or type mismatch'
		,5;
END;

UPDATE learning.ContentEntries
SET [Data] = @Data
	,[Configuration] = @Config
WHERE Id = @Id
	AND TenantId = @TenantId;

GO
PRINT N'Update complete.';

GO