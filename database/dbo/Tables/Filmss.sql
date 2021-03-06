﻿CREATE TABLE [dbo].[Filmss]
(
	[Id] INT  IDENTITY (1,1) NOT NULL, 
    [DivertismentTypeId] INT NOT NULL, 
    [Title] NVARCHAR(50) NULL, 
    [GenreId] INT NOT NULL, 
    [Duration] DATETIME NULL, 
    [DateReleased] DATETIME2 NULL, 
    [Director] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL,
    [UserId] INT NOT NULL, 
    [Trailer] NVARCHAR(200) NULL, 
    [ImagePath] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_Filmss] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Filmss_DivertismentTypes] FOREIGN KEY ([DivertismentTypeId]) REFERENCES [DivertismentTypes]([Id]) ,
       CONSTRAINT [FK_Filmss_Genres] FOREIGN KEY ([GenreId]) REFERENCES [Genres]([Id]) ,
          CONSTRAINT [FK_Filmss_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) 
)
