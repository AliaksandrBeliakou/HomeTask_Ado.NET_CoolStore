CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] TEXT NULL, 
    [Weight] INT NOT NULL, 
    [Height] INT NOT NULL, 
    [Width] INT NOT NULL, 
    [Length] INT NOT NULL
)
