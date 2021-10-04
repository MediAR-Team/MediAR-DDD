﻿CREATE TABLE [membership].[Users] (
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
	,[UserName] VARCHAR(256) NOT NULL
	,[Email] VARCHAR(256) NOT NULL
	,[PasswordHash] VARCHAR(512) NOT NULL
	,[FirstName] VARCHAR(256)
	,[LastName] VARCHAR(256)
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	)
