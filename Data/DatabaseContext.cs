//using Microsoft.EntityFrameworkCore;

//namespace CsvToDatabaseApi.Data
//{
//    public class DatabaseContext : DbContext
//    {
//        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
//        { }
//    }
//}



using CsvToDatabaseApi.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CsvToDatabaseApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Companies> Companies { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define OrganizationId as the primary key if needed
            modelBuilder.Entity<Companies>().HasKey(c => c.OrganizationId);
        }
    }
}

