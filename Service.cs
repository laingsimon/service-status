using System.ServiceProcess;

namespace ServiceStatus
{
    public class Service
    {
        public string Name { get; set; }
        public ServiceControllerStatus Status { get; set; }
    }
}
