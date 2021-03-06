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
