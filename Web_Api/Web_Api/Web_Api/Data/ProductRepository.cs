using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Model;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Data
{
    public class ProductRepository
    {
        private IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public List<ProductModel> GetAllProducts()
        {
            var Products = new List<ProductModel>();
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Product_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductCode = reader["ProductCode"].ToString(),
                        ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                        Description = reader["Description"].ToString(),
                        UserName = reader["UserName"].ToString()
                    });
                }

                return Products;
            }

        }

        public bool AddProduct(ProductModel Products)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Product_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Products.ProductName;
            cmd.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = Products.ProductCode;
            cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = Products.ProductPrice;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Products.Description;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Products.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditProduct(ProductModel Products)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Product_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = Products.ProductID;

            cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Products.ProductName;
            cmd.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = Products.ProductCode;
            cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = Products.ProductPrice;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Products.Description;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Products.UserID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteProduct(int ProductID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Product_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ProductID", ProductID);

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
