CREATE PROCEDURE [learning].[ins_ContentEntry] @TenantId UNIQUEIDENTIFIER
	,@ModuleId INT
	,@TypeId INT
	,@Data XML
	,@Config XML
AS
INSERT [learning].[ContentEntries] (
	ModuleId
	,TypeId
	,TenantId
	,[Data]
	,[Configuration]
	)
SELECT @ModuleId
	,@TypeId
	,@TenantId
	,@Data
	,@Config;
