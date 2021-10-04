CREATE INDEX [IX_Users_TenantId] ON [membership].[Users] (TenantId)
GO

CREATE INDEX [IX_Users_UserName] ON [membership].[Users] (UserName)
GO

CREATE INDEX [IX_Users_Email] ON [membership].[Users] (Email)
GO

CREATE INDEX [IX_Roles_Name] ON [membership].[Roles] ([Name])
GO

CREATE INDEX [IX_Permissions_Name] ON [membership].[Permissions] ([Name])
GO
