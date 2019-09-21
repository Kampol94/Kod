CREATE TABLE [dbo].[Address] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]      INT           NOT NULL,
    [StreetAddress] NVARCHAR (50) NOT NULL,
    [City]          NVARCHAR (50) NOT NULL,
    [ZipCode]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

