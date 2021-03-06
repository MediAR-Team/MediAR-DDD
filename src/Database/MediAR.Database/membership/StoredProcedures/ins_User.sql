

CREATE PROCEDURE [membership].[ins_User] @UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@PasswordHash VARCHAR(512)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
	,@InitialRole VARCHAR(256)
	,@UserId UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Count AS INT;
SET @UserId = NEWID();

SELECT @Count = COUNT(*)
FROM [membership].[Users]
WHERE Email = @Email
	OR UserName = @UserName;

IF @Count = 0
BEGIN
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
		@UserId
		,@UserName
		,@Email
		,@PasswordHash
		,@FirstName
		,@LastName
		,@TenantId
		);

	INSERT membership.UsersToRoles (
		UserId
		,TenantId
		,RoleId
		)
	SELECT @UserId
		,@TenantId
		,r.Id
	FROM membership.Roles r
	WHERE r.[Name] = @InitialRole;
END
ELSE
	THROW 60000
		,'User with UserName or Email already exists'
		,5;

