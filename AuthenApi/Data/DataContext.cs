using AuthenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-P2GJQLE\\WINCC;Database=userdb;Trusted_Connection=true;Encrypt=False;");
        }

        public DbSet<User> Users => Set<User>();

    }
}
