CREATE TABLE [dbo].[MenuItems] (
    [IdRecord]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [IdPhoto]     INT            NOT NULL,
    [description] NVARCHAR (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Weight]      NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Price]       MONEY          NOT NULL,
    [Country]     NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Platform]    NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserCreate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]  DATETIME       CONSTRAINT [DF__MenuItems__DateC__70A8B9AE] DEFAULT (getdate()) NOT NULL,
    [DateUpdate]  DATETIME       NULL,
    [IdGroup]     INT            NOT NULL,
    CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK_MenuItems_MenuGroups] FOREIGN KEY ([IdGroup]) REFERENCES [dbo].[MenuGroups] ([IdRecord]),
    CONSTRAINT [FK_MenuItems_Photos] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

