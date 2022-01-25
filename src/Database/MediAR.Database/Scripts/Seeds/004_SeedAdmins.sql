

CREATE TABLE #TUsers (
	Id uniqueidentifier
	,UserName VARCHAR(256)
	,Email VARCHAR(256)
	,PasswordHash VARCHAR(1024)
	,FirstName VARCHAR(256)
	,LastName VARCHAR(256)
	,TenantId UNIQUEIDENTIFIER
	);

DECLARE @TInserted TABLE (Id uniqueidentifier, TenantId uniqueidentifier);

INSERT INTO #TUsers (
	Id
	,UserName
	,Email
	,PasswordHash
	,FIrstName
	,LastName
	,TenantId
	)
SELECT NEWID()
	,'admin'
	,'admin@mail.ua'
	,'ALayPx2WSC0GITkrPECKK5SCsPCZ2uhh0aNWgjbykPRPrFQcbt/ZBzp8cg6v11ZnmA=='
	,'admin'
	,'admin'
	,t.Id
FROM tenants.Tenants t;

MERGE INTO membership.Users tgt
USING #TUsers src
	ON tgt.UserName = src.UserName
		AND tgt.TenantId = src.TenantId
WHEN NOT MATCHED
	THEN
		INSERT (
			Id
			,UserName
			,Email
			,PasswordHash
			,FIrstName
			,LastName
			,TenantId
			)
		VALUES (
			src.Id
			,src.UserName
			,src.Email
			,src.PasswordHash
			,src.FIrstName
			,src.LastName
			,src.TenantId
			)
	OUTPUT INSERTED.Id, INSERTED.TenantId INTO @TInserted;

INSERT membership.UsersToRoles (UserId, TenantId, RoleId)
SELECT ti.Id, ti.TenantId, r.Id
FROM @TInserted ti
JOIN membership.Roles R ON R.[Name] = 'Admin';

