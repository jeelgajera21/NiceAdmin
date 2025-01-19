using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using Web_Api.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Data
{
    public class OrderDetailRepository
    {
        private IConfiguration _configuration;

        public OrderDetailRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }
            
        public List<OrderDetailModel> GetAllOrderDetails()
        {
            var OrderDetails = new List<OrderDetailModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_OrderDetail_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderDetails.Add(new OrderDetailModel
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Amount = Convert.ToDouble(reader["Amount"]),
                        TotalAmount = Convert.ToDouble(reader["TotalAmount"]),
                        UserName = reader["UserName"].ToString()


                        
                    });
                }

                return OrderDetails;
            }

        }

        public bool AddOrderDetail(OrderDetailModel OrderDetails)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_OrderDetail_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            


            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderDetails.OrderID;
        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = OrderDetails.ProductID;
        cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = OrderDetails.Quantity;
        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = OrderDetails.Amount;
        cmd.Parameters.Add("@TotalAmount", SqlDbType.VarChar).Value = OrderDetails.TotalAmount;
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = OrderDetails.UserID;

        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditOrderDetail(OrderDetailModel OrderDetails)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_OrderDetail_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = OrderDetails.OrderDetailID;

            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderDetails.OrderID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = OrderDetails.ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = OrderDetails.Quantity;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = OrderDetails.Amount;
            cmd.Parameters.Add("@TotalAmount", SqlDbType.VarChar).Value = OrderDetails.TotalAmount;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = OrderDetails.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteOrderDetail(int OrderDetailID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_OrderDetail_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("OrderDetailID", OrderDetailID);

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
