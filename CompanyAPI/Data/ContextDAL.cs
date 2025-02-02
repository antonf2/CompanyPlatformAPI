using CompanyAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data
{
    public class ContextDAL : DbContext
    {
        public ContextDAL(DbContextOptions<ContextDAL> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users", "HumanResources");
            modelBuilder.Entity<InventoryItem>().ToTable("Items", "Inventory");
            modelBuilder.Entity<InventoryMovement>().ToTable("Movements", "Inventory");
        }
    }
}