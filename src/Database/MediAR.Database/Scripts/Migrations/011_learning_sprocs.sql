GO
PRINT N'Dropping Foreign Key unnamed constraint on [learning].[StudentToGroup]...';


GO
ALTER TABLE [learning].[StudentToGroup] DROP CONSTRAINT [FK__StudentToGroup__4E88ABD4];


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
PRINT N'Creating Primary Key [learning].[PK_StudentToGroup]...';


GO
ALTER TABLE [learning].[StudentToGroup]
    ADD CONSTRAINT [PK_StudentToGroup] PRIMARY KEY CLUSTERED ([StudentId] ASC, [TenantId] ASC, [GroupId] ASC);


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
	THROW 6000
		,'No student or group with such id'
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
	THROW 6000
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
	THROW 6000
		,'Course with same name exists'
		,5;

RETURN 0
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
	THROW 6000
		,'Student already exists'
		,5;

RETURN 0
GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [learning].[StudentToGroup] WITH CHECK CHECK CONSTRAINT [FK_StudentToGroup_Group];

ALTER TABLE [learning].[StudentToGroup] WITH CHECK CHECK CONSTRAINT [FK_StudentToGroup_Student];


GO
PRINT N'Update complete.';


GO