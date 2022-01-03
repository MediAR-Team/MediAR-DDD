GO
PRINT N'Creating Foreign Key [learning].[FK_ContentEntries_Modules]...';


GO
ALTER TABLE [learning].[ContentEntries] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentEntries_Modules] FOREIGN KEY ([ModuleId], [TenantId]) REFERENCES [learning].[Modules] ([Id], [TenantId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [learning].[ContentEntries] WITH CHECK CHECK CONSTRAINT [FK_ContentEntries_Modules];


GO
PRINT N'Update complete.';


GO
