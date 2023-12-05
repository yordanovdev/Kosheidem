using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Kosheidem.EntityFrameworkCore
{
    public static class KosheidemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<KosheidemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<KosheidemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
