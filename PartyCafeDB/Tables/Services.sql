CREATE TABLE [dbo].[Services] (
    [IdRecord]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Title]       NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS CONSTRAINT [DF_Services_Title] DEFAULT ('') NULL,
    [IdPhoto]     INT            CONSTRAINT [DF_Services_IdPhoto] DEFAULT ((0)) NOT NULL,
    [Text]        NVARCHAR (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [serviceType] INT            CONSTRAINT [DF_Services_serviceType] DEFAULT ((0)) NOT NULL,
    [UserCreate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]  NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]  DATETIME       CONSTRAINT [DF__Services__DateCr__403A8C7D] DEFAULT (getdate()) NOT NULL,
    [DateUpdate]  DATETIME       NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK__Services__IdPhot__3C69FB99] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

