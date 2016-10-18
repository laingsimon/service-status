using ServiceStatus.Properties;
using System.Linq;
using System.ServiceProcess;

namespace ServiceStatus
{
    public class WindowsService : ServiceBase
    {
        private WebHost _host;

        public WindowsService()
        {
            _host = new WebHost(Settings.Default.Urls.Cast<string>().ToArray());
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            _host.Start();
        }

        protected override void OnStop()
        {
            _host.Dispose();
        }
    }
}
