using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Smart.Web.Infrastructure;

namespace Smart.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Global.LoadSettings();
            Global.Handlers = GatewayProtocol.ScanForHandlers(Assembly.GetExecutingAssembly());
            MainLogger.Initialize(new LoggerConfigModel { LogRootDirectory = Global.CurrentDirectory });

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //IIS
                .UseKestrel()
                .UseIISIntegration()
                //IIS
                .UseStartup<Startup>();
    }
}
