using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using Web_Api.Model;

namespace Web_Api.Data
{
    public class CityRepository
    {
        private IConfiguration _configuration;

        public CityRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<CityModel> GetAllCities()
        {
            var Cities = new List<CityModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cities.Add(new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                    });
                }

                return Cities;
            }

        }

        public bool AddCites(CityModel cityModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("StateID", cityModel.StateID);
            cmd.Parameters.AddWithValue("CountryID", cityModel.CountryID);
            cmd.Parameters.AddWithValue("CityName", cityModel.CityName);
            cmd.Parameters.AddWithValue("CityCode", cityModel.CityCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditCites(CityModel cityModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("cityID", cityModel.CityID);
            cmd.Parameters.AddWithValue("StateID", cityModel.StateID);
            cmd.Parameters.AddWithValue("CountryID", cityModel.CountryID);
            cmd.Parameters.AddWithValue("CountryName", cityModel.CityName);
            cmd.Parameters.AddWithValue("CountryCode", cityModel.CityCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool DeleteCites(int CityID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CityID", CityID);

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
        public List<CityModel> GetCityByID(int CityID)
        {
            CityModel City = new CityModel();
            var city = new List<CityModel>();
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectByPK";
            command.Parameters.AddWithValue("@CityID", CityID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                city.Add(new CityModel
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CityName = reader["CityName"].ToString(),
                    CityCode = reader["CityCode"].ToString(),


                });
            }

            return city;
        }
        #endregion

    }
}
