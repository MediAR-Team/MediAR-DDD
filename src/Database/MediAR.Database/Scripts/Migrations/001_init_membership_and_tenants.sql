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
PRINT N'Creating Table [membership].[RolesToPermissions]...';


GO
CREATE TABLE [membership].[RolesToPermissions] (
    [RoleId]       UNIQUEIDENTIFIER NOT NULL,
    [PermissionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RolesToPermissions_RoleId_PermissionId] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC)
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
PRINT N'Creating Table [membership].[UsersToRoles]...';


GO
CREATE TABLE [membership].[UsersToRoles] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsersToRoles_UserId_RoleId] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
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
    PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating Table [tenants].[Tenants]...';


GO
CREATE TABLE [tenants].[Tenants] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             VARCHAR (256)    NOT NULL,
    [ConnectionString] VARCHAR (256)    NOT NULL,
	[ReferUrl] VARCHAR(256) DEFAULT '',
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Foreign Key unnamed constraint on [membership].[RolesToPermissions]...';


GO
ALTER TABLE [membership].[RolesToPermissions] WITH NOCHECK
    ADD FOREIGN KEY ([RoleId]) REFERENCES [membership].[Roles] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [membership].[RolesToPermissions]...';


GO
ALTER TABLE [membership].[RolesToPermissions] WITH NOCHECK
    ADD FOREIGN KEY ([PermissionId]) REFERENCES [membership].[Permissions] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [membership].[UsersToRoles]...';


GO
ALTER TABLE [membership].[UsersToRoles] WITH NOCHECK
    ADD FOREIGN KEY ([UserId]) REFERENCES [membership].[Users] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [membership].[UsersToRoles]...';


GO
ALTER TABLE [membership].[UsersToRoles] WITH NOCHECK
    ADD FOREIGN KEY ([RoleId]) REFERENCES [membership].[Roles] ([Id]);


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
SELECT u.UserName AS [UserName]
	,u.Email AS [Email]
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
PRINT N'Creating Procedure [membership].[count_Users_With_UserName_or_Email]...';


GO
CREATE PROCEDURE [membership].[count_Users_With_UserName_or_Email] @UserName VARCHAR(256)
	,@Email VARCHAR(256)
AS
SELECT COUNT(*)
FROM [membership].[Users]
WHERE UserName = @UserName
	OR Email = @Email
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
	)
GO
PRINT N'Creating Procedure [membership].[assign_Role_to_User_by_Email]...';


GO
CREATE PROCEDURE [membership].[assign_Role_to_User_by_Email] @Email VARCHAR(256)
	,@RoleName VARCHAR(256)
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
			,'No user with Email'
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
	,@RoleName VARCHAR(256)
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
			,'No user with UserName'
			,5;
	END;

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
PRINT N'Update complete.';


GO
