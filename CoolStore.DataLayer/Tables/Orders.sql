CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Status] NVARCHAR(50) NOT NULL, 
    [CreateDate] DATE NOT NULL, 
    [UpdateDate] DATE NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Orders_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
