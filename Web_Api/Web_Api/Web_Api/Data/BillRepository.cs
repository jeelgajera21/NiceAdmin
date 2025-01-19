using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;

namespace Web_Api.Data
{
    public class BillRepository
    {
        private IConfiguration _configuration;

        public BillRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<BillsModel> GetAllBills()
        {
            var Bills= new List<BillsModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bills_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bills.Add(new BillsModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillNumber = reader["BillNumber"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserId = Convert.ToInt16(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }

                return Bills;
            }

        }

        public bool AddBill(BillsModel billsModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Bills_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            /*cmd.Parameters.AddWithValue("BillID", billsModel.BillID);*/
            cmd.Parameters.AddWithValue("BillNumber", billsModel.BillNumber);
            cmd.Parameters.AddWithValue("BillDate", billsModel.BillDate);
            cmd.Parameters.AddWithValue("OrderID", billsModel.OrderID);
            cmd.Parameters.AddWithValue("TotalAmount", billsModel.TotalAmount);
            cmd.Parameters.AddWithValue("Discount", billsModel.Discount);
            cmd.Parameters.AddWithValue("NetAmount", billsModel.NetAmount);
            cmd.Parameters.AddWithValue("UserID", billsModel.UserId);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditBill(BillsModel billsModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Bills_UpdateByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("BillID", billsModel.BillID);
            cmd.Parameters.AddWithValue("BillNumber", billsModel.BillNumber);
            cmd.Parameters.AddWithValue("BillDate", billsModel.BillDate);
            cmd.Parameters.AddWithValue("OrderID", billsModel.OrderID);
            cmd.Parameters.AddWithValue("TotalAmount", billsModel.TotalAmount);
            cmd.Parameters.AddWithValue("Discount", billsModel.Discount);
            cmd.Parameters.AddWithValue("NetAmount", billsModel.NetAmount);
            cmd.Parameters.AddWithValue("UserName", billsModel.UserName);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool DeleteBill(int BillID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Bill_DeleteByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("BillID", BillID);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #region GetByID
        public List<BillsModel> GetBillByID(int BillID)
        {
            BillsModel Bill = new BillsModel();
            var bill = new List<BillsModel>();
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectByPK";
            command.Parameters.AddWithValue("@BillID", BillID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                bill.Add(new BillsModel
                {

                    BillID = Convert.ToInt32(reader["BillID"]),
                    BillNumber = reader["BillNumber"].ToString(),
                    BillDate = Convert.ToDateTime(reader["BillDate"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Discount = Convert.ToDecimal(reader["Discount"]),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    UserId = Convert.ToInt32(reader["UserID"])

                });
            }

            return bill;
        }
        #endregion

    }
}
