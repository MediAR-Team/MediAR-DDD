CREATE VIEW [learning].[v_StudentSubmissions] AS
SELECT ss.Id,
	ss.EntryId,
	ss.StudentId,
	ss.TenantId,
	ss.[Data],
	ss.CreatedAt,
	ss.ChangedAt,
	sm.MarkValue AS SubmissionMarkMarkValue,
	sm.Comment AS SubmissionMarkComment,
	sm.CreatedAt AS SubmissionMarkCreatedAt,
	sm.ChangedAt AS SubmissionMarkChangedAt,
	ce.TypeId
FROM learning.StudentSubmissions ss
	LEFT JOIN learning.SubmissionMarks sm ON sm.TenantId = ss.TenantId AND sm.SubmissionId = ss.Id
	JOIN learning.ContentEntries ce
		ON ce.Id = ss.EntryId AND ce.TenantId = ss.TenantId
