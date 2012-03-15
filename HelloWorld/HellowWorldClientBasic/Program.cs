using System;

namespace HellowWorldClientBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HelloWorldServiceClient();

            client.ClientCredentials.UserName.UserName = "mom";
            client.ClientCredentials.UserName.Password = "01151965";

            Console.WriteLine(client.GetMessage("Tom Whitner"));
        }
    }
}