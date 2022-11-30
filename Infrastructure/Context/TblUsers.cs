using Bk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Context
{
    public class TblUsers
    {
        internal static void SetMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tblUsers");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("UserId");
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired(true);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired(true);
        }
    }
}
