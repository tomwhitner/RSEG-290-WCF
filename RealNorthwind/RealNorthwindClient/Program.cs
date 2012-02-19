using System;
using System.Globalization;
using System.ServiceModel;
using RealNorthwindClient.ProductServiceRef;

namespace RealNorthwindClient
{
    internal class Program
    {
        private static void MainOld(string[] args)
        {
            var client = new ProductServiceClient();
            var product = client.GetProduct(23);
            Console.WriteLine("product name is " + product.ProductName);
            Console.WriteLine("product price is " + product.UnitPrice.ToString(CultureInfo.InvariantCulture));
            product.UnitPrice = (decimal) 20.0;
            var result = client.UpdateProduct(product);
            Console.WriteLine("Update result is " + result.ToString(CultureInfo.InvariantCulture));
            
            TestException(client, 0); // channel is still open after a FaultException
            TestException(client, 999); // channel is Faulted after a non handled fault exception
            Console.WriteLine("\n\nTest Faulted client ...");
            client.GetProduct(20); // can't use a client with a Faulted channel
            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();

        }

        static void TestException(ProductServiceClient client, int id)
        {
            Console.WriteLine("\n\nTest {0} Fault Exception for product id {1}...", (id != 999) ? "handled" : "unhandled", id);
            try
            {
                client.GetProduct(id);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("The service operation timed out." + ex.Message);
            }
            catch (FaultException<ProductFault> ex)
            {
                Console.WriteLine("ProductFault: " + ex);
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Unknown Fault: " + ex);
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("There was a communication problem. " + ex.Message + ex.StackTrace);
            }
            Console.WriteLine("\n\nChannel Status after the exception: " + client.InnerChannel.State);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}