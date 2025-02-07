using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class SubCategoryRepository
    {
        #region Configuration
        private readonly string connectionstring;

        public SubCategoryRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion

        #region Selectall

        public IEnumerable<SubCategoryModel> SelectAllSubCategory()
        {
            var subcategory = new List<SubCategoryModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subcategory.Add(new SubCategoryModel
                {
                    SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                    SubCategoryName = Convert.ToString(reader["SubCategoryName"]),
                    SubCategoryImage = Convert.ToString(reader["SubCategoryImage"]),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = Convert.ToString(reader["CategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"]),
                });
            }
            return subcategory;
        }
        #endregion

        #region GetbyID
        public SubCategoryModel GetbyID(int SubCategoryID)
        {
            SubCategoryModel subcategory = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_SELECTBYPK";
            command.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subcategory = new SubCategoryModel
                {
                    SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                    SubCategoryName = Convert.ToString(reader["SubCategoryName"]),
                    SubCategoryImage = Convert.ToString(reader["SubCategoryImage"]),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName=Convert.ToString(reader["CategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return subcategory;
        }
        #endregion

        #region Delete

        public bool SubCategoryDelete(int SubCategoryID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_DELETE";
            command.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }
        #endregion

        #region Insert
        public bool SubCategoryInsert(SubCategoryModel subcategory)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_INSERT";
            command.Parameters.AddWithValue("@CategoryID", subcategory.CategoryID);
            command.Parameters.AddWithValue("@SubCategoryName", subcategory.SubCategoryName);
            command.Parameters.AddWithValue("@SubCategoryImage", subcategory.SubCategoryImage);
            command.Parameters.AddWithValue("@Created", DateTime.Now);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion

        #region Update

        public bool SubCategoryUpdate(SubCategoryModel subcategory)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_UPDATE";
            command.Parameters.AddWithValue("@SubCategoryID", subcategory.SubCategoryID);
            command.Parameters.AddWithValue("@SubCategoryName", subcategory.SubCategoryName);
            command.Parameters.AddWithValue("@SubCategoryImage", subcategory.SubCategoryImage);
            command.Parameters.AddWithValue("@CategoryID", subcategory.CategoryID);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;

            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion


        #region CategoryDropDown
        public IEnumerable<CategoryDropDownModel> CategoryDropDown()
        {
            var category = new List<CategoryDropDownModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CATEGORY_DROPDOWN";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                category.Add(new CategoryDropDownModel
                {
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = Convert.ToString(reader["CategoryName"]),
                   
                });
            }
            return category;
        }
        #endregion
    }
}
