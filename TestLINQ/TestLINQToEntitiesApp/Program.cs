using System;
using System.Collections.Generic;
using System.Linq;

namespace TestLINQToEntitiesApp
{
    class Program
    {
        static void Main()
        {
            // CRUD operations on tables
            TestTables();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        static void TestTables()
        {
            var nwEntities = new NorthwindEntities();
            // retrieve all Beverages
            IEnumerable<Product> beverages =
                from p in nwEntities.Products
                where p.Category.CategoryName == "Beverages"
                orderby p.ProductName
                select p;
            Console.WriteLine("There are {0} Beverages", beverages.Count());
            // update one product
            var bev1 = beverages.ElementAtOrDefault(10);
            if (bev1 != null)
            {
                if (bev1.UnitPrice != null) {
                    decimal newPrice = (decimal)bev1.UnitPrice + 10.00m;
                    Console.WriteLine("The price of {0} is {1}. Update to {2}",
                                      bev1.ProductName, bev1.UnitPrice, newPrice);
                    bev1.UnitPrice = newPrice;
                }
            }﻿
            nwEntities.SaveChanges();

            // insert a product
            var newProduct = new Product
            {
                ProductName = "new test product"
            };
            nwEntities.Products.AddObject(newProduct);
            nwEntities.SaveChanges();
            Console.WriteLine("Added a new product");

            // delete a product
            IQueryable<Product> productsToDelete =
                from p in nwEntities.Products
                where p.ProductName == "new test product"
                select p;
            if (productsToDelete.Any())
            {
                foreach (var p in productsToDelete)
                {
                    nwEntities.DeleteObject(p);
                    Console.WriteLine("Deleted product {0}",
                    p.ProductID);
                }
                nwEntities.SaveChanges();
            }
            nwEntities.Dispose();
        }
    }
}