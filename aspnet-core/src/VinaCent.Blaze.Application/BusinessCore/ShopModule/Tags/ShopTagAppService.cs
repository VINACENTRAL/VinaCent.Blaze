using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using VinaCent.Blaze.BusinessCore.Shop;
using VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Tags;

public class ShopTagAppService : AsyncCrudAppService<Tag, TagDto, Guid, FilterTagDto, CreateTagDto, TagDto>,
    IShopTagAppService
{
    public ShopTagAppService(IRepository<Tag, Guid> repository) : base(repository)
    {
    }

    protected override IQueryable<Tag> CreateFilteredQuery(FilterTagDto input)
    {
        var query = base.CreateFilteredQuery(input);

        query = query.WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Title.Contains(input.Keyword));
        
        return query;
    }

    protected override IQueryable<Tag> ApplySorting(IQueryable<Tag> query, FilterTagDto input)
    {
        return query.OrderBy(x => x.Title);
    }
}