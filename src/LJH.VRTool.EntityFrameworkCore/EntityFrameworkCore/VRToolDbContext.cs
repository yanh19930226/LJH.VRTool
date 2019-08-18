using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LJH.VRTool.Authorization.Roles;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.MultiTenancy;
using LJH.VRTool.Entities;
using LJH.VRTool.Entities.Student;
using LJH.VRTool.Entities.Teacher;
using LJH.VRTool.Entities.StudentTeacher;

namespace LJH.VRTool.EntityFrameworkCore
{
    public class VRToolDbContext : AbpZeroDbContext<Tenant, Role, User, VRToolDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public VRToolDbContext(DbContextOptions<VRToolDbContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
    }
}
