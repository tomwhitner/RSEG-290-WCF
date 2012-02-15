using System.Configuration;
using System.Data.SqlClient;
using MyWCFServices.RealNorthwindEntities;

namespace MyWCFServices.RealNorthwindDAL
{
    public class ProductDAO
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;

        public ProductEntity GetProduct(int id)
        {
            ProductEntity p = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var comm = new SqlCommand
                               {CommandText = "select * from Products where ProductID=" + id, Connection = conn};
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    p = new ProductEntity
                            {
                                ProductID = id,
                                ProductName = (string) reader["ProductName"],
                                QuantityPerUnit = (string) reader["QuantityPerUnit"],
                                UnitPrice = (decimal) reader["UnitPrice"],
                                UnitsInStock = (short) reader["UnitsInStock"],
                                UnitsOnOrder = (short) reader["UnitsOnOrder"],
                                ReorderLevel = (short) reader["ReorderLevel"],
                                Discontinued = (bool) reader["Discontinued"]
                            };
                }
            }
            return p;
        }

        public bool UpdateProduct(ProductEntity product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd =
                    new SqlCommand(
                        "UPDATE products SET ProductName=@name,QuantityPerUnit=@unit,UnitPrice=@price,Discontinued=@discontinued WHERE ProductID=@id",
                        conn);
                cmd.Parameters.AddWithValue("@name", product.ProductName);
                cmd.Parameters.AddWithValue("@unit", product.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@price", product.UnitPrice);
                cmd.Parameters.AddWithValue("@discontinued", product.Discontinued);
                cmd.Parameters.AddWithValue("@id", product.ProductID);
                conn.Open();
                int numRows = cmd.ExecuteNonQuery();
                if (numRows != 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}