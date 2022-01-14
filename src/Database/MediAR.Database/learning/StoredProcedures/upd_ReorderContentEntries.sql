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