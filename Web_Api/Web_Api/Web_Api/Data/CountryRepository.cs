using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;


namespace Web_Api.Data
{
    public class CountryRepository
    {
        private IConfiguration _configuration;

        public CountryRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<CountryModel> GetAllCountries()
        {
            var Countries = new List<CountryModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Countries.Add(new CountryModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                    });
                }

                return Countries;
            }

        }

        public bool AddCountry(CountryModel countryModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CountryName", countryModel.CountryName);
            cmd.Parameters.AddWithValue("CountryCode", countryModel.CountryCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditCountry(CountryModel countryModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CountryID", countryModel.CountryID);
            cmd.Parameters.AddWithValue("CountryName", countryModel.CountryName);
            cmd.Parameters.AddWithValue("CountryCode", countryModel.CountryCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteCountry(int CountryID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CountryID", CountryID);

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
        public List<CountryModel> GetCountryByID(int CountryID)
        {
            CountryModel Country = new CountryModel();
            var country = new List<CountryModel>();
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_SelectByPK";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                country.Add(new CountryModel
                {

                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString(),
                    CountryCode = reader["CountryCode"].ToString(),

                });
            }

            return country;
        }
        #endregion


    }
}
    