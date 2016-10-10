using System;
using System.Configuration;
using Nancy.Hosting.Self;

namespace TrxViewer
{
    class Program
    {
        private const string HostUriKey = "hostUri";

        static void Main(string[] _)
        {
            var hostUri = GetHost();

            using (var host = new NancyHost(new Uri(hostUri)))
            {
                host.Start();
                Console.WriteLine($"Running on {hostUri}");
                Console.ReadLine();
            }
        }

        static string GetHost()
        {
            return ConfigurationManager.AppSettings[HostUriKey];
        }
    }
}
