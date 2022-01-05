DECLARE @Count INT;

SELECT @Count = COUNT(*)
FROM [tenants].[Tenants];

IF @Count = 0
	INSERT INTO [tenants].[Tenants] (
		[Id]
		,[Name]
		,[ConnectionString],
		[ReferUrl]
		)
	VALUES (
		NEWID()
		,'Master'
		,'Server = .; Initial Catalog = MediAR_DDD; Integrated Security = True'
		,''
		)
		,(
		NEWID()
		,'Default'
		,'Server = .; Initial Catalog = MediAR_DDD; Integrated Security = True'
		,'default.mediar.app'
		);
