CREATE PROCEDURE [learning].[upd_ContentEntry] @Id INT
	,@TenantId UNIQUEIDENTIFIER
	,@Data XML
	,@Config XML
	,@TypeId INT
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
WHERE Id = @Id
	AND TenantId = @TenantId;
