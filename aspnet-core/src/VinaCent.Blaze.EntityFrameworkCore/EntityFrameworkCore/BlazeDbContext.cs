using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.MultiTenancy;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.AppCore.TextTemplates;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.BusinessCore;
using VinaCent.Blaze.BusinessCore.Shop.Categories;
using VinaCent.Blaze.BusinessCore.Shop.CartItems;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.BusinessCore.Shop.ProductCategories;
using VinaCent.Blaze.BusinessCore.Shop.ProductImages;
using VinaCent.Blaze.BusinessCore.Shop.ProductReviews;
using VinaCent.Blaze.BusinessCore.Shop.ProductTags;
using VinaCent.Blaze.BusinessCore.Shop.Tags;
using VinaCent.Blaze.BusinessCore.Shop.ProductMetas;

namespace VinaCent.Blaze.EntityFrameworkCore
{
    // dotnet ef migrations add "Update.Product.v230121082022" -p ./src/VinaCent.Blaze.EntityFrameworkCore -s ./src/VinaCent.Blaze.Web.Mvc --context BlazeDbContext
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

        // ======================== START SHOP ======================== //
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductMeta> ProductMetas { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<Tag> Tags { get; set; }
        // ========================  END SHOP  ======================== //

        #endregion

        public BlazeDbContext(DbContextOptions<BlazeDbContext> options)
            : base(options)
        {
        }
    }
}