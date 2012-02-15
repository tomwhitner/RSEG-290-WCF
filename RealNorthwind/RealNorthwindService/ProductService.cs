using MyWCFServices.RealNorthwindEntities;
using RealNorthwindLogic;

namespace MyWCFServices.RealNorthwindService
{
    public class ProductService : IProductService
    {
        private readonly ProductLogic _productLogic = new ProductLogic();

        #region IProductService Members

        public Product GetProduct(int id)
        {
            var productEntity = _productLogic.GetProduct(id);
            var product = new Product();
            TranslateProductEntityToProductContractData(productEntity, product);
            return product;
        }

        public bool UpdateProduct(Product product)
        {
            var productEntity = new ProductEntity();
            TranslateProductContractDataToProductEntity(product, productEntity);
            return _productLogic.UpdateProduct(productEntity);
        }

        #endregion

        private void TranslateProductEntityToProductContractData(
            ProductEntity productEntity,
            Product product)
        {
            product.ProductID = productEntity.ProductID;
            product.ProductName = productEntity.ProductName;
            product.QuantityPerUnit = productEntity.QuantityPerUnit;
            product.UnitPrice = productEntity.UnitPrice;
            product.Discontinued = productEntity.Discontinued;
        }

        private void TranslateProductContractDataToProductEntity(
            Product product,
            ProductEntity productEntity)
        {
            productEntity.ProductID = product.ProductID;
            productEntity.ProductName = product.ProductName;
            productEntity.QuantityPerUnit = product.QuantityPerUnit;
            productEntity.UnitPrice = product.UnitPrice;
            productEntity.Discontinued = product.Discontinued;
        }
    }
}