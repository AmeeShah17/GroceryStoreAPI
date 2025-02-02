using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
   
    public class CategoryRepository
    {
        #region Configuration
        private readonly string connectionstring;

        public CategoryRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion


        #region Select all
        public IEnumerable<CategoryModel> SelectAll() 
        {
            var category = new List<CategoryModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_SELECTALL";
            SqlDataReader reader= command.ExecuteReader();
            while (reader.Read()) 
            {
                category.Add(new CategoryModel
                {
                    CateoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = Convert.ToString(reader["CategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return category;
        }
        #endregion


        #region SelectbyID

        public CategoryModel GetbyID(int CategoryID)
        {
            CategoryModel category = null;
            SqlConnection connection = new SqlConnection( connectionstring);
            connection.Open();
            SqlCommand command=connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_SELECTBYPK";
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            SqlDataReader reader= command.ExecuteReader();
            while (reader.Read())
            {
                category = new CategoryModel
                {
                    CateoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = Convert.ToString(reader["CategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return category;
        }
        #endregion


        #region Delete
        public bool CategoryDelete(int CategoryID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command=connection.CreateCommand();
            command.CommandType= CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_DELETE";
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected>0;
        }
        #endregion


        #region Insert

        public bool CategoryInsert(CategoryModel category)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command=connection.CreateCommand();
            command.CommandType=CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_INSERT";
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            command.Parameters.AddWithValue("@Created", DateTime.Now);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion


        #region Update

        public bool CategoryUpdate(CategoryModel category)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_UPDATE";
            command.Parameters.AddWithValue("@CategoryID", category.CateoryID);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;

            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion
    }
}
