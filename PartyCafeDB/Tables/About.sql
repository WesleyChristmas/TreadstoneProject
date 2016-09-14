﻿CREATE TABLE [dbo].[About]
(
	[IdRecord] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[IdPhoto] INT NOT NULL,
	FOREIGN KEY ([IdPhoto]) REFERENCES Photos(IdRecord)
)
