using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDeleterService.Core.Scheduling;
using FileDeleterService.Core.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileDeleterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IScheduledTask, FileDeleterTask>();

                    services.AddScheduler((sender, args) =>
                    {
                        Console.Write(args.Exception.Message);
                        args.SetObserved();
                    });                    
                });
    }
}
