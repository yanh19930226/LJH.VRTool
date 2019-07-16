using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LJH.VRTool.Authorization;

namespace LJH.VRTool
{
    [DependsOn(
        typeof(VRToolCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class VRToolApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<VRToolAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(VRToolApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
