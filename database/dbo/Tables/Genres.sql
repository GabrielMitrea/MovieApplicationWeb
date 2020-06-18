CREATE TABLE [dbo].[Genres]
(
	[Id] INT  IDENTITY (1,1) NOT NULL, 
    [Genre] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id]) 
)
