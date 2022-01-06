CREATE TABLE [membership].[RolesToPermissions] (
	[RoleId] UNIQUEIDENTIFIER NOT NULL
	,[PermissionId] UNIQUEIDENTIFIER NOT NULL
	,CONSTRAINT [PK_RolesToPermissions_RoleId_PermissionId] PRIMARY KEY (
		RoleId
		,PermissionId
		)
	,CONSTRAINT [FK_RolesToPermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [membership].[Roles](Id)
	,CONSTRAINT [FK_RolesToPermissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [membership].[Permissions](Id)
	)
