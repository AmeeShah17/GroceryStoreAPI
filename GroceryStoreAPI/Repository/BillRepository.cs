using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class BillRepository
    {

        #region Configration
        private readonly string connectionstring;

        public BillRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion


        #region Select ALL
        public IEnumerable<BillModel> SelectAll()
        {
            var bill = new List<BillModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BILL_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bill.Add(new BillModel
                {
                    BillID=Convert.ToInt32(reader["BillID"]),
                    BillNumber=Convert.ToString(reader["BillNumber"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    CustomerID=Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return bill;
        }
        #endregion

        #region GetbyID

        public BillModel GetbyID(int BillID)
        {
            BillModel bill = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BILL_SELECTBYPK";
            command.Parameters.AddWithValue("@BillID", BillID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bill = new BillModel
                {
                    BillID = Convert.ToInt32(reader["BillID"]),
                    BillNumber = Convert.ToString(reader["BillNumber"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return bill;
        }
        #endregion

        #region Delete

        public bool BillDelete(int BillID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BILL_DELETE";
            command.Parameters.AddWithValue("@BillID", BillID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }
        #endregion

        #region Insert
        public bool BillInsert(BillModel bill)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BILL_INSERT";
            command.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
            command.Parameters.AddWithValue("@OrderID", bill.OrderID);
            command.Parameters.AddWithValue("@customerID", bill.CustomerID);
            command.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
            command.Parameters.AddWithValue("@Discount", bill.Discount);
            command.Parameters.AddWithValue("@NetAmount", bill.NetAmount);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion

        #region Update
        public bool BillUpdate(BillModel bill)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BILL_UPDATE";
            command.Parameters.AddWithValue("@BillID", bill.BillID);
            command.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
            command.Parameters.AddWithValue("@OrderID", bill.OrderID);
            command.Parameters.AddWithValue("@CustomerID", bill.CustomerID);
            command.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
            command.Parameters.AddWithValue("@Discount", bill.Discount);
            command.Parameters.AddWithValue("@NetAmount", bill.NetAmount);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion


        #region OrderDropDown
        public IEnumerable<OrderDropDownModel> OrderDropDown()
        {
            var order = new List<OrderDropDownModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_DROPDOWN";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.Add(new OrderDropDownModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderInfo = Convert.ToString(reader["OrderInfo"]),
                    
                });
            }
            return order;
        }
        #endregion


        #region CustomerDropDown
        public IEnumerable<CustomerDropDownModel> CustomerDropDown()
        {
            var customer = new List<CustomerDropDownModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CUSTOMER_DROPDOWN";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer.Add(new CustomerDropDownModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),

                });
            }
            return customer;
        }
        #endregion
    }
}

