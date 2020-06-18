CREATE TABLE [dbo].[Films] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [DivertismentTypeId] INT            NOT NULL,
    [Title]              NVARCHAR (30)  NULL,
    [GenreId]            INT            NULL,
    [Duration]           DATETIME       NULL,
    [DateReleased]       DATETIME2 (7)  NULL,
    [Director]           NVARCHAR (100) NULL,
    [Trailer]            NVARCHAR (200) NULL,
    [ImagePath]          NVARCHAR (100) NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Films_DivertismentTypes] FOREIGN KEY ([DivertismentTypeId]) REFERENCES [dbo].[DivertismentTypes] ([Id]),
    CONSTRAINT [FK_Films_Genres] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genres] ([Id])
);

