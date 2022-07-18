using Abp.Application.Services;
using VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products;

public interface
    IShopProductAppService : IAsyncCrudAppService<ProductDto, long, FilterProductDto, CreateProductDto,
        UpdateProductDto>
{
}