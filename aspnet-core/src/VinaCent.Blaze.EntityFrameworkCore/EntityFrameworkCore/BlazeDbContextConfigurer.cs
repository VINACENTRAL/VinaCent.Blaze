using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace VinaCent.Blaze.EntityFrameworkCore
{
    public static class BlazeDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BlazeDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BlazeDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
