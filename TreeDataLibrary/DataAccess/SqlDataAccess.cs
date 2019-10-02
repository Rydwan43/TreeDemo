using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TreeDataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "TreeDatabase")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                return dbConnection.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                return dbConnection.Execute(sql, data);
            }
        }

        public static void ExecuteData(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                dbConnection.Execute(sql);
            }
        }
    }
}
