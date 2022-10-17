using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace OrderMe.Catalog.DataAccess.Contexts
{
    public static class CatalogDbContextConfigurer<T> where T : CatalogDbContext
    {
        public static void Configure(
           DbContextOptionsBuilder<T> dbContextOptions,
           string connectionString,
           int maxRetryCount = 5,
           int maxRetryDelayInSeconds = 30)
        {
            dbContextOptions.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount,
                    TimeSpan.FromSeconds(maxRetryDelayInSeconds),
                    null);
                sqlOptions.MigrationsAssembly(typeof(T).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory");
            });
        }
    }
}
