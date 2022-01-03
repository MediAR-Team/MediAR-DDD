CREATE PROCEDURE [learning].[del_Course] @CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DELETE [Entry]
FROM [learning].[ContentEntries] [Entry]
JOIN [learning].[Modules] [Module] ON [Module].Id = [Entry].[ModuleId]
	AND [Module].TenantId = [Entry].TenantId
WHERE [Module].CourseId = @CourseId
	AND [Module].[TenantId] = @TenantId
	AND [Entry].[TenantId] = @TenantId

DELETE [learning].[Modules]
WHERE [CourseId] = @CourseId
	AND [TenantId] = @TenantId

DELETE [learning].[Courses]
WHERE [Id] = @CourseId
	AND [TenantId] = @TenantId
