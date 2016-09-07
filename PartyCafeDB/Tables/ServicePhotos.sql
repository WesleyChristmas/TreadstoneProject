CREATE TABLE [dbo].[ServicePhotos] (
    [IdRecord]  INT            IDENTITY (1, 1) NOT NULL,
    [IdService] INT            NOT NULL,
    [IdPhoto]   INT            NOT NULL,
    [name]      NVARCHAR (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    CONSTRAINT [PK_ServicePhotos] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    CONSTRAINT [FK__ServicePh__IdPho__3D5E1FD2] FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord]),
    CONSTRAINT [FK__ServicePh__IdSer__3F466844] FOREIGN KEY ([IdService]) REFERENCES [dbo].[Services] ([IdRecord])
);

