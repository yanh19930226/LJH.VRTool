using Abp.MultiTenancy;
using LJH.VRTool.Authorization.Users;

namespace LJH.VRTool.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
