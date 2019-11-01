using Client.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Client.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage storageInstance = Storage.GetInstance();
            // Adding Clients
            storageInstance.Add(new Models.Entities.Client() { Id = Guid.NewGuid(), Name = "Sebastian Soler", Email = "sebastian.soler@test.com" });
            storageInstance.Add(new Models.Entities.Client() { Id = Guid.NewGuid(), Name = "Juan Perez", Email = "juan.Perez@test.com" });
            storageInstance.Add(new Models.Entities.Client() { Id = Guid.NewGuid(), Name = "Cosme Fulanito", Email = "Cosme.Fulanito@test.com" });

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
