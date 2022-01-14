
GO
PRINT N'Creating User-Defined Table Type [learning].[ut_OrdinalUpdate]...';


GO
CREATE TYPE [learning].[ut_OrdinalUpdate] AS TABLE (
    [Id]      INT NOT NULL,
    [Ordinal] INT NOT NULL);


GO
PRINT N'Altering Table [learning].[ContentEntries]...';


GO
ALTER TABLE [learning].[ContentEntries]
    ADD [Ordinal] INT NOT NULL;


GO
PRINT N'Creating Unique Constraint [learning].[UQ_ContentEntries_ModuleOrdinals]...';


GO
ALTER TABLE [learning].[ContentEntries]
    ADD CONSTRAINT [UQ_ContentEntries_ModuleOrdinals] UNIQUE NONCLUSTERED ([TenantId] ASC, [ModuleId] ASC, [Ordinal] ASC);


GO
PRINT N'Altering Table [learning].[Modules]...';


GO
ALTER TABLE [learning].[Modules]
    ADD [Ordinal] INT NOT NULL;


GO
PRINT N'Creating Unique Constraint [learning].[UQ_Modules_CounrseOrdinals]...';


GO
ALTER TABLE [learning].[Modules]
    ADD CONSTRAINT [UQ_Modules_CounrseOrdinals] UNIQUE NONCLUSTERED ([TenantId] ASC, [CourseId] ASC, [Ordinal] ASC);


GO
PRINT N'Refreshing View [learning].[v_CourseAggregate]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[v_CourseAggregate]';


GO
PRINT N'Altering Procedure [learning].[ins_ContentEntry]...';


GO
ALTER PROCEDURE [learning].[ins_ContentEntry] @TenantId UNIQUEIDENTIFIER
	,@ModuleId INT
	,@TypeId INT
	,@Data XML
	,@Config XML
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
	)
SELECT @ModuleId
	,@TypeId
	,@TenantId
	,@Data
	,@Config;
GO
PRINT N'Altering Procedure [learning].[ins_Module]...';


GO
ALTER PROCEDURE [learning].[ins_Module] @Name VARCHAR(256)
	,@CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DECLARE @NewOrdinal INT;

SELECT @NewOrdinal = MAX(Ordinal) + 1
FROM [learning].[Modules]
WHERE CourseId = @CourseId
	AND TenantId = @TenantId;

IF @NewOrdinal IS NULL
BEGIN
	SET @NewOrdinal = 1;
END

INSERT [learning].[Modules] (
	[Name]
	,[CourseId]
	,[TenantId]
	,[Ordinal]
	)
SELECT @Name
	,@CourseId
	,@TenantId
	,@NewOrdinal;
GO
PRINT N'Creating Procedure [learning].[upd_ReorderContentEntries]...';


GO
CREATE PROCEDURE [learning].[upd_ReorderContentEntries] @NewOrder ut_OrdinalUpdate READONLY
	,@TenantId UNIQUEIDENTIFIER
AS
DECLARE @ModulesCount INT = 0
	,@ModuleId INT;

SELECT @ModulesCount = COUNT(DISTINCT ModuleId)
FROM @NewOrder NewO
JOIN learning.ContentEntries CE ON CE.Id = NewO.Id
	AND CE.TenantId = @TenantId;

IF @ModulesCount <> 1
BEGIN
		;

	THROW 60000
		,'Invalid ordinal'
		,5;
END;

IF EXISTS (
		SELECT 1
		FROM @NewOrder NO_1
		JOIN learning.ContentEntries CE_1 ON CE_1.Id = NO_1.Id
			AND CE_1.TenantId = @TenantId
		WHERE CE_1.Ordinal NOT IN (
				SELECT CE_2.Ordinal
				FROM learning.ContentEntries CE_2
				JOIN @NewOrder NO_2 ON NO_2.Ordinal = CE_2.Ordinal
				WHERE TenantId = @TenantId
				)
		)
BEGIN
		;

	THROW 60000
		,'Invalid ordinal'
		,5;
END;

UPDATE CE
SET Ordinal = NewO.Ordinal
FROM learning.ContentEntries CE
JOIN @NewOrder NewO ON NewO.Id = CE.Id
WHERE CE.TenantId = @TenantId;
GO
PRINT N'Creating Procedure [learning].[upd_ReorderModules]...';


GO
CREATE PROCEDURE [learning].[upd_ReorderModules] @NewOrder ut_OrdinalUpdate READONLY
	,@TenantId UNIQUEIDENTIFIER
AS
DECLARE @CoursesCount INT = 0
	,@CourseId INT;

SELECT @CoursesCount = COUNT(DISTINCT CourseId)
FROM @NewOrder NewO
JOIN learning.Modules M ON M.Id = NewO.Id
	AND M.TenantId = @TenantId;

IF @CoursesCount <> 1
BEGIN
		;

	THROW 60000
		,'Invalid ordinal'
		,5;
END;

IF EXISTS (
		SELECT 1
		FROM @NewOrder NO_1
		JOIN learning.Modules M_1 ON M_1.Id = NO_1.Id
			AND M_1.TenantId = @TenantId
		WHERE M_1.Ordinal NOT IN (
				SELECT M_2.Ordinal
				FROM learning.Modules M_2
				JOIN @NewOrder NO_2 ON NO_2.Ordinal = M_2.Ordinal
				WHERE TenantId = @TenantId
				)
		)
BEGIN
		;

	THROW 60000
		,'Invalid ordinal'
		,5;
END;

UPDATE M
SET Ordinal = NewO.Ordinal
FROM learning.Modules M
JOIN @NewOrder NewO ON NewO.Id = M.Id
WHERE M.TenantId = @TenantId;
GO
PRINT N'Refreshing Procedure [learning].[del_Course]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[del_Course]';


GO
PRINT N'Refreshing Procedure [learning].[del_Module]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[del_Module]';


GO
PRINT N'Refreshing Procedure [learning].[upd_ContentEntry]...';


GO
EXECUTE sp_refreshsqlmodule N'[learning].[upd_ContentEntry]';


GO
PRINT N'Update complete.';
