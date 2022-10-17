using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Castle.Core.Internal;
using System;
using Microsoft.Extensions.Configuration;

namespace OrderMe.Catalog.DataAccess.Contexts
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string connectionString)
        {
            return CreateDbContext(new[] { connectionString });
        }

        public CatalogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CatalogDbContext>();
            string connectionString;
            if (!args.IsNullOrEmpty())
            {
                connectionString = args[0];
            }
            else
            {
                //Only used during development for migration creation
                var appConfiguration = AppConfiguration.Get();
                connectionString = appConfiguration.GetConnectionString("DefaultConnection")
                                   ?? appConfiguration.GetConnectionString("Local");
            }
            CatalogDbContextConfigurer<CatalogDbContext>.Configure(
              builder,
              connectionString
            );
            return (CatalogDbContext)Activator.CreateInstance(typeof(CatalogDbContext), builder.Options);
        }
    }
}
