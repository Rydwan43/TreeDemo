CREATE TABLE [dbo].[TreeElements]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Parent_Id] INT NOT NULL, 
    [Pos] INT NOT NULL
)
