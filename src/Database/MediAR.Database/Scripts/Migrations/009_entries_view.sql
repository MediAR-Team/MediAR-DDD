PRINT N'Creating View [learning].[v_ContentEntries]...';


GO
CREATE VIEW [learning].[v_ContentEntries] AS
SELECT CE.Id,
	CE.ModuleId,
	CE.Ordinal,
	CE.TenantId,
	CE.Title,
	CE.TypeId,
	ET.[Name] AS TypeName,
	CE.[Configuration],
	CE.[Data]
FROM [learning].[ContentEntries] CE
	JOIN learning.EntryTypes ET ON ET.Id = CE.TypeId
GO
PRINT N'Update complete.';