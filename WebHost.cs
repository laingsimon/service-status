using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Net;

namespace ServiceStatus
{
    internal class WebHost : IDisposable
    {
        private readonly string[] _urls;
        private IDisposable _host;

        public WebHost(string[] urls)
        {
            _urls = urls.Select(url => url.Replace("%COMPUTERNAME%", Dns.GetHostName())).ToArray();
        }

        public void Start()
        {
            var options = new StartOptions();
            foreach (var url in _urls)
                options.Urls.Add(url);

            _host = WebApp.Start<Startup>(options);
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}
