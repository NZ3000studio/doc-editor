CREATE TABLE [dbo].[Logs] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Log]     NVARCHAR (200) NOT NULL,
    [LogData] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

