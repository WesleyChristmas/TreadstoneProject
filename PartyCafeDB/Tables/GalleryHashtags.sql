CREATE TABLE [dbo].[GalleryHashtags]
(
	[IdRecord] INT  NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[GalleryId] INT NOT NULL,
	[Hashtag] VARCHAR(256) NOT NULL,
	FOREIGN KEY ([GalleryId]) REFERENCES Gallery(IdRecord)
)
