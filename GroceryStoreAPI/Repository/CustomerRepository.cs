using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class CustomerRepository
    {

        #region Configuration
        private readonly string connectionstring;

        public CustomerRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion

        #region SelectAll

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
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return customer;
        }
        #endregion


        #region GetbyID

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
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return customer;
        }
        #endregion

        #region delete

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
        #endregion

        #region Insert
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
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion
        #region CustomerRegister
        public bool CustomerRegister(CustomerRegisterModel customer)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_Register";
            command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@Password", customer.Password);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
            command.Parameters.AddWithValue("@PinCode", customer.PinCode);
            command.Parameters.AddWithValue("@Address", customer.Address);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
            #endregion

            #region update

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
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion

        public CustomerModel CustomerLogin(CustomerLoginModel customer)
        {
            CustomerModel customerData = null;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_CUSTOMER_Login", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("Email", customer.Email);
                cmd.Parameters.AddWithValue("Password", customer.Password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customerData = new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = Convert.ToString(reader["CustomerName"]),
                        Email = Convert.ToString(reader["Email"]),
                        Password = Convert.ToString(reader["Password"]),
                        City = Convert.ToString(reader["City"]),
                        MobileNo = Convert.ToString(reader["MobileNo"]),
                        Address = Convert.ToString(reader["Address"]),
                        PinCode = Convert.ToString(reader["Pincode"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return customerData;

        }
        public CustomerModel GetCustomerProfile(int CustomerID)
        {
            CustomerModel customer = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_PROFILE";
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
                    
                };
            }
            return customer;
        }

    }
}
