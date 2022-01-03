GO
PRINT N'Creating View [learning].[v_EntryTypes]...';


GO
CREATE VIEW [learning].[v_EntryTypes]
AS
SELECT [Name]
	,[HandlerClass]
FROM [learning].[EntryTypes]
GO
PRINT N'Creating Procedure [learning].[del_Course]...';


GO
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
GO
PRINT N'Creating Procedure [learning].[del_Module]...';


GO
CREATE PROCEDURE [learning].[del_Module] @ModuleId INT
	,@TenantId UNIQUEIDENTIFIER
AS
DELETE [learning].[ContentEntries]
WHERE ModuleId = @ModuleId
	AND TenantId = @TenantId

DELETE [learning].[Modules]
WHERE Id = @ModuleId
	AND TenantId = @TenantId
GO
PRINT N'Creating Procedure [learning].[ins_ContentEntry]...';


GO
CREATE PROCEDURE [learning].[ins_ContentEntry] @TenantId UNIQUEIDENTIFIER
	,@ModuleId INT
	,@TypeId INT
	,@Data XML
	,@Config XML
AS
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
PRINT N'Creating Procedure [learning].[ins_Group]...';


GO
CREATE PROCEDURE [learning].[ins_Group] @Name VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
INSERT [learning].[Groups] (
	[Name]
	,[TenantId]
	)
SELECT @Name
	,@TenantId;
GO
PRINT N'Creating Procedure [learning].[ins_Module]...';


GO
CREATE PROCEDURE [learning].[ins_Module] @Name VARCHAR(256)
	,@CourseId INT
	,@TenantId UNIQUEIDENTIFIER
AS
INSERT [learning].[Modules] (
	[Name]
	,[CourseId]
	,[TenantId]
	)
SELECT @Name
	,@CourseId
	,@TenantId;
GO
PRINT N'Update complete.';

