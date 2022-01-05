IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'app' )
    EXEC('CREATE SCHEMA [app] AUTHORIZATION [dbo]');
GO