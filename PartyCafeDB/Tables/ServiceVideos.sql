CREATE TABLE [dbo].[ServiceVideos]
(
	[IdRecord] INT NOT NULL PRIMARY KEY clustered IDENTITY(1, 1), 
	[IdService] INT NOT NULL,
	[Url] nvarchar(1000),
	[Name] nvarchar(256),
	[Description] nvarchar(1000),
	CONSTRAINT [FK_VideoService] FOREIGN KEY ([IdService]) REFERENCES Services(IdRecord)
)
