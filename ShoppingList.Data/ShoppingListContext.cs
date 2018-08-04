using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Models;

namespace ShoppingList.Data
{
    public class ShoppingListContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password");
        }
    }
}