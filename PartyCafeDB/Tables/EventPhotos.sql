CREATE TABLE [dbo].[EventPhotos] (
    [IdRecord] INT           IDENTITY (1, 1) NOT NULL,
    [IdEvent]  INT           NOT NULL,
    [IdPhoto]  INT           NOT NULL,
    [name]     VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_EventPhotos] PRIMARY KEY CLUSTERED ([IdRecord] ASC),
    FOREIGN KEY ([IdEvent]) REFERENCES [dbo].[Events] ([IdRecord]),
    FOREIGN KEY ([IdPhoto]) REFERENCES [dbo].[Photos] ([IdRecord])
);

