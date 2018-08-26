using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace flood_hackathon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = hostingContext.HostingEnvironment;
                    config
                        // .SetBasePath(environment.ContentRootPath)
                        .AddJsonFile("appsettings.secrets.json", optional: true)
                        .AddJsonFile($"appSettings.{environment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();

                })
                .UseStartup<Startup>()
                .UseUrls("https://localhost:8003");
    }
}
