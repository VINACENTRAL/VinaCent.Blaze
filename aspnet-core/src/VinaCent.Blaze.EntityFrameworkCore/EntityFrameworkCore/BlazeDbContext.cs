using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.MultiTenancy;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.AppCore.TextTemplates;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.BusinessCore;

namespace VinaCent.Blaze.EntityFrameworkCore
{
    // dotnet ef migrations add "Add.CurrencyUnits_CurrencyExchangeRates" -p ./src/VinaCent.Blaze.EntityFrameworkCore -s ./src/VinaCent.Blaze.Web.Mvc --context BlazeDbContext
    // dotnet ef database update -p ./src/VinaCent.Blaze.EntityFrameworkCore -s ./src/VinaCent.Blaze.Web.Mvc --context BlazeDbContext
    // dotnet ef migrations remove -p ./src/VinaCent.Blaze.EntityFrameworkCore -s ./src/VinaCent.Blaze.Web.Mvc --context BlazeDbContext
    public class BlazeDbContext : AbpZeroDbContext<Tenant, Role, User, BlazeDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<FileUnit> FileUnits { get; set; }

        public DbSet<TextTemplate> TextTemplates { get; set; }

        public DbSet<CommonData> CommonDatas { get; set; }

        #region Business Core
        public DbSet<CurrencyUnit> CurrencyUnits { get; set; }
        public DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }
        #endregion

        public BlazeDbContext(DbContextOptions<BlazeDbContext> options)
            : base(options)
        {
        }
    }
}
