using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class CategoryRepository
    {
        private readonly string connectionstring;

        public CategoryRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }

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
                    CategoryImage = Convert.ToString(reader["CategoryImage"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return category;
        }

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
                    CategoryImage = Convert.ToString(reader["CategoryImage"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return category;
        }

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

        public bool CategoryInsert(CategoryModel category)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command=connection.CreateCommand();
            command.CommandType=CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_INSERT";
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            command.Parameters.AddWithValue("@CategoryImage", category.CategoryImage);
            command.Parameters.AddWithValue("@Created", DateTime.Now);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }

        public bool CategoryUpdate(CategoryModel category)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_UPDATE";
            command.Parameters.AddWithValue("@CategoryID", category.CateoryID);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            command.Parameters.AddWithValue("@CategoryImage", category.CategoryImage);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;

            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
    }
}
