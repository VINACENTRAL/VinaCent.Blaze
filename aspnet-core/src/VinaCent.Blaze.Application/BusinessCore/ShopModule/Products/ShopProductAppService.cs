using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using VinaCent.Blaze.BusinessCore.Shop.ProductCategories;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.BusinessCore.Shop.ProductTags;
using VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products;

public class
    ShopProductAppService : AsyncCrudAppService<Product, ProductDto, long, FilterProductDto, CreateProductDto,
        UpdateProductDto>, IShopProductAppService
{
    private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
    private readonly IRepository<ProductTag, Guid> _productTagRepository;

    public ShopProductAppService(IRepository<Product, long> repository,
        IRepository<ProductCategory, Guid> productCategoryRepository,
        IRepository<ProductTag, Guid> productTagRepository) : base(repository)
    {
        _productCategoryRepository = productCategoryRepository;
        _productTagRepository = productTagRepository;
    }

    protected override IQueryable<Product> CreateFilteredQuery(FilterProductDto input)
    {
        var query = base.CreateFilteredQuery(input);

        query = query.WhereIf(!input.Keyword.IsNullOrEmpty(),
            x => x.Title.Contains(input.Keyword) ||
                 x.Summary.Contains(input.Keyword));

        // Filter by category
        if (input.CategoryId.HasValue)
        {
            query = query.Join(_productCategoryRepository.GetAll(),
                    p => p.Id,
                    pc => pc.ProductId,
                    (p, pc) => new {p, pc.CategoryId})
                .Where(x => x.CategoryId == input.CategoryId.Value)
                .Select(x => x.p);
        }

        // Filter by tags
        if (!input.Tags.IsNullOrEmpty())
        {
            query = query.Join(_productTagRepository.GetAll(),
                    p => p.Id,
                    pt => pt.ProductId,
                    (p, pt) => new {p, pt.TagId})
                .Where(x => input.Tags.Contains(x.TagId))
                .Select(x => x.p);
        }

        return query;
    }
}