CREATE PROC [dbo].[DeleteTree](@Id INT)
AS
BEGIN
WITH child_tree AS (
   SELECT Id, Parent_Id, Name
   FROM dbo.TreeElements
   WHERE Parent_Id = @Id
   UNION ALL
   SELECT c.Id, c.Parent_Id, c.Name
   FROM [dbo].[TreeElements] c
     JOIN child_tree p ON p.Id = c.Parent_Id
)
DELETE [dbo].[TreeElements] FROM [dbo].[TreeElements] INNER JOIN child_tree ON [dbo].[TreeElements].Id = child_tree.Id;
DELETE FROM [dbo].[TreeElements] WHERE Id = @Id;
END;