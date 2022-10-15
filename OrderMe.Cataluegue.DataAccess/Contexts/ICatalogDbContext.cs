using Microsoft.EntityFrameworkCore;
using OrderMe.Catalog.DataAccess.Models;
using System.Threading.Tasks;

namespace OrderMe.Catalog.DataAccess.Contexts
{
    public interface ICatalogDbContext
    {
        DbSet<Category> Categories { get; set; }
        Task<int> SaveChangesAsync();
    }
}