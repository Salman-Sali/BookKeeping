using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Context
{
    public class TblBooks
    {
        internal static void SetMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("tblBooks");
            modelBuilder.Entity<Book>().Property(x => x.Id).HasColumnName("BookId");
            modelBuilder.Entity<Book>().Property(x => x.Name).IsRequired(true);
            modelBuilder.Entity<Book>().HasIndex(x => x.Name).IsUnique(true);
            modelBuilder.Entity<Book>().Property(x => x.Phone).IsRequired(false);
            modelBuilder.Entity<Book>().Property(x => x.DiscountPerLitre).IsRequired(false);
            modelBuilder.Entity<Book>().Property(e => e.BookType)
              .HasConversion(
                  x => x.ToString(),
                  x => (BookType)Enum.Parse(typeof(BookType), x));
        }
    }
}
