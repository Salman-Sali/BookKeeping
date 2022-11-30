using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Context
{
    public class DatabaseTables
    {
        public static void SetMappings(ModelBuilder modelBuilder)
        {
            TblUsers.SetMappings(modelBuilder);
            TblBooks.SetMappings(modelBuilder);
            TblBookEntries.SetMappings(modelBuilder);
        }
    }
}
