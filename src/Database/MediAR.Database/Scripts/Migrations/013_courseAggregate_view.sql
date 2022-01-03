
GO

PRINT N'Creating View [learning].[v_CourseAggregate]...';
GO

CREATE VIEW [learning].[v_CourseAggregate]
AS
SELECT C.[Id] AS [CourseId]
	,C.[TenantId] AS [TenantId]
	,C.[Name] AS [CourseName]
	,C.[Description] AS [CourseDescription]
	,C.[BackgroundImageUrl] AS [CourseBackgroundImageUrl]
	,M.[Id] AS [ModuleId]
	,M.[Name] AS [ModuleName]
	,CE.[Id] AS [EntryId]
	,CE.[Configuration] AS [EntryConfiguration]
	,CE.[Data] AS [EntryData]
FROM [learning].[Courses] C
LEFT JOIN [learning].[Modules] M ON M.[CourseId] = C.[Id]
	AND M.[TenantId] = C.[TenantId]
LEFT JOIN [learning].[ContentEntries] CE ON CE.[ModuleId] = M.[Id]
	AND CE.[TenantId] = M.[TenantId]
GO

PRINT N'Update complete.';
