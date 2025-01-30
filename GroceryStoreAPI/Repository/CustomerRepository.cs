using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class CustomerRepository
    {
        private readonly string connectionstring;

        public CustomerRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }

        public IEnumerable<CustomerModel> SelectAll()
        {
            var customer = new List<CustomerModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer.Add(new CustomerModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    City = Convert.ToString(reader["City"]),
                    MobileNo = Convert.ToString(reader["MobileNo"]),
                    Address = Convert.ToString(reader["Address"]),
                    PinCode = Convert.ToString(reader["Pincode"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return customer;
        }

        public CustomerModel GetbyID(int CustomerID)
        {
            CustomerModel customer = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_SELECTBYPK";
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer = new CustomerModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    City = Convert.ToString(reader["City"]),
                    MobileNo = Convert.ToString(reader["MobileNo"]),
                    Address = Convert.ToString(reader["Address"]),
                    PinCode = Convert.ToString(reader["Pincode"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return customer;
        }

        public bool CustomerDelete(int CustomerID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_DELETE";
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }

        public bool CustomerInsert(CustomerModel customer)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_INSERT";
            command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@Password", customer.Password);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
            command.Parameters.AddWithValue("@PinCode", customer.PinCode);
            command.Parameters.AddWithValue("@IsActive", customer.IsActive);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }

        public bool CustomerUpdate(CustomerModel customer)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_UPDATE";
            command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
            command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@Password", customer.Password);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
            command.Parameters.AddWithValue("@PinCode", customer.PinCode);
            command.Parameters.AddWithValue("@IsActive", customer.IsActive);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
    }
}
