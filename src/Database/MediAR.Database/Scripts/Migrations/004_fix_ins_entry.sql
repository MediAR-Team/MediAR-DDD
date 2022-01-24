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
	,[Ordinal]
	)
SELECT @ModuleId
	,@TypeId
	,@TenantId
	,@Data
	,@Config
	,@NewOrdinal;
GO
PRINT N'Update complete.';


GO