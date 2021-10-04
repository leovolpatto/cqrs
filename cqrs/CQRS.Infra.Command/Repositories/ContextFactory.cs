using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace CQRS.Infra.Command.Repositories
{
    public class ContextFactory  : IDesignTimeDbContextFactory<EntityContext>
    {
        public EntityContext CreateDbContext(string[] args)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            dir = Path.Combine(dir, "CQRS.API");

            Console.WriteLine("Trying to read appsettings.json from {0}", dir);

            IConfigurationRoot configuration = new ConfigurationBuilder()                
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var builder = new DbContextOptionsBuilder<EntityContext>();
            var connectionString = configuration.GetConnectionString("cqrs-db");
            builder.UseNpgsql<EntityContext>(connectionString);
            return new EntityContext(builder.Options);
        }
    }
}
