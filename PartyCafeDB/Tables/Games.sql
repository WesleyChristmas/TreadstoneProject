CREATE TABLE [dbo].[Games] (
    [IdRecord]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Platform]    NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Description] NVARCHAR (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [IdPhoto]     INT            CONSTRAINT [DF_Games_IdPhoto] DEFAULT ((0)) NOT NULL,
    [UserCreate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]  DATETIME       CONSTRAINT [DF_Games_DateCreate] DEFAULT (getdate()) NOT NULL,
    [DateUpdate]  DATETIME       NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK_Games_Photos] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

