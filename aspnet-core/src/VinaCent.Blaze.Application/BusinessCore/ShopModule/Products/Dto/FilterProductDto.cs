using System;
using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

public class FilterProductDto: PagedResultRequestDto
{
    public string Keyword { get; set; }
    
    /// <summary>
    /// The parent id to identify the parent category.
    /// </summary>
    public int? CategoryId { get; set; }

    public Guid[] Tags { get; set; }
}