CREATE TABLE [dbo].[Users] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Username]        NVARCHAR (50) NULL,
    [EmailAddress]    NVARCHAR (50) NULL,
    [ConfirmEmail]    NVARCHAR (50) NULL,
    [Password]        NVARCHAR (50) NULL,
    [ConfirmPassword] NVARCHAR (50) NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

