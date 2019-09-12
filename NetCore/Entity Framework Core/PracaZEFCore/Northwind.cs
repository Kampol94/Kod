using Microsoft.EntityFrameworkCore;

namespace PracaZEFCore
{
    public class Northwind : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              @"Data Source=(localdb)\mssqllocaldb;" +
              "Initial Catalog=Northwind;" +
                "Integrated Security=true;" +
                "MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(Category => Category.CategoryName)
                .IsRequired()
                .HasMaxLength(40);

            //globalny filtr nieprodukowanych 

            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.Discontinued);
        }
    }
}
