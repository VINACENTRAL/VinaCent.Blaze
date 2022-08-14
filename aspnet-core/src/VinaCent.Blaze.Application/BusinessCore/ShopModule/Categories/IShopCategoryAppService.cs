using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories;

public interface IShopCategoryAppService: IAsyncCrudAppService<CategoryDto, int, FilterCategoryDto, CreateCategoryDto, UpdateCategoryDto>
{
    /// <summary>
    /// Get all parent category of inputted category, and sort them by level from root (0)
    /// No included itself
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<CategoryListDto>> GetAllParentListAsync(int id);

    Task<List<CategoryListDto>> GetAllListByLevelAsync(int level);



    //
    // /// <summary>
    // /// Get all nearest level cateory. Ex in level 0 will get paged result level 1
    // /// </summary>
    // /// <param name="input"></param>
    // /// <returns></returns>
    // Task<PagedResultDto<CategoryDto>> GetNearestChildLevelListAsync(FilterCategoryDto input);
    //
    // /// <summary>
    // /// Get all nearest level cateory. Ex in level 0 will get all result level 1
    // /// </summary>
    // /// <param name="input"></param>
    // /// <returns></returns>
    // Task<List<CategoryDto>> GetAllNearestChildLevelListAsync(FilterCategoryDto input);
}