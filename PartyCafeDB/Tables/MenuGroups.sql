CREATE TABLE [dbo].[MenuGroups] (
    [IdRecord]   INT            IDENTITY (1, 1) NOT NULL,
    [GroupName]  NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [IdParent]   INT            NULL,
    [UserCreate] NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate] NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate] DATETIME       CONSTRAINT [DF_MenuGroups_DateCreate] DEFAULT (getdate()) NOT NULL,
    [DateUpdate] DATETIME       NULL,
    [IdPhoto]    INT            CONSTRAINT [DF_MenuGroups_IdPhoto] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MenuGroups] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK_MenuGroups_MenuGroups] FOREIGN KEY ([IdParent]) REFERENCES [dbo].[MenuGroups] ([IdRecord]),
    CONSTRAINT [FK_MenuGroups_Photos] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

