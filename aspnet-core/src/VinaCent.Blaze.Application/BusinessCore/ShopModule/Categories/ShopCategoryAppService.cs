using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using VinaCent.Blaze.BusinessCore.Shop;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories;

public class ShopCategoryAppService:AsyncCrudAppService<Category, CategoryDto, int, FilterCategoryDto, CreateCategoryDto, UpdateCategoryDto>,
    IShopCategoryAppService
{
    public ShopCategoryAppService(IRepository<Category, int> repository) : base(repository)
    {
    }

    protected override IQueryable<Category> CreateFilteredQuery(FilterCategoryDto input)
    {
        var query = base.CreateFilteredQuery(input);
        
        query = query.WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId);

        query = query.WhereIf(!input.Keyword.IsNullOrEmpty(),
            x => x.Title.Contains(input.Keyword) || x.Content.Contains(input.Keyword));
        
        return query;
    }

    protected override IQueryable<Category> ApplySorting(IQueryable<Category> query, FilterCategoryDto input)
    {
        return query.OrderBy(x => x.Title).ThenByDescending(x => x.CreationTime);
    }

    public async Task<List<CategoryListDto>> GetAllParentListAsync(int id)
    {
        var parents = new List<CategoryListDto>();

        var current = await Repository.GetAsync(id);

        if (!current.ParentId.HasValue) return parents;

        var parentId = current.ParentId;
        while (parentId.HasValue)
        {
            var parent = await Repository.GetAsync(parentId.Value);
            parents = parents.Prepend(ObjectMapper.Map<CategoryListDto>(parent)).ToList();
            parentId = parent.ParentId;
        }

        return parents;
    }

    public async Task<List<CategoryListDto>> GetAllListByLevelAsync(int level)
    {
        var listResult = await Repository.GetAllListAsync(x => x.Level == level && x.IsActive);
        return ObjectMapper.Map<List<CategoryListDto>>(listResult);
    }
}