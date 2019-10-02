CREATE FUNCTION [dbo].[GetLastPos](@Parent INT)
RETURNS INT AS
BEGIN
    DECLARE @Position INT;
	SET @Position = (SELECT MAX(Pos) FROM [dbo].[TreeElements] WHERE Parent_Id = @Parent);
	RETURN @Position;
END
