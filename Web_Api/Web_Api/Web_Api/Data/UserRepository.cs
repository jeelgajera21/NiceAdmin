
using Microsoft.Data.SqlClient;
using System.Net;
using Web_Api.Model;

namespace Web_Api.Data
{
    public class UserRepository
    {
        private IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<UserModel> GetAllUser()
        {
            var users = new List<UserModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_User_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }

                return users;
            }

        }

        public bool AddUser(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_SelectByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("Email", userModel.Email);
            cmd.Parameters.AddWithValue("Password", userModel.Password);
            cmd.Parameters.AddWithValue("MobileNo", userModel.MobileNo);
            cmd.Parameters.AddWithValue("Address", userModel.Address);
            cmd.Parameters.AddWithValue("IsActive", userModel.IsActive);


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditUser(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_User_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("Email", userModel.Email);
            cmd.Parameters.AddWithValue("Password", userModel.Password);
            cmd.Parameters.AddWithValue("MobileNo", userModel.MobileNo);
            cmd.Parameters.AddWithValue("Address", userModel.Address);
            cmd.Parameters.AddWithValue("IsActive", userModel.IsActive);


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool DeleteUser(int UserID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_DeleteByPK", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", UserID);

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
