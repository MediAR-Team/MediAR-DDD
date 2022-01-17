CREATE VIEW [learning].[v_GroupCourses]
	AS SELECT 
	G.Id AS GroupId,
	G.[Name] AS GroupName,
	C.Id AS CourseId,
	C.[Name] AS CourseName,
	C.[Description] AS CourseDescription,
	C.BackgroundImageUrl AS BackgroundImageUrl,
	GTC.TenantId AS TenantId
	FROM learning.Groups G
	JOIN learning.GroupToCourse GTC ON GTC.TenantId = G.TenantId AND GTC.GroupId = G.Id
	JOIN learning.Courses C ON C.TenantId = GTC.TenantId AND C.Id = GTC.CourseId

