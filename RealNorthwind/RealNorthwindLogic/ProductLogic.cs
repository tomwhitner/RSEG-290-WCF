using MyWCFServices.RealNorthwindEntities;

namespace RealNorthwindLogic
{
    public class ProductLogic
    {
        public ProductEntity GetProduct(int id)
        {
            // TODO: call data access layer to retrieve product
            var p = new ProductEntity
                        {
                            ProductID = id,
                            ProductName = "fake product name from business logic layer",
                            UnitPrice = (decimal) 20.00
                        };
            if (id > 50)
            {
                p.UnitsOnOrder = 30;
            }
            return p;
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
            return !(product.Discontinued && productInDB.UnitsOnOrder > 0);
        }
    }
}