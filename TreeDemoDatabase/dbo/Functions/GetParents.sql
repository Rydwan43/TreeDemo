CREATE FUNCTION [dbo].[GetParents] (@child INT)
RETURNS TABLE
AS
RETURN
WITH ParentsTree AS (
   SELECT Id, Parent_Id, Name
   FROM dbo.TreeElements
   WHERE Id = @child
   UNION all
   SELECT c.Id, c.Parent_Id, c.Name
   FROM dbo.TreeElements c
     JOIN ParentsTree p ON p.Parent_Id = c.Id
)
SELECT * FROM ParentsTree;