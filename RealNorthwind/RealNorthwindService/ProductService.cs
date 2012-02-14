using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyWCFServices.RealNorthwindService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in both code and config file together.
    public class ProductService : IProductService
    {
        public Product GetProduct(int id)
        {
            // TODO: call business logic layer to retrieve product
            Product product = new Product();
            product.ProductID = id;
            product.ProductName = "fake product name from service layer";
            product.UnitPrice = (decimal)10.0;
            return product;
        }

        public bool UpdateProduct(Product product)
        {
            // TODO: call business logic layer to update product
            if (product.UnitPrice <= 0)
                return false;
            else
                return true;
        }
    }
}
