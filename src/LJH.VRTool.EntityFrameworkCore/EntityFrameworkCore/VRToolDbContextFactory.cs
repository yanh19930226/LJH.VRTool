using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LJH.VRTool.Configuration;
using LJH.VRTool.Web;

namespace LJH.VRTool.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class VRToolDbContextFactory : IDesignTimeDbContextFactory<VRToolDbContext>
    {
        public VRToolDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<VRToolDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            VRToolDbContextConfigurer.Configure(builder, configuration.GetConnectionString(VRToolConsts.ConnectionStringName));

            return new VRToolDbContext(builder.Options);
        }
    }
}
