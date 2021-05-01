using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPDataRepository.Data;
using SPDataRepository.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPDataRepository.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            
            var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            
            await InitiateData(context);
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// Seed the Database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async static Task InitiateData(ProductDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Brands.AnyAsync() is false)
            {
                Brand[] brands = new Brand[]
                {
                    new()
                    {
                         Name="Apple",
                         Products=new List<Product>()
                         {
                            new()
                    {
                         Name="iPhone",
                    },
                            new()
                            {
                                Name = "iPad",
                            },
                            new()
                            {
                                Name = "MacBook",
                            },
                            new()
                            {
                                Name = "iWatch",
                            },
                         }
                    },
                    new()
                    {
                        Name = "Microsoft",
                        Products=new List<Product>()
                         {
                            new()
                            {
                                 Name="Surface Pro",
                            },
                            new()
                            {
                                Name = "Surface Book",
                            },
                            new()
                            {
                                Name = "xBox 360",
                            },
                         }
                    },
                };
                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }
        }
    }   
}
