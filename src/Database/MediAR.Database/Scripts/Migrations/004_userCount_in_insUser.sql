GO
PRINT N'Altering Procedure [membership].[ins_User]...';


GO
ALTER PROCEDURE [membership].[ins_User] @UserName VARCHAR(256)
	,@Email VARCHAR(256)
	,@PasswordHash VARCHAR(512)
	,@FirstName VARCHAR(256)
	,@LastName VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
DECLARE @Count AS INT;

EXEC @Count = [membership].[count_Users_With_UserName_or_Email] @UserName = @UserName
	,@Email = @Email;

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
PRINT N'Update complete.';


GO