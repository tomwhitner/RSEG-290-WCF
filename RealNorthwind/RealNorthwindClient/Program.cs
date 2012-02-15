using System;
using System.Globalization;
using RealNorthwindClient.ProductServiceRef;

namespace RealNorthwindClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new ProductServiceClient();
            var product = client.GetProduct(23);
            Console.WriteLine("product name is " + product.ProductName);
            Console.WriteLine("product price is " + product.UnitPrice.ToString(CultureInfo.InvariantCulture));
            product.UnitPrice = (decimal) 20.0;
            var result = client.UpdateProduct(product);
            Console.WriteLine("Update result is " + result.ToString(CultureInfo.InvariantCulture));
            Console.ReadLine();
        }
    }
}