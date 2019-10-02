CREATE PROC [dbo].[AddBranch](@Name NVARCHAR (MAX), @Parent INT)
AS BEGIN
DECLARE @Pos INT;
SELECT @Pos = COUNT(*) FROM dbo.TreeElements WHERE Parent_Id = @Parent;
INSERT INTO dbo.TreeElements (Name, Parent_Id, Pos)
                            values (@Name, @Parent, @Pos);
END