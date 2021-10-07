﻿CREATE VIEW [membership].[v_Users]
	WITH SCHEMABINDING
AS
SELECT u.Id AS Id
	,u.UserName AS [UserName]
	,u.Email AS [Email]
	,u.PasswordHash AS [PasswordHash]
	,u.FirstName AS [FirstName]
	,u.LastName AS [LastName]
	,u.TenantId [TenantId]
FROM [membership].Users u
