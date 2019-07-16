using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LJH.VRTool.Configuration;

namespace LJH.VRTool.Web.Host.Startup
{
    [DependsOn(
       typeof(VRToolWebCoreModule))]
    public class VRToolWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public VRToolWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VRToolWebHostModule).GetAssembly());
        }
    }
}
