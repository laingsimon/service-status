using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace ServiceStatus
{
    using System.IO;
    using System.Linq;

    public static class Program
    {
        private const string _serviceName = "ServiceStatus";

        public static void Main(string[] args)
        {
            if (args.Any(arg => arg == "/install"))
            {
                _Install();
                return;
            }

            if (args.Any(arg => arg == "/uninstall"))
            {
                _Uninstall();
                return;
            }

            if (Debugger.IsAttached)
            {
                new WindowsService().Start(args);

                Console.ReadKey();
            }
            else
                ServiceBase.Run(new WindowsService());
        }

        private static void _Install()
        {
            var pathToExecutable = Path.GetFullPath(Environment.GetCommandLineArgs()[0]);
            var arguments = $"create \"{_serviceName}\" start= auto binPath= \"{pathToExecutable}\" displayName= \"Service Status\"";

            var process = new Process
            {
                StartInfo =
                {
                    FileName = "sc.exe",
                    Arguments = arguments,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();

            Environment.ExitCode = process.ExitCode;
        }

        private static void _Uninstall()
        {
            var arguments = $"delete \"{_serviceName}\"";

            var process = new Process
            {
                StartInfo =
                {
                    FileName = "sc.exe",
                    Arguments = arguments,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();

            Environment.ExitCode = process.ExitCode;
        }
    }
}
