CREATE PROCEDURE [learning].[ins_Module] @Name VARCHAR(256)
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
