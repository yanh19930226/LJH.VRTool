using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LJH.VRTool.Authorization.Roles;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.MultiTenancy;

namespace LJH.VRTool.EntityFrameworkCore
{
    public class VRToolDbContext : AbpZeroDbContext<Tenant, Role, User, VRToolDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public VRToolDbContext(DbContextOptions<VRToolDbContext> options)
            : base(options)
        {
        }
    }
}
