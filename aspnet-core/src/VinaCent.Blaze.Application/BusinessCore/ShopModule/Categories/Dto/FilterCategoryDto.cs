using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

public class FilterCategoryDto: PagedResultRequestDto
{
    public string Keyword { get; set; }
    public string Language { get; set; }
    
    /// <summary>
    /// The parent id to identify the parent category.
    /// </summary>
    public int? ParentId { get; set; }
}