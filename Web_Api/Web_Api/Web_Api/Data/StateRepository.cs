using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;

namespace Web_Api.Data
{
    public class StateRepository
    {
        private IConfiguration _configuration;

        public StateRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<StateModel> GetAllState()
        {
            var States = new List<StateModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    States.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                    });
                }

                return States;
            }

        }

        public bool AddState(StateModel StateModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_State_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CountryName", StateModel.CountryName);
            cmd.Parameters.AddWithValue("StateName", StateModel.StateName);
            cmd.Parameters.AddWithValue("StateCode", StateModel.StateCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditState(StateModel StateModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_State_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("StateID", StateModel.StateID);
            cmd.Parameters.AddWithValue("StateName", StateModel.StateName);
            cmd.Parameters.AddWithValue("StateCode", StateModel.StateCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteState(int StateID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_State_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("StateID", StateID);

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
        public List<StateModel> GetStateByID(int StateID)
        {
            StateModel State = new StateModel();
            var state = new List<StateModel>();
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectByPK";
            command.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                state.Add(new StateModel
                {

                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = reader["StateName"].ToString(),
                    StateCode = reader["StateCode"].ToString(),
                    CountryID = Convert.ToInt32(reader["CountryID"]),

                });
            }

            return state;
        }
        #endregion

    }
}
