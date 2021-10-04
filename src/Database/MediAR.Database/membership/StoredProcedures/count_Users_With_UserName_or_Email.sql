CREATE PROCEDURE [membership].[count_Users_With_UserName_or_Email] @UserName VARCHAR(256)
	,@Email VARCHAR(256)
AS
SELECT COUNT(*)
FROM [membership].[Users]
WHERE UserName = @UserName
	OR Email = @Email
