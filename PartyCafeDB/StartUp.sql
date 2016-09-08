-- Только при первичном деплое!!!

SET IDENTITY_INSERT dbo.Photos ON;  
INSERT INTO Photos (IdRecord,	FileName,	Path,	UserCreate,	UserUpdate,	DateCreate,	DateUpdate) 
VALUES (0,	'NoFoto.jpg',	'NoPhoto.jpg',	'admin',	NULL,	'2016-03-12 00:00:00.000',	NULL)
SET IDENTITY_INSERT dbo.Photos OFF;  

SET IDENTITY_INSERT dbo.Users ON;  
INSERT INTO Users (IdRecord,	UserName,	Description,	PasswordHash,	UserCreate,	UserUpdate,	DateCreate,	DateUpdate,	UserId)
VALUES (0,	'admin',	NULL,	'ABpXRA/Vj6LqarZTa+CJAxq7buZT/96sWOD/9l26IoLN1jGnUUouYnEAGED/a3rTGw==',	NULL,	NULL,	'2016-07-27 18:00:52.040',	NULL,	'AD9662E0-520B-47E9-A417-19D65C27A411');
SET IDENTITY_INSERT dbo.Users OFF;  
