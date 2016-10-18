using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace ServiceStatus
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                new WindowsService().Start(args);

                Console.ReadKey();
            }
            else
                ServiceBase.Run(new WindowsService());
        }
    }
}
