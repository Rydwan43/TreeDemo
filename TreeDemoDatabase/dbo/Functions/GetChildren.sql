CREATE FUNCTION [dbo].[GetChildren] (@Parent INT)
RETURNS TABLE
AS
RETURN
WITH ChildrenTree AS (
   SELECT Id, Parent_Id, Name
   FROM dbo.TreeElements
   WHERE Parent_Id = @Parent
   UNION ALL
   SELECT c.Id, c.Parent_Id, c.Name
   FROM [dbo].[TreeElements] c
     JOIN ChildrenTree p ON p.Id = c.Parent_Id
)
SELECT * FROM ChildrenTree;