CREATE TABLE [dbo].[ComingSoon]
(
	[Id] INT  IDENTITY (1,1) NOT NULL, 
    [DivertismentTypeId] INT NOT NULL, 
    [Title] NVARCHAR(50) NULL, 
    [GenreId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [Description] NVARCHAR (MAX) NULL,
    [Trailer] NVARCHAR(200) NULL, 
    [ImagePath] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_ComingSoon] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ComingSoon_DivertismentTypes] FOREIGN KEY ([DivertismentTypeId]) REFERENCES [DivertismentTypes]([Id]) ,
       CONSTRAINT [FK_ComingSoon_Genres] FOREIGN KEY ([GenreId]) REFERENCES [Genres]([Id]) ,
          CONSTRAINT [FK_ComingSoon_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) 
)
