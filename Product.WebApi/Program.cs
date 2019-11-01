using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Product.WebApi.Models;

namespace Product.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage storageInstance = Storage.GetInstance();
            // Adding Products
            storageInstance.Add(new Models.Entities.Product() { Id = Guid.NewGuid(), Name = "Smartwatch West C1S ", Code = "000100" });
            storageInstance.Add(new Models.Entities.Product() { Id = Guid.NewGuid(), Name = "Smartband Samsung Galaxy R370", Code = "000101" });
            storageInstance.Add(new Models.Entities.Product() { Id = Guid.NewGuid(), Name = "Fitband Samsung Galaxy FIT BT", Code = "000102" });
            storageInstance.Add(new Models.Entities.Product() { Id = Guid.NewGuid(), Name = "Smartwatch GADNIC SW20", Code = "000103" });

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}