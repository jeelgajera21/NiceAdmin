using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Data
{
    public class CustomerRepository
    {
        private IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<CustomerModel> GetAllCustomer()
        {
            var Customers = new List<CustomerModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Customer_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customers.Add(new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        HomeAddress = Convert.ToString(reader["HomeAddress"]),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        GST_NO = reader["GST_NO"].ToString(),
                        CityName = Convert.ToString(reader["CityName"]),
                        PinCode = reader["PinCode"].ToString(),
                        NetAmount = Convert.ToDouble(reader["NetAmount"]),
                        UserID = Convert.ToInt16(reader["UserID"]),
                        UserName  = reader["UserName"].ToString()
                    });
                }

                return Customers;
            }

        }

        public bool AddCustomer(CustomerModel customers)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customers.CustomerName;
            cmd.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customers.HomeAddress;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = customers.Email;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customers.MobileNo;
            cmd.Parameters.Add("@GST_NO", SqlDbType.VarChar).Value = customers.GST_NO;
            cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customers.CityName;
            cmd.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customers.PinCode;
            cmd.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customers.NetAmount;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = customers.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditCustomer(CustomerModel customers)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Customers_UpdateByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customers.CustomerID;

            cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customers.CustomerName;
            cmd.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customers.HomeAddress;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = customers.Email;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customers.MobileNo;
            cmd.Parameters.Add("@GST_NO", SqlDbType.VarChar).Value = customers.GST_NO;
            cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customers.CityName;
            cmd.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customers.PinCode;
            cmd.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customers.NetAmount;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = customers.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool DeleteCustomer(int CustomerID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Customer_DeleteByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CustomerID", CustomerID);

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
        public List<CustomerModel> GetCustomerByID(int CustomerID)
        {
            CustomerModel City = new CustomerModel();
            var customer = new List<CustomerModel>();
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectByPK";
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                customer.Add(new CustomerModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    HomeAddress = Convert.ToString(reader["HomeAddress"]),
                    Email = reader["Email"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    GST_NO = reader["GST_NO"].ToString(),
                    CityName = Convert.ToString(reader["CityName"]),
                    PinCode = reader["PinCode"].ToString(),
                    NetAmount = Convert.ToDouble(reader["NetAmount"]),
                    UserID = Convert.ToInt16(reader["UserID"])
                   
                });
            }

            return customer;
        }
        #endregion

    }
}
