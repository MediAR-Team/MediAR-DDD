CREATE TABLE [membership].[UsersToRoles] (
	[UserId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [membership].[Users](Id)
	,[RoleId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [membership].[Roles](Id)
	,CONSTRAINT [PK_UsersToRoles_UserId_RoleId] PRIMARY KEY (
		UserId
		,RoleId
		)
	)
