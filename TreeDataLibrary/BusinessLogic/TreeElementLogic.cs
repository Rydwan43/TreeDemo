using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeDataLibrary.DataAccess;
using TreeDataLibrary.Models;

namespace TreeDataLibrary.BusinessLogic
{
    public static class TreeElementLogic
    {
        public static void CreateTreeElement(string name, int parentId)
        {
            string sql = @"exec dbo.AddBranch " + name + ", " + parentId.ToString() + ";";


            SqlDataAccess.ExecuteData(sql);
        }

        public static void DeleteTreeElement(int id)
        {
            string sql = "exec dbo.DeleteTree " + id.ToString();

            SqlDataAccess.ExecuteData(sql);
        }

        public static void ChangeName(string name, int id)
        {
            string sql = "update dbo.TreeElements set Name = '"+name+"' WHERE Id = "+id.ToString()+";";

            SqlDataAccess.ExecuteData(sql);
        }

        public static List<TreeElementModel> LoadTree(int parent_id = 0)
        {
            string sql = @"select Id, Name, Parent_Id, Pos from dbo.TreeElements where Parent_Id = " + parent_id.ToString() + " ORDER BY Pos";
            return SqlDataAccess.LoadData<TreeElementModel>(sql);
        }
        
        public static void ChangeParent(int id, int parent_id)
        {
            string sql = "update dbo.TreeElements set Parent_Id = " + parent_id.ToString() + " WHERE Id = " + id;
        }

        public static void SetLastPosition(int Id)
        {
            string sql = "exec dbo.SetPositionToEnd " + Id.ToString() + " ;";
            SqlDataAccess.ExecuteData(sql);
        }

        public static void ChangePosition(int Id ,int Pos)
        {
            string sql = "exec ChangeOrder "+Id.ToString() + ","+ Pos.ToString() + ";";
            SqlDataAccess.ExecuteData(sql);
        }

        public static List<TreeElementModel> LoadParents(int child_id = 0)
        {
            string sql = @"select * from dbo.GetParents("+ child_id.ToString() +");";
            return SqlDataAccess.LoadData<TreeElementModel>(sql);
        }
    }
}
