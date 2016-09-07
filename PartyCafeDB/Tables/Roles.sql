CREATE TABLE [dbo].[Roles] (
    [RoleId]      UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (256)   NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

