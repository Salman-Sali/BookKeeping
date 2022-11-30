using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Context
{
    public class TblBookEntries
    {
        internal static void SetMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntry>().ToTable("tblBookEntries");
            modelBuilder.Entity<BookEntry>().Property(x => x.Id).HasColumnName("BookEntryId");

            modelBuilder.Entity<BookEntry>().Property(x => x.BookId).IsRequired(true);
            modelBuilder.Entity<BookEntry>().HasIndex(x => x.BookId).IsUnique(false);
            modelBuilder.Entity<BookEntry>().HasOne(c => c.Book).WithMany(b => b.BookEntries)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntry>().Property(x => x.Date).IsRequired(true);

            modelBuilder.Entity<BookEntry>().Property(x => x.Description).IsRequired(false);
            modelBuilder.Entity<BookEntry>().Property(x => x.Amount).IsRequired(false);

            modelBuilder.Entity<BookEntry>().Property(x => x.Credit).IsRequired(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => x.Credit).IsUnique(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => new { x.Id, x.Credit }).IsUnique(false);

            modelBuilder.Entity<BookEntry>().Property(x => x.Debit).IsRequired(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => x.Debit).IsUnique(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => new { x.Id, x.Debit }).IsUnique(false);

            modelBuilder.Entity<BookEntry>().HasIndex(x => new { x.Id, x.Credit, x.Debit }).IsUnique(false);

            modelBuilder.Entity<BookEntry>().Property(x => x.Driver).IsRequired(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => x.Driver).IsUnique(false);

            modelBuilder.Entity<BookEntry>().Property(x => x.Vehicle).IsRequired(false);
            modelBuilder.Entity<BookEntry>().HasIndex(x => x.Vehicle).IsUnique(false);

            modelBuilder.Entity<BookEntry>().Property(x => x.Litre).IsRequired(false);

            modelBuilder.Entity<BookEntry>().Property(e => e.ItemType)
              .HasConversion(
                  x => x.ToString(),
                  x => x == null ? null : (ItemType)Enum.Parse(typeof(ItemType), x))
              .IsRequired(false);
        }
    }
}