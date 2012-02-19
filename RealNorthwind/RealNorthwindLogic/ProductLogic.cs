using MyWCFServices.RealNorthwindEntities;
using MyWCFServices.RealNorthwindDAL;

namespace MyWCFServices.RealNorthwindLogic
{
    public class ProductLogic
    {
        readonly ProductDAO _productDAO = new ProductDAO();

        public ProductEntity GetProduct(int id)
        {
            return _productDAO.GetProduct(id);
        }

        public bool UpdateProduct(ProductEntity product)
        {
            // TODO: call data access layer to update product
            // first check to see if it is a valid price
            if (product.UnitPrice <= 0)
            {
                return false;
            }

            // ProductName can't be empty
            if (string.IsNullOrEmpty(product.ProductName))
            {
                return false;
            }

            // QuantityPerUnit can't be empty
            if (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                return false;
            }

            // then validate other properties
            var productInDB = GetProduct(product.ProductID);

            // invalid product to update
            if (productInDB == null)
            {
                return false;
            }

            // a product can't be discontinued if there are non-fulfilled orders
            if (product.Discontinued == true && productInDB.UnitsOnOrder > 0)
            {
                return false;
            }

            return _productDAO.UpdateProduct(product);
        }
    }
}