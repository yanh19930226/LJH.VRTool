using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.Editions;

namespace LJH.VRTool.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
