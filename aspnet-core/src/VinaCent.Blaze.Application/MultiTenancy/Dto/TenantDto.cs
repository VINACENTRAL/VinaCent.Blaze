using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantDto : EntityDto
    {
        [AppRequired]
        [AppStringLength(AbpTenantBase.MaxTenancyNameLength)]
        [AppRegex(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [AppRequired]
        [AppStringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }        
        
        public bool IsActive {get; set;}
    }
}
