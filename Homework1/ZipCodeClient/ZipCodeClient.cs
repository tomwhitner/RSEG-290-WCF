using System;

namespace ZipCodeClient
{
    class ZipCodeClient
    {
        static void Main(string[] args)
        {
            // create zipcode service client
            var zipcodeSvc = new ZipCodeServiceClient();

            // prompt for username
            Console.Write("Enter username: ");
            var user = Console.ReadLine();

            do
            {
                // prompt for zipcode to lookup
                Console.Write("Enter a zipcode (return to exit): ");
                var zipcode = Console.ReadLine();

                // exit on blank zipcode
                if (string.IsNullOrEmpty(zipcode)) break;  

                // invoke service to perform lookup
                var cityState = zipcodeSvc.Lookup(user, zipcode);

                // output results to console.
                Console.WriteLine(cityState);

            } while (true);
        }
    }
}