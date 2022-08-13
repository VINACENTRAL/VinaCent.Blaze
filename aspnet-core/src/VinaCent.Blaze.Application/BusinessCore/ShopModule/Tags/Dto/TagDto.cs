using System;
using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

public class TagDto: EntityDto<Guid>
{
    public string Title { get; set; }
    public string MetaTitle { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
}