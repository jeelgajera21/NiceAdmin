using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Data
{
    public class OrderRepository
    {
        private IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<OrderModel> GetAllOrders()
        {
            var Orders = new List<OrderModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Order_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orders.Add(new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerName =reader["CustomerName"].ToString(),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        OrderNumber = Convert.ToString(reader["OrderNumber"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt16(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }

                return Orders;
            }

        }

        public bool AddOrder(OrderModel Orders)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Order_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.Add("@OrderDate ", SqlDbType.DateTime).Value = Orders.OrderDate;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Orders.CustomerID;
            cmd.Parameters.Add("@PaymentMode ", SqlDbType.VarChar).Value = Orders.PaymentMode;
            cmd.Parameters.Add("@OrderNumber ", SqlDbType.VarChar).Value = Orders.OrderNumber;
            cmd.Parameters.Add("@TotalAmount ", SqlDbType.Decimal).Value = Orders.TotalAmount;
            cmd.Parameters.Add("@ShippingAddress ", SqlDbType.VarChar).Value = Orders.ShippingAddress;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Orders.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditOrder(OrderModel Orders)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Order_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = Orders.OrderID;

            cmd.Parameters.Add("@OrderDate ", SqlDbType.DateTime).Value = Orders.OrderDate;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Orders.CustomerID;
            cmd.Parameters.Add("@PaymentMode ", SqlDbType.VarChar).Value = Orders.PaymentMode;
            cmd.Parameters.Add("@OrderNumber ", SqlDbType.VarChar).Value = Orders.OrderNumber;
            cmd.Parameters.Add("@TotalAmount ", SqlDbType.Decimal).Value = Orders.TotalAmount;
            cmd.Parameters.Add("@ShippingAddress ", SqlDbType.VarChar).Value = Orders.ShippingAddress;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Orders.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteOrder(int OrderID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Order_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("OrderID", OrderID);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
