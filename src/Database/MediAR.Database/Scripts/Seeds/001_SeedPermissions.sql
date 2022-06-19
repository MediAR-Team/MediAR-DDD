DECLARE @Data XML = '<?xml version="1.0" encoding="utf-8" ?>
<root>
	<role name="Admin" description="Admin (per tenant)">
		<permission name="CreateUser" description=""></permission>
		<permission name="UpdateUserRole" description=""></permission>

		<permission name="GetAllCourses" description=""></permission>
		<permission name="CreateCourse" description=""></permission>
		<permission name="UpdateCourse" description=""></permission>
		<permission name="DeleteCourse" description=""></permission>
		<permission name="AddStudentToGoup" description=""></permission>
		<permission name="DelStudentFromGroup" description=""></permission>
		<permission name="AddGroupToCourse" description=""></permission>
		<permission name="CreateGroup" description=""></permission>
		<permission name="DeleteGroup" description=""></permission>
	</role>
	<role name="Instructor" description="Instructor (per tenant)">
		<permission name="GetAllCourses" description=""></permission>
		<permission name="CreateCourse" description=""></permission>
		<permission name="UpdateOwnCourse" description=""></permission>
		<permission name="DeleteOwnCourse" description=""></permission>
		<permission name="AddGroupToOwnCourse" description=""></permission>
		<permission name="GetAllSubmissionsForEntry" description=""></permission>
	</role>
	<role name="Student" description="Student (per tenant)">
		<permission name="CreateCourse" description=""></permission>
		<permission name="UpdateOwnCourse" description=""></permission>
		<permission name="GetOwnSubmissionForEntry" description=""></permission>
		<permission name="CreateSubmissions" description=""></permission>
	</role>
</root>';

MERGE INTO [membership].[Roles] [Role]
USING @Data.nodes('root/role') X(y)
	ON X.y.value('@name', 'VARCHAR(256)') = [Role].[Name]
WHEN NOT MATCHED
	THEN
		INSERT (
			[Id]
			,[Name]
			,[Description]
			)
		VALUES (
			NEWID()
			,X.y.value('@name', 'VARCHAR(256)')
			,X.y.value('@description', 'VARCHAR(512)')
			)
WHEN MATCHED
	THEN
		UPDATE
		SET [Role].[Description] = X.y.value('@description', 'VARCHAR(512)');

WITH CTE
AS (
	SELECT DISTINCT X.y.value('@name', 'VARCHAR(256)') AS [Name]
		,X.y.value('@description', 'VARCHAR(512)') AS [Description]
	FROM @Data.nodes('//permission') X(y)
	)
MERGE INTO [membership].[Permissions] [Permission]
USING CTE
	ON CTE.[Name] = [Permission].[Name]
WHEN NOT MATCHED
	THEN
		INSERT (
			[Id]
			,[Name]
			,[Description]
			)
		VALUES (
			NEWID()
			,CTE.[Name]
			,CTE.[Description]
			)
WHEN MATCHED
	THEN
		UPDATE
		SET [Permission].[Description] = CTE.[Description];

DELETE
FROM [membership].[RolesToPermissions];

INSERT INTO [membership].[RolesToPermissions] (
	RoleId
	,PermissionId
	)
SELECT R.Id
	,P.Id
FROM @Data.nodes('//permission') X(y)
JOIN [membership].[Roles] R ON X.y.value('../@name', 'VARCHAR(256)') = R.[Name]
JOIN [membership].[Permissions] P ON X.y.value('@name', 'VARCHAR(256)') = P.[Name]
LEFT JOIN [membership].[RolesToPermissions] [existing] ON [existing].[PermissionId] = P.[Id]
	AND [existing].[RoleId] = R.[Id]
WHERE [existing].[RoleId] IS NULL;
