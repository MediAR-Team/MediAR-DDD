CREATE TABLE [membership].[UsersToRoles] (
	[UserId] UNIQUEIDENTIFIER NOT NULL
	,[RoleId] UNIQUEIDENTIFIER NOT NULL
	,[TenantId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_UsersToRoles_UserId_RoleId] PRIMARY KEY (
		UserId
		,RoleId
		,TenantId
		)
	,CONSTRAINT [FK_UsersToRoles_Users] FOREIGN KEY (
		UserId
		,TenantId
		) REFERENCES [membership].[Users](Id, TenantId)
	,CONSTRAINT [FK_UsersToRoles_Roles] FOREIGN KEY (RoleId) REFERENCES [membership].[Roles](Id)
	)
