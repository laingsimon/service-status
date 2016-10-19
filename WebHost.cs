using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Net;

namespace ServiceStatus
{
    using System.Text.RegularExpressions;

    internal class WebHost : IDisposable
    {
        private readonly string[] _urls;
        private IDisposable _host;

        public WebHost(string[] urls)
        {
            _urls = urls.Select(url => Regex.Replace(url, @"%COMPUTERNAME%", Dns.GetHostName(), RegexOptions.IgnoreCase)).ToArray();
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
