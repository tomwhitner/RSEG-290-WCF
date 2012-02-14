using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealNorthwindClient.ProductServiceRef;

namespace RealNorthwindClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductServiceClient client = new ProductServiceClient();
            Product product = client.GetProduct(23);
            Console.WriteLine("product name is " + product.ProductName);
            Console.WriteLine("product price is " + product.UnitPrice.ToString());
            product.UnitPrice = (decimal)20.0;
            bool result = client.UpdateProduct(product);
            Console.WriteLine("Update result is " + result.ToString());
            Console.ReadLine(); 
        }
    }
}
