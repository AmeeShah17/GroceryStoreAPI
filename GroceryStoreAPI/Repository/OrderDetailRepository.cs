using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class OrderDetailRepository
    {
        private readonly string connectionstring;

        public OrderDetailRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }

        public IEnumerable<OrderDetailModel> SelectAll()
        {
            var orderdetail = new List<OrderDetailModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDERDETAIL_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderdetail.Add(new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return orderdetail;
        }

        public OrderDetailModel GetbyID(int OrderDetailID)
        {
            OrderDetailModel orderdetail = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDERDETAIL_SELECTBYPK";
            command.Parameters.AddWithValue("@OderDetailID", OrderDetailID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderdetail = new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return orderdetail;
        }

        public bool OrderDetailDelete(int OrderDetailID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDERDETAIL_DELETE";
            command.Parameters.AddWithValue("@OrderDetailID", OrderDetailID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }

        public bool OrderDetailInsert(OrderDetailModel orderDetail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDERDETAIL_INSERT";
            command.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
            command.Parameters.AddWithValue("@CustomerID", orderDetail.CustomerID);
            command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
            command.Parameters.AddWithValue("@Amount", orderDetail.Amount);
            command.Parameters.AddWithValue("@TotalAmount", orderDetail.TotalAmount);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }

        public bool OrderdetailUpdate(OrderDetailModel orderDetail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDERDETAIL_UPDATE";
            command.Parameters.AddWithValue("@OrderDetailID", orderDetail.OrderDetailID);
            command.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
            command.Parameters.AddWithValue("@CustomerID", orderDetail.CustomerID);
            command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
            command.Parameters.AddWithValue("@Amount", orderDetail.Amount);
            command.Parameters.AddWithValue("@TotalAmount", orderDetail.TotalAmount);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
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
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),

                });
            }
            return order;
        }
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
                    CustomerName = Convert.ToString(reader["CustomerName"])

                });
            }
            return customer;
        }
    }
}
