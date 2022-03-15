CREATE VIEW [learning].[v_StudentSubmissions] AS
SELECT ss.Id,
	ss.EntryId,
	ss.StudentId,
	ss.TenantId,
	ss.[Data],
	ss.CreatedAt,
	ss.ChangedAt,
	ce.TypeId
FROM learning.StudentSubmissions ss
	JOIN learning.ContentEntries ce
		ON ce.Id = ss.EntryId AND ce.TenantId = ss.TenantId
