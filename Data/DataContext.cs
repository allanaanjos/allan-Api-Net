using Microsoft.EntityFrameworkCore;
using Shop.models;

namespace Shop.Data
{
    
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options): base(options){

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
    }
}