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
