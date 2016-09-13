CREATE TABLE [dbo].[Gallery] (
    [IdRecord]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [IdPhoto]     INT            CONSTRAINT [DF_Gallery_IdPhoto] DEFAULT ((0)) NOT NULL,
    [Description] NVARCHAR (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [UserCreate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]  DATETIME       CONSTRAINT [DF_Gallery_DateCreate] DEFAULT (getdate()) NOT NULL,
    [DateUpdate]  DATETIME       NULL,
    [Tag]         VARCHAR (256)  NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK_Gallery_Photos] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

