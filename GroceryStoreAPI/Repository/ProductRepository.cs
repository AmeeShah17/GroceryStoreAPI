﻿using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class ProductRepository
    {
        #region configuration
        private readonly string connectionstring;

        public ProductRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion

        #region SelectALL
        public IEnumerable<ProductModel> SelectAll()
        {
            var product = new List<ProductModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_PRODUCT_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                product.Add(new ProductModel
                {
                    ProductID=Convert.ToInt32(reader["ProductID"]),
                    ProductName = Convert.ToString(reader["ProductName"]),
                    //ProductImage = Convert.ToString(reader["ProductImage"]),
                    ProductImage = reader["ProductImage"] != DBNull.Value ? $"https://localhost:7011{reader["ProductImage"].ToString()}" : null,
                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                    ProductCode=Convert.ToString(reader["ProductCode"]),
                    Description = Convert.ToString(reader["Description"]),
                    SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                    SubCategoryName = Convert.ToString(reader["SubCategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return product;
        }
        #endregion


        #region GetbyID
        public ProductModel GetbyID(int ProductID)
        {
            ProductModel product = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_PRODUCT_SELECTBYPK";
            command.Parameters.AddWithValue("@ProductID", ProductID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                product = new ProductModel
                {
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    ProductName = Convert.ToString(reader["ProductName"]),
                    //ProductImage = Convert.ToString(reader["ProductImage"]),
                    ProductImage = reader["ProductImage"] != DBNull.Value ? $"https://localhost:7011{reader["ProductImage"].ToString()}" : null,
                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                    ProductCode = Convert.ToString(reader["ProductCode"]),
                    Description = Convert.ToString(reader["Description"]),
                    SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                    SubCategoryName=Convert.ToString(reader["SubCategoryName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return product;
        }
        #endregion

        #region Delete
        public bool ProductDelete(int ProductID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_PRODUCT_DELETE";
            command.Parameters.AddWithValue("@ProductID", ProductID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }
        #endregion


        #region Insert
        public bool ProductInsert(ProductModel product)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_PRODUCT_INSERT";
            
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                Directory.CreateDirectory(uploadsFolder); 
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);
                }

                product.ProductImage = "/ProductImages/" + uniqueFileName; 
            }



            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@ProductImage", (object)product.ProductImage ?? DBNull.Value);
            //command.Parameters.AddWithValue("@ProductImage",product.ProductImage); 
            command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@SubCategoryID", product.SubCategoryID);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion


        #region Update
        public bool ProductUpdate(ProductModel product)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_PRODUCT_UPDATE";
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);
                }

                product.ProductImage = "/ProductImages/" + uniqueFileName;
            }
            command.Parameters.AddWithValue("@ProductID", product.ProductID);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@ProductImage", product.ProductImage ?? DBNull.Value.ToString());
            //command.Parameters.AddWithValue("@ProductImage", product.ProductImage); 
            command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@SubCategoryID", product.SubCategoryID);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion


        #region SubCateoryDropDown

        public IEnumerable<SubCategoryDropDownModel> SubCategoryDropDown()
        {
            var subcategory = new List<SubCategoryDropDownModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_SUBCATEGORY_DROPDOWN";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subcategory.Add(new SubCategoryDropDownModel
                {
                    SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                   SubCategoryName = Convert.ToString(reader["SubCategoryName"]),
                    
                });
            }
            return subcategory;
        }
        #endregion

        #region getProductbySubCategory

        public List<ProductModel> GetProductBySubCategory(int SubCategoryID)
        {
            List<ProductModel> products = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_PRODUCT_GETPRODDUCTBYSUBCATEGORY";
                    command.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductModel
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = Convert.ToString(reader["ProductName"]),
                                ProductImage = reader["ProductImage"] != DBNull.Value ? $"https://localhost:7011{reader["ProductImage"].ToString()}" : null,
                                ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                ProductCode = Convert.ToString(reader["ProductCode"]),
                                Description = Convert.ToString(reader["Description"]),
                                SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                                SubCategoryName = Convert.ToString(reader["SubCategoryName"]),
                                Created = Convert.ToDateTime(reader["Created"]),
                                Modified = Convert.ToDateTime(reader["Modified"])
                            });
                        }
                    }
                }
            }

            return products;
        }

        #endregion
    }
}
