using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

public class FilterTagDto: PagedResultRequestDto
{
    public string Keyword { get; set; }
}