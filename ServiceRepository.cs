using System;
using System.ServiceProcess;

namespace ServiceStatus
{
    internal class ServiceRepository
    {
        public Service GetStatus(string name)
        {
            var manager = new ServiceController();
            manager.ServiceName = name;

            try
            {
                return new Service
                {
                    Name = manager.ServiceName,
                    Status = manager.Status
                };
            }
            catch (InvalidOperationException exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }
    }
}
