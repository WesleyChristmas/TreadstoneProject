CREATE TABLE [dbo].[Users] (
    [IdRecord]      INT              IDENTITY (1, 1) NOT NULL,
    [UserName]      NVARCHAR (50)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Description]   NVARCHAR (255)   NULL,
    [PasswordHash]  NVARCHAR (255)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [UserCreate]    NVARCHAR (50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [UserUpdate]    NVARCHAR (50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [DateCreate]    DATETIME         CONSTRAINT [DF_User_DateCreate] DEFAULT (getdate()) NULL,
    [DateUpdate]    DATETIME         NULL,
    [UserId]        UNIQUEIDENTIFIER CONSTRAINT [DF_Users_UserId] DEFAULT (newid()) NOT NULL,
    [SecurityStamp] NVARCHAR (256)   NULL,
    [TimeLimit]     DATETIME         NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

