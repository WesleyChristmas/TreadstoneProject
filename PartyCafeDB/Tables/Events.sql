CREATE TABLE [dbo].[Events] (
    [IdRecord]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [EventDate]   DATETIME       NOT NULL,
    [IdPhoto]     INT            CONSTRAINT [DF_Events_IdPhoto] DEFAULT ((0)) NOT NULL,
    [UserCreate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]  DATETIME       CONSTRAINT [DF_Events_DateCreate] DEFAULT (getdate()) NOT NULL,
    [DateUpdate]  DATETIME       NULL,
    [description] NVARCHAR (255) CONSTRAINT [DF_Events_description] DEFAULT ('') NOT NULL,
    [isOpen] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK_Events_Photos] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

