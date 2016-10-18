using System;

namespace ServiceStatus
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHost(new[]
            {
                "http://localhost:2016/"
            });

            host.Start();
            Console.WriteLine("Listening...");
            Console.ReadKey();
            host.Dispose();
        }
    }
}
