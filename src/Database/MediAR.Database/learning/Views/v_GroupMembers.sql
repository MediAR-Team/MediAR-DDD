CREATE VIEW [learning].[v_GroupMembers]
AS
SELECT G.Id AS [GroupId]
	,G.[Name] AS [GroupName]
	,S.Id AS [StudentId]
	,S.UserName AS [UserName]
	,S.Email AS [Email]
	,S.FirstName AS [FirstName]
	,S.LastName AS [LastName]
	,STG.TenantId AS [TenantId]
FROM [learning].[StudentToGroup] STG
JOIN [learning].[Students] S ON S.Id = STG.StudentId
	AND S.TenantId = STG.TenantId
JOIN [learning].[Groups] G ON G.Id = STG.GroupId
	AND G.TenantId = STG.TenantId
