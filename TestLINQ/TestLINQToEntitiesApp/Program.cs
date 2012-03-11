using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace TestLINQToEntitiesApp
{
    class Program
    {
        static void Main()
        {
            // CRUD operations on tables
            //TestTables();
            ViewGeneratedSql();
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
                if (bev1.UnitPrice != null)
                {
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

        static void ViewGeneratedSql()
        {﻿﻿
            var nwEntities = new NorthwindEntities();
            IQueryable<Product> beverages =
                from p in nwEntities.Products
                where p.Category.CategoryName == "Beverages"
                orderby p.ProductName
                select p;
            // view SQL using ToTraceString method
            Console.WriteLine("The SQL statement is:\n" + beverages.ToTraceString());
            nwEntities.Dispose();
        }
    }

    public static class MyExtensions
    {
        public static string ToTraceString<T>(this IQueryable<T> t)
        {
            string sql = "";
            var oqt = t as ObjectQuery<T>;
            if (oqt != null)
                sql = oqt.ToTraceString();
            return sql;
        }
    }
}
