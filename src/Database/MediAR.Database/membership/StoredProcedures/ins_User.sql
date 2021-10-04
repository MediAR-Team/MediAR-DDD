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
