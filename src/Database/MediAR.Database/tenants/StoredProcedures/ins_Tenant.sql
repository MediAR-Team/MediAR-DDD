CREATE PROCEDURE [tenants].[ins_Tenant] @Name VARCHAR(256)
	,@ConnectionString VARCHAR(256) = ''
AS
DECLARE @SameNameCount INT;

SELECT @SameNameCount = COUNT(*)
FROM [tenants].[Tenants]
WHERE [Name] = @Name;

IF @SameNameCount = 0
BEGIN
	INSERT INTO [tenants].[Tenants] (
		[Id]
		,[Name]
		,[ConnectionString]
		)
	VALUES (
		NEWID()
		,@Name
		,@ConnectionString
		);

	SELECT @@IDENTITY;
END
ELSE
	THROW 60000
		,'Tenant with same name exists'
		,5;

RETURN 0
