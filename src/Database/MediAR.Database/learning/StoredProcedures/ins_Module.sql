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
