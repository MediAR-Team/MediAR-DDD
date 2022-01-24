
GO
PRINT N'Creating Table [learning].[GroupToCourse]...';


GO
CREATE TABLE [learning].[GroupToCourse] (
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [GroupId]  INT              NOT NULL,
    [CourseId] INT              NOT NULL,
    CONSTRAINT [PK_GroupToCourse] PRIMARY KEY CLUSTERED ([TenantId] ASC, [GroupId] ASC, [CourseId] ASC)
);


GO
PRINT N'Creating Table [learning].[Instructors]...';


GO
CREATE TABLE [learning].[Instructors] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [UserName]  VARCHAR (256)    NOT NULL,
    [Email]     VARCHAR (256)    NOT NULL,
    [FirstName] VARCHAR (256)    NOT NULL,
    [LastName]  VARCHAR (256)    NOT NULL,
    [TenantId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Instructors] PRIMARY KEY CLUSTERED ([Id] ASC, [TenantId] ASC)
);


GO
PRINT N'Creating Foreign Key [learning].[FK_GroupToCourse_Group]...';


GO
ALTER TABLE [learning].[GroupToCourse] WITH NOCHECK
    ADD CONSTRAINT [FK_GroupToCourse_Group] FOREIGN KEY ([GroupId], [TenantId]) REFERENCES [learning].[Groups] ([Id], [TenantId]);


GO
PRINT N'Creating Foreign Key [learning].[FK_GroupToCourse_Course]...';


GO
ALTER TABLE [learning].[GroupToCourse] WITH NOCHECK
    ADD CONSTRAINT [FK_GroupToCourse_Course] FOREIGN KEY ([CourseId], [TenantId]) REFERENCES [learning].[Courses] ([Id], [TenantId]);


GO
PRINT N'Creating View [learning].[v_GroupCourses]...';


GO
CREATE VIEW [learning].[v_GroupCourses]
	AS SELECT 
	G.Id AS GroupId,
	G.[Name] AS GroupName,
	C.Id AS CourseId,
	C.[Name] AS CourseName,
	C.[Description] AS CourseDescription,
	C.BackgroundImageUrl AS BackgroundImageUrl,
	GTC.TenantId AS TenantId
	FROM learning.Groups G
	JOIN learning.GroupToCourse GTC ON GTC.TenantId = G.TenantId AND GTC.GroupId = G.Id
	JOIN learning.Courses C ON C.TenantId = GTC.TenantId AND C.Id = GTC.CourseId
GO
PRINT N'Creating View [learning].[v_StudentCourses]...';


GO


CREATE VIEW [learning].[v_StudentCourses]
AS
SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,G.Id AS GroupId
	,G.[Name] AS GroupName
	,STG.StudentId AS StudentId
	,S.UserName AS UserName
	,S.Email AS Email
FROM [learning].[Courses] C
JOIN [learning].[GroupToCourse] GTC ON GTC.TenantId = C.TenantId
	AND GTC.CourseId = C.Id
JOIN [learning].[Groups] g ON G.TenantId = GTC.TenantId
	AND G.Id = GTC.GroupId
JOIN [learning].[StudentToGroup] STG ON STG.TenantId = G.TenantId
	AND STG.GroupId = G.Id
JOIN [learning].[Students] S ON S.Id = STG.StudentId
	AND S.TenantId = STG.TenantId;
GO
PRINT N'Altering Procedure [membership].[assign_Role_to_User_by_Email]...';


GO
ALTER PROCEDURE [membership].[assign_Role_to_User_by_Email] @Email VARCHAR(256)
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
				AND [TenantId] = @TenantId
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
		,TenantId
		)
	VALUES (
		@UserId
		,@RoleId
		,@TenantId
		);

	RETURN 0
END
GO
PRINT N'Altering Procedure [membership].[assign_Role_to_User_by_UserName]...';


GO
ALTER PROCEDURE [membership].[assign_Role_to_User_by_UserName] @UserName VARCHAR(256)
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
				AND [TenantId] = @TenantId
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
		,TenantId
		)
	VALUES (
		@UserId
		,@RoleId
		,@TenantId
		);

	RETURN 0
END
GO
PRINT N'Creating Procedure [learning].[add_Group_to_Course]...';


GO


CREATE PROCEDURE [learning].[add_Group_to_Course] @GroupId INT
	,@CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM learning.Groups G
		WHERE TenantId = @TenantId
			AND Id = @GroupId
		)
BEGIN
		;

	THROW 60000
		,'Group does not exist'
		,5;
END;

IF NOT EXISTS (
		SELECT 1
		FROM learning.Courses
		WHERE TenantId = @TenantId
			AND Id = @CourseId
		)
BEGIN
		;

	THROW 60000
		,'Group does not exist'
		,5;
END;

INSERT learning.GroupToCourse (
	CourseId
	,GroupId
	,TenantId
	)
SELECT @CourseId
	,@GroupId
	,@TenantId;
GO
PRINT N'Creating Procedure [learning].[ins_Instructor]...';


GO


CREATE PROCEDURE [learning].[ins_Instructor] @UserId UNIQUEIDENTIFIER
	,@UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
IF NOT EXISTS (
		SELECT 1
		FROM [learning].[Instructors]
		WHERE [Id] = @UserId
			AND [TenantId] = @TenantId
			OR [UserName] = @UserName
			AND [TenantId] = @TenantId
		)
	INSERT INTO [learning].[Instructors] (
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
BEGIN
		;

	THROW 60000
		,'Instructor already exists'
		,5;
END;
GO