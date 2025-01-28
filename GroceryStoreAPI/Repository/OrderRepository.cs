using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class OrderRepository
    {
        private readonly string connectionstring;

        public OrderRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }

        public IEnumerable<OrderModel> SelectAll()
        {
            var order = new List<OrderModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.Add(new OrderModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate=Convert.ToDateTime(reader["OrderDate"]),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName=Convert.ToString(reader["CustomerName"]),
                    Discount=Convert.ToInt32(reader["Discount"]),
                    TotalAmount=Convert.ToDecimal(reader["TotalAmount"]),
                    ShippingAddress=Convert.ToString(reader["ShippingAddress"]),
                    PaymentMode = Convert.ToString(reader["PaymentMode"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return order;
        }

        public OrderModel GetbyID(int OrderID)
        {
            OrderModel order = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_SELECTBYPK";
            command.Parameters.AddWithValue("@OrderID", OrderID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order = new OrderModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = Convert.ToString(reader["CustomerName"]),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    ShippingAddress = Convert.ToString(reader["ShippingAddress"]),
                    PaymentMode = Convert.ToString(reader["PaymentMode"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return order;
        }

        public bool OrderDelete(int OrderID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_DELETE";
            command.Parameters.AddWithValue("@OrderID", OrderID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }

        public bool OrderInsert(OrderModel order)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_INSERT";
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            command.Parameters.AddWithValue("@Discount", order.Discount);
            command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            command.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }

        public bool OrderUpdate(OrderModel order)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_ORDER_UPDATE";
            command.Parameters.AddWithValue("@OrderID", order.OrderID);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            command.Parameters.AddWithValue("@Discount", order.Discount);
            command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            command.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
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
                    CustomerName = Convert.ToString(reader["CustomerName"]),

                });
            }
            return customer;
        }
    }
}
