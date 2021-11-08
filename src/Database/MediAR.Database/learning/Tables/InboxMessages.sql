﻿CREATE TABLE [learning].[InboxMessages] (
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
	,[OccuredOn] DATETIME NOT NULL
	,[Type] NVARCHAR(256) NOT NULL
	,[Data] NVARCHAR(MAX) NOT NULL
	,[ProcessedOn] DATETIME
	)
