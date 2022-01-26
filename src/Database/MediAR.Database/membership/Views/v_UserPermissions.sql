CREATE VIEW [membership].[v_UserPermissions]
	WITH SCHEMABINDING
AS
SELECT u.Id AS [UserId]
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,p.Id AS [PermissionId]
	,p.[Name] AS [PermissionName]
	,u.TenantId AS [TenantId]
FROM [membership].[Users] u
JOIN [membership].[UsersToRoles] utr ON utr.UserId = u.Id
JOIN [membership].[RolesToPermissions] rtp ON rtp.RoleId = utr.RoleId
JOIN [membership].[Permissions] p ON p.Id = rtp.PermissionId
