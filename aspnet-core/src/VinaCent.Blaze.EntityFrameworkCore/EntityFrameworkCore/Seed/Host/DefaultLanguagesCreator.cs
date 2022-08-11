using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using Abp.MultiTenancy;

namespace VinaCent.Blaze.EntityFrameworkCore.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        private static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly BlazeDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = (int?)MultiTenancyConsts.DefaultTenantId;
            return new List<ApplicationLanguage>
            {
                new(tenantId, "en-US", "English", "us.svg"),
                new(tenantId, "vi-VN", "English", "vn.svg"),
            };
        }

        public DefaultLanguagesCreator(BlazeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);
            _context.SaveChanges();
        }
    }
}
