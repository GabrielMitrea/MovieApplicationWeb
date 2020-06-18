CREATE TABLE [dbo].[FavSerials] (
    [Id]                 INT     IDENTITY(1,1)      NOT NULL,
    [DivertismentTypeId] INT           NULL,
    [Title]              NVARCHAR (50) NULL,
    [GenreId]              INT NULL,
    [NumberOfSeasons]    INT           NULL,
    [NumberOfEpisodes]   INT           NULL,
    [DateReleased]       DATETIME2      NULL,
    [Director]           NVARCHAR(50) NULL,
    [Description]     NVARCHAR(MAX) NULL,
    [UserId]  INT NOT NULL,
    [Trailer] NVARCHAR(200) NULL, 
    [ImagePath] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_FavSerials] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FavSerials_DivertismentTypes] FOREIGN KEY ([DivertismentTypeId]) REFERENCES [dbo].[DivertismentTypes] ([Id]),
    CONSTRAINT [FK_FavSerials_Genres] FOREIGN KEY ([GenreId]) REFERENCES [Genres]([Id]),
        CONSTRAINT [FK_FavSerials_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
 
);


