CREATE TABLE [dbo].[DivertismentTypes] (
    [Id]   INT     IDENTITY(1,1)      NOT NULL,
    [DivertismentType] NVARCHAR (50) NULL,
    CONSTRAINT [PK_DivertismentType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

