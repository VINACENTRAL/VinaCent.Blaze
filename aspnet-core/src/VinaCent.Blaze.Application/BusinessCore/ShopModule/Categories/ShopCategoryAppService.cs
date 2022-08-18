using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using VinaCent.Blaze.BusinessCore.Shop.Categories;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories;

public class ShopCategoryAppService : BlazeAppServiceBase,
    IShopCategoryAppService
{
    private readonly IRepository<Category> _repository;
    private readonly IRepository<CategoryTranslation> _translationRepository;

    public ShopCategoryAppService(IRepository<Category> repository, IRepository<CategoryTranslation> translationRepository)
    {
        _repository = repository;
        _translationRepository = translationRepository;
    }

    public async Task<CategoryDto> GetAsync(EntityDto input)
    {
        var result = await _repository.GetAllIncluding(x => x.Translations).FirstOrDefaultAsync(x => x.Id == input.Id);
        return ObjectMapper.Map<CategoryDto>(result);
    }

    public async Task DeleteAsync(EntityDto input)
    {
        await _translationRepository.DeleteAsync(x => x.CoreId == input.Id);
        await _repository.DeleteAsync(input.Id);
    }

    public async Task<PagedResultDto<CategoryListDto>> GetAllAsync(FilterCategoryDto input)
    {
        var query = CreateFilteredQuery(input);
        var total = await query.CountAsync();
        query = ApplySorting(query, input);
        query = query.PageBy(input);

        var dataSet = query.ToList().Select(MapToEntityDto).ToList();

        return new PagedResultDto<CategoryListDto>(total, dataSet);
    }

    private CategoryListDto MapToEntityDto(Category inpt)
    {
        var dto = ObjectMapper.Map<CategoryListDto>(inpt);
        var currentLanguage = CultureInfo.CurrentUICulture.Name;

        var trans = _translationRepository.FirstOrDefault(x => x.CoreId == inpt.Id && x.Language.Equals(currentLanguage));

        dto = ObjectMapper.Map(trans, dto);
        dto.Id = inpt.Id;

        return dto;
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
    {
        var category = ObjectMapper.Map<Category>(input);
        var translations = category.Translations;
        category.Translations = new List<CategoryTranslation>();

        if (category.ParentId.HasValue)
        {
            category.Level = _repository.Get(category.ParentId.Value)?.Level ?? 0;
        } else
        {
            category.Level = 0;
        }

        category = await _repository.InsertAsync(category);
        foreach (var translation in translations)
        {
            var trans = await _translationRepository.InsertAsync(translation);
            category.Translations.Add(trans);
        }

        return ObjectMapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto input)
    {
        var category = ObjectMapper.Map<Category>(input);
        var translations = category.Translations;

        // Clear old translation
        category.Translations = new List<CategoryTranslation>();
        await _translationRepository.DeleteAsync(x => x.CoreId == category.Id);

        if (category.ParentId.HasValue)
        {
            category.Level = _repository.Get(category.ParentId.Value)?.Level ?? 0;
        }
        else
        {
            category.Level = 0;
        }

        category = await _repository.UpdateAsync(category);

        // Insert new translation again
        foreach (var translation in translations)
        {
            var trans = await _translationRepository.InsertAsync(translation);
            category.Translations.Add(trans);
        }

        return ObjectMapper.Map<CategoryDto>(category);
    }



    protected IQueryable<Category> CreateFilteredQuery(FilterCategoryDto input)
    {
        var query = _repository.GetAll();

        query = query.WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId);

        if (!input.Keyword.IsNullOrWhiteSpace())
        {
            query = query.Join(_translationRepository.GetAll(),
                c => c.Id,
                ct => ct.CoreId,
                (c, ct) => new { c, ct.Title, ct.Content })
                .Where(x => x.Title.Contains(input.Keyword) || x.Content.Contains(input.Keyword))
                .Select(x => x.c);
        }

        return query;
    }

    protected IQueryable<Category> ApplySorting(IQueryable<Category> query, FilterCategoryDto input)
    {
        return query.OrderBy(x => x.CreationTime);
    }

    public async Task<List<CategoryListDto>> GetAllParentListAsync(int id)
    {
        var parents = new List<CategoryListDto>();

        var current = await _repository.GetAsync(id);

        if (!current.ParentId.HasValue) return parents;

        var parentId = current.ParentId;
        while (parentId.HasValue)
        {
            var parent = await _repository.GetAsync(parentId.Value);
            parents = parents.Prepend(ObjectMapper.Map<CategoryListDto>(parent)).ToList();
            parentId = parent.ParentId;
        }

        return parents;
    }

    public async Task<List<CategoryListDto>> GetAllListByLevelAsync(int level)
    {
        var listResult = await _repository.GetAllListAsync(x => x.Level == level && x.IsActive);
        return ObjectMapper.Map<List<CategoryListDto>>(listResult);
    }
}