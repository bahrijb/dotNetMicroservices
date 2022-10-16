using Microsoft.EntityFrameworkCore;
using OrderMe.Catalog.DataAccess.Models;
using System.Threading.Tasks;

namespace OrderMe.Catalog.DataAccess.Contexts
{
    public class CatalogDbContext : DbContext, ICatalogDbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
           : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
