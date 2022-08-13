using System;
using Abp.Application.Services;
using VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Tags;

public interface IShopTagAppService: IAsyncCrudAppService<TagDto, Guid, FilterTagDto, CreateTagDto, TagDto>
{
    
}