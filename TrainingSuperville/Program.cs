using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TrainingSuperville.Models;
using TrainingSuperville.Models.Entities;

namespace TrainingSuperville
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage storageInstance = Storage.GetInstance();

            // Adding Products
            storageInstance.Add(new Product() { Id = Guid.NewGuid(), Name = "Smartwatch West C1S ", Code = "000100" });
            storageInstance.Add(new Product() { Id = Guid.NewGuid(), Name = "Smartband Samsung Galaxy R370", Code = "000101" });
            storageInstance.Add(new Product() { Id = Guid.NewGuid(), Name = "Fitband Samsung Galaxy FIT BT", Code = "000102" });
            storageInstance.Add(new Product() { Id = Guid.NewGuid(), Name = "Smartwatch GADNIC SW20", Code = "000103" });

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() //Serilog Config
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
