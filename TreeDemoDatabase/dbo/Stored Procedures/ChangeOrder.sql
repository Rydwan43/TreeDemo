CREATE PROC [dbo].[ChangeOrder] (@Id INT, @NewPosition INT)
AS
BEGIN
DECLARE @Parent INT;
SET @Parent = (SELECT Parent_Id from [dbo].[TreeElements] WHERE Id = @Id);

UPDATE [dbo].[TreeElements] SET Pos = @NewPosition WHERE Id = @Id;

DECLARE @Increment INT
SET @Increment = @NewPosition

DECLARE 
    @Element_Id INT, 
    @Element_Pos INT
 
DECLARE cursor_pos CURSOR
FOR SELECT 
        Id, 
        Pos
    FROM 
        [dbo].[TreeElements]
	WHERE Pos >= @NewPosition AND Id != @Id AND Parent_Id = @Parent
	ORDER BY Pos
 
OPEN cursor_pos
 
FETCH NEXT FROM cursor_pos INTO 
    @Element_Id, 
    @Element_Pos
 
WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE [dbo].[TreeElements] SET Pos = @Increment, @Increment = @Increment + 1
		WHERE Id = @Element_Id
        FETCH NEXT FROM cursor_pos INTO 
            @Element_Id, 
            @Element_Pos
    END
END