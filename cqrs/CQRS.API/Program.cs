using System;
using CQRS.Infra.Command.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSLaqus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = BuildWebHost(args);

                using (var scope = host.Services.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetService<EntityContext>())
                    {
                        // Necessary to seed the InMemory database initial data
                        context.Database.EnsureCreated();
                    }

                    host.Run();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .Build();
        }

    }
}
