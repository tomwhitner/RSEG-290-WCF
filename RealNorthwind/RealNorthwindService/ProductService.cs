using MyWCFServices.RealNorthwindEntities;
using RealNorthwindLogic;
using System;

namespace MyWCFServices.RealNorthwindService
{
    public class ProductService : IProductService
    {
        private readonly ProductLogic _productLogic = new ProductLogic();

        #region IProductService Members

        public Product GetProduct(int id)
        {
            var productEntity = _productLogic.GetProduct(id);
            if (productEntity == null)
            {
                throw new Exception("No product found with id " + id);
            }

            return TranslateProductEntityToProductContractData(productEntity);
        }

        public bool UpdateProduct(Product product)
        {
            var productEntity = TranslateProductContractDataToProductEntity(product);
            return _productLogic.UpdateProduct(productEntity);
        }

        #endregion

        private Product TranslateProductEntityToProductContractData(
            ProductEntity productEntity)
        {
            return new Product
                       {
                           ProductID = productEntity.ProductID,
                           ProductName = productEntity.ProductName,
                           QuantityPerUnit = productEntity.QuantityPerUnit,
                           UnitPrice = productEntity.UnitPrice,
                           Discontinued = productEntity.Discontinued
                       };
        }

        private ProductEntity TranslateProductContractDataToProductEntity(
            Product product)
        {
            return new ProductEntity
                       {
                           ProductID = product.ProductID,
                           ProductName = product.ProductName,
                           QuantityPerUnit = product.QuantityPerUnit,
                           UnitPrice = product.UnitPrice,
                           Discontinued = product.Discontinued
                       };
        }
    }
}