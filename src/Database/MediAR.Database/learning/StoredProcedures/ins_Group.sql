CREATE PROCEDURE [learning].[ins_Group] @Name VARCHAR(256)
	,@TenantId UNIQUEIDENTIFIER
AS
INSERT [learning].[Groups] (
	[Name]
	,[TenantId]
	)
SELECT @Name
	,@TenantId;
