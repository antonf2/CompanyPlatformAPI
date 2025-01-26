using CompanyAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data
{
    public class ContextDAL : DbContext
    {
        public ContextDAL(DbContextOptions<ContextDAL> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}
