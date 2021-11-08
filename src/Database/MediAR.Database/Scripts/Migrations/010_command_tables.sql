GO
PRINT N'Creating Table [learning].[InboxMessages]...';


GO
CREATE TABLE [learning].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [learning].[InternalCommands]...';


GO
CREATE TABLE [learning].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [membership].[InboxMessages]...';


GO
CREATE TABLE [membership].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [membership].[InternalCommands]...';


GO
CREATE TABLE [membership].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [tenants].[InboxMessages]...';


GO
CREATE TABLE [tenants].[InboxMessages] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OccuredOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [tenants].[InternalCommands]...';


GO
CREATE TABLE [tenants].[InternalCommands] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Type]        NVARCHAR (256)   NOT NULL,
    [Data]        NVARCHAR (MAX)   NOT NULL,
    [ProcessedOn] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Update complete.';


GO                                      
