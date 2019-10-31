using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi1.Models;
using WebApi1.Models.Entities;

namespace WebApi1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage storageInstance = Storage.GetInstance();
            // Adding Clients
            storageInstance.Add(new Client() { Id = Guid.NewGuid(), Name = "Sebastian Soler", Email = "sebastian.soler@test.com" });
            storageInstance.Add(new Client() { Id = Guid.NewGuid(), Name = "Juan Perez", Email = "juan.Perez@test.com" });
            storageInstance.Add(new Client() { Id = Guid.NewGuid(), Name = "Cosme Fulanito", Email = "Cosme.Fulanito@test.com" });

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
