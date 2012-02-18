using System.Configuration;
using System.Data.SqlClient;
using MyWCFServices.RealNorthwindEntities;

namespace RealNorthwindDAL
{
    public class CategoryDAO
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;

        public CategoryEntity GetCategory(int id)
        {
            CategoryEntity c = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand { CommandText = "select CategoryName, Description from Categories where CategoryID=@id", Connection = conn };
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    c = new CategoryEntity
                    {
                        CategoryID = id,
                        Name = (string)reader["CategoryName"],
                        Description = (string)reader["Description"]
                    };
                }
            }
            return c;
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd =
                    new SqlCommand(
                        "UPDATE Categories SET CategoryName=@name, Description=@description WHERE CategoryID=@id", conn);
                cmd.Parameters.AddWithValue("@name", category.Name);
                cmd.Parameters.AddWithValue("@description", category.Description);
                cmd.Parameters.AddWithValue("@id", category.CategoryID);
                conn.Open();
                int numRows = cmd.ExecuteNonQuery();

                return (numRows == 1);
            }
        }
    }
}