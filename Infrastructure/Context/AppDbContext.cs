using Bk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bk.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseTables.SetMappings(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName = bookkeeping.db", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookEntry> BookEntries { get; set; }
    }
}
//dotnet-ef migrations add M_tblBooks_M_Name_unique --project Infrastructure --startup-project UserInterface
//dotnet-ef migrations remove --project Infrastructure --startup-project UserInterface
//dotnet-ef database update --project Infrastructure --startup-project UserInterface