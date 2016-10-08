CREATE TABLE [dbo].[Photos] (
    [IdRecord]   INT            IDENTITY (0, 1) NOT NULL,
    [FileName]   NVARCHAR (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Path]       NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Hashtag] NVARCHAR(256) NULL,
    [UserCreate] NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate] NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate] DATETIME       CONSTRAINT [DF_Photos_DateCreate] DEFAULT (getdate()) NOT NULL,
    [DateUpdate] DATETIME       NULL,

    CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED ([IdRecord] ASC)
);

