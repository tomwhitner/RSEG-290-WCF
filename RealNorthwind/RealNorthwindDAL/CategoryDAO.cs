using System.Configuration;
using System.Data.SqlClient;
using MyWCFServices.RealNorthwindEntities;
using RealNorthwindDAL.Properties;

namespace MyWCFServices.RealNorthwindDAL
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
                var cmd = new SqlCommand(string.Format(Resources.DB_SQL_CAL_SEL,
                                                       Resources.DB_COL_CAT_NAME + ", " +
                                                       Resources.DB_COL_CAT_DESC,
                                                       Resources.DB_TAB_CAT,
                                                       Resources.DB_COL_CAT_ID,
                                                       Resources.DB_COL_CAT_ID), conn);

                cmd.Parameters.AddWithValue("@" + Resources.DB_COL_CAT_ID, id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    c = new CategoryEntity
                            {
                                CategoryID = id,
                                CategoryName = (string) reader[Resources.DB_COL_CAT_NAME],
                                Description = (string) reader[Resources.DB_COL_CAT_DESC]
                            };
                }
            }
            return c;
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    string.Format(Resources.DB_SQL_CAL_UPD, Resources.DB_TAB_CAT, Resources.DB_COL_CAT_NAME,
                                  Resources.DB_COL_CAT_NAME,
                                  Resources.DB_COL_CAT_DESC, Resources.DB_COL_CAT_DESC, Resources.DB_COL_CAT_ID,
                                  Resources.DB_COL_CAT_ID), conn);

                cmd.Parameters.AddWithValue("@" + Resources.DB_COL_CAT_NAME, category.CategoryName);
                cmd.Parameters.AddWithValue("@" + Resources.DB_COL_CAT_DESC, category.Description);
                cmd.Parameters.AddWithValue("@" + Resources.DB_COL_CAT_ID, category.CategoryID);
                conn.Open();
                int numRows = cmd.ExecuteNonQuery();

                return (numRows == 1);
            }
        }
    }
}