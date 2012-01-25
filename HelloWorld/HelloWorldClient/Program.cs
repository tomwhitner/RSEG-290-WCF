using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HelloWorldServiceClient();
            Console.WriteLine(client.GetMessage("Tom Whitner"));
            Console.ReadLine();
        }
    }
}
