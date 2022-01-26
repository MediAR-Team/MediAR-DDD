

CREATE VIEW [learning].[v_StudentCourses]
AS
SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,STG.StudentId AS StudentId
FROM [learning].[Courses] C
JOIN [learning].[GroupToCourse] GTC ON GTC.TenantId = C.TenantId
	AND GTC.CourseId = C.Id
JOIN [learning].[Groups] g ON G.TenantId = GTC.TenantId
	AND G.Id = GTC.GroupId
JOIN [learning].[StudentToGroup] STG ON STG.TenantId = G.TenantId
	AND STG.GroupId = G.Id
WHERE C.OwnerStudentId IS NULL

UNION ALL

SELECT C.TenantId AS TenantId
	,C.Id AS CourseId
	,C.[Name] AS CourseName
	,C.[Description] AS CourseDescription
	,C.BackgroundImageUrl AS BackgroundImageUrl
	,C.OwnerStudentId AS StudentId
FROM [learning].[Courses] C
WHERE OwnerStudentId IS NOT NULL

