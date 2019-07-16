using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LJH.VRTool.EntityFrameworkCore
{
    public static class VRToolDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<VRToolDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
            builder.UseSqlServer(connectionString, b => b.UseRowNumberForPaging());
        }

        public static void Configure(DbContextOptionsBuilder<VRToolDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
