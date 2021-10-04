CREATE TABLE [membership].[RolesToPermissions] (
	[RoleId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [membership].[Roles](Id)
	,[PermissionId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [membership].[Permissions](Id)
	,CONSTRAINT [PK_RolesToPermissions_RoleId_PermissionId] PRIMARY KEY (
		RoleId
		,PermissionId
		)
	)
