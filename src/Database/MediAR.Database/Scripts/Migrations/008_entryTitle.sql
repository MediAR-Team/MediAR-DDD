GO
PRINT N'Altering Table [learning].[ContentEntries]...';


GO
ALTER TABLE [learning].[ContentEntries]
    ADD [Title] VARCHAR (256) NOT NULL;


GO
PRINT N'Altering View [learning].[v_CourseAggregate]...';


GO
ALTER VIEW [learning].[v_CourseAggregate]
	AS SELECT C.[Id] AS [CourseId],
	C.[TenantId] AS [TenantId],
	C.[Name] AS [CourseName],
	C.[Description] AS [CourseDescription],
	C.[BackgroundImageUrl] AS [CourseBackgroundImageUrl],
	M.[Id] AS [ModuleId],
	M.[Name] AS [ModuleName],
	M.[Ordinal] AS [ModuleOrdinal],
	CE.[Id] AS [EntryId],
	ET.[Name] AS [EntryTypeName],
	CE.[Configuration] AS [EntryConfiguration],
	CE.[Data] AS [EntryData],
	CE.[Ordinal] AS [EntryOrdinal],
	CE.[Title] AS [EntryTitle]
 	FROM [learning].[Courses] C
	LEFT JOIN [learning].[Modules] M ON M.[CourseId] = C.[Id] AND M.[TenantId] = C.[TenantId]
	LEFT JOIN [learning].[ContentEntries] CE ON CE.[ModuleId] = M.[Id] AND CE.[TenantId] = M.[TenantId]
	LEFT JOIN [learning].[EntryTypes] ET ON ET.Id = CE.TypeId;
GO
PRINT N'Altering Procedure [learning].[ins_ContentEntry]...';


GO
ALTER PROCEDURE [learning].[ins_ContentEntry] @TenantId UNIQUEIDENTIFIER
	,@ModuleId INT
	,@TypeId INT
	,@Data XML
	,@Config XML
	,@Title VARCHAR(256)
AS
DECLARE @NewOrdinal INT;

SELECT @NewOrdinal = MAX(Ordinal) + 1
FROM [learning].[ContentEntries]
WHERE ModuleId = @ModuleId
	AND TenantId = @TenantId;

IF @NewOrdinal IS NULL
BEGIN
	SET @NewOrdinal = 1;
END

INSERT [learning].[ContentEntries] (
	ModuleId
	,TypeId
	,TenantId
	,[Data]
	,[Configuration]
	,[Ordinal]
	,[Title]
	)
SELECT @ModuleId
	,@TypeId
	,@TenantId
	,@Data
	,@Config
	,@NewOrdinal
	,@Title;
GO
PRINT N'Altering Procedure [learning].[upd_ContentEntry]...';


GO
ALTER PROCEDURE [learning].[upd_ContentEntry] @Id INT
	,@TenantId UNIQUEIDENTIFIER
	,@Data XML
	,@Config XML
	,@TypeId INT
	,@Title VARCHAR(256)
AS
DECLARE @EntryExists BIT = 0
	,@CorrespondingType BIT = 0;

SELECT @EntryExists = 1
	,@CorrespondingType = CASE 
		WHEN CE.TypeId = @TypeId
			THEN 1
		ELSE 0
		END
FROM learning.ContentEntries CE
WHERE CE.Id = @Id
	AND CE.TenantId = @TenantId

IF @EntryExists = 0
	OR @CorrespondingType = 0
BEGIN
		;

	THROW 60000
		,'Entry does not exist or type mismatch'
		,5;
END;

UPDATE learning.ContentEntries
SET [Data] = @Data
	,[Configuration] = @Config
	,[Title] = ISNULL(@Title, [Title])
WHERE Id = @Id
	AND TenantId = @TenantId;
GO
PRINT N'Refreshing Procedure [learning].[del_Course]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[del_Course]';


GO
PRINT N'Refreshing Procedure [learning].[del_Module]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[del_Module]';


GO
PRINT N'Refreshing Procedure [learning].[upd_ReorderContentEntries]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[upd_ReorderContentEntries]';


GO
PRINT N'Update complete.';

