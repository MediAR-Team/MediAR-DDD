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
