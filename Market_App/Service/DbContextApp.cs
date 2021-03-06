using Market_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Market_App.Service
{
    internal class DbContextApp : DbContext
    {   
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Market_db; Trusted_Connection = True;");
        }
    }
}
