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