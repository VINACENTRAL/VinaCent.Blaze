using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.MultiTenancy;
using VinaCent.Blaze.AppCore.FileUnits;

namespace VinaCent.Blaze.EntityFrameworkCore
{
    public class BlazeDbContext : AbpZeroDbContext<Tenant, Role, User, BlazeDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<FileUnit> FileUnits { get; set; }

        public BlazeDbContext(DbContextOptions<BlazeDbContext> options)
            : base(options)
        {
        }
    }
}
