using System;

namespace ZipCodeClient
{
    class ZipCodeClient
    {
        static void Main(string[] args)
        {
            var zipcodeSvc = new ZipCodeServiceClient();
            
            Console.Write("Enter username: ");
            var user = Console.ReadLine();

            do
            {
                Console.Write("Enter a zipcode (return to exit): ");
                var zipcode = Console.ReadLine();
                if (string.IsNullOrEmpty(zipcode)) break;
                var cityState = zipcodeSvc.Lookup(user, zipcode);
                Console.WriteLine(cityState);
            } while (true);
        }
    }
}