

CREATE VIEW [learning].[v_StudentCourses]
AS
SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,G.Id AS GroupId
	,G.[Name] AS GroupName
	,STG.StudentId AS StudentId
	,S.UserName AS UserName
	,S.Email AS Email
FROM [learning].[Courses] C
JOIN [learning].[GroupToCourse] GTC ON GTC.TenantId = C.TenantId
	AND GTC.CourseId = C.Id
JOIN [learning].[Groups] g ON G.TenantId = GTC.GroupId
	AND G.Id = GTC.GroupId
JOIN [learning].[StudentToGroup] STG ON STG.TenantId = G.TenantId
	AND STG.GroupId = G.Id
JOIN [learning].[Students] S ON S.Id = STG.StudentId
	AND S.TenantId = STG.TenantId;

