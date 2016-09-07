CREATE TABLE [dbo].[sysdiagrams] (
    [name]         NVARCHAR (128)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [principal_id] INT             NOT NULL,
    [diagram_id]   INT             NOT NULL,
    [version]      INT             NULL,
    [definition]   VARBINARY (MAX) NULL
);

