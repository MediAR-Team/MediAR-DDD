CREATE VIEW [membership].[v_Users]
	WITH SCHEMABINDING
AS
SELECT u.Id AS Id
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,u.PasswordHash AS [PasswordHash]
	,u.FirstName AS [FirstName]
	,u.LastName AS [LastName]
	,u.TenantId [TenantId]
	,r.Name AS RoleName
FROM [membership].Users u
JOIN [membership].UsersToRoles utr ON utr.TenantId = u.TenantId AND utr.UserId = u.Id
JOIN membership.Roles r ON r.Id = utr.RoleId
