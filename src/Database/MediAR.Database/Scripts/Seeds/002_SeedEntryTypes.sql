CREATE TABLE #EntryTypes (
	[Name] VARCHAR(256)
	,[HandlerClass] VARCHAR(256)
	);

INSERT #EntryTypes
VALUES (
	'Lecture'
	,'MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.LectureContentEntryHandler'
	),
	('SubmissionTask', 'MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.SubmissionTaskEntryHandler');

MERGE INTO [learning].[EntryTypes] dest
USING #EntryTypes src
	ON src.[Name] = dest.[Name]
WHEN NOT MATCHED
	THEN
		INSERT (
			[Name]
			,[HandlerClass]
			)
		VALUES (
			src.[Name]
			,src.HandlerClass
			);

