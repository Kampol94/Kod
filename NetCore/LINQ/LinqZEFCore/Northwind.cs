using Microsoft.EntityFrameworkCore;

namespace LinqZEFCore
{
    
    public class Northwind : DbContext
    {
        // these properties map to tables in the database 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(
          DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              @"Data Source=(localdb)\mssqllocaldb;" +
              "Initial Catalog=Northwind;" +
              "Integrated Security=true;" +
              "MultipleActiveResultSets=true;");
        }
    }
}
