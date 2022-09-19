using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.BusinessCore.Shop.ProductCategories;
using VinaCent.Blaze.BusinessCore.Shop.ProductImages;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.BusinessCore.Shop.ProductTags;
using VinaCent.Blaze.BusinessCore.Shop.Tags;
using VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products;

public class
    ShopProductAppService : AsyncCrudAppService<Product, ProductDto, long, FilterProductDto, CreateProductDto,
        UpdateProductDto>, IShopProductAppService
{
    private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
    private readonly IRepository<Tag, Guid> _tagRepository;
    private readonly IRepository<ProductTag, Guid> _productTagRepository;
    private readonly IRepository<ProductImage, Guid> _productImageRepository;
    private readonly IRepository<CurrencyUnit, Guid> _currencyUnitRepository;
    
    private readonly FileUnitManager _fileUnitManager;

    public ShopProductAppService(IRepository<Product, long> repository,
        IRepository<ProductCategory, Guid> productCategoryRepository,
        IRepository<ProductTag, Guid> productTagRepository, FileUnitManager fileUnitManager, IRepository<Tag, Guid> tagRepository, IRepository<ProductImage, Guid> productImageRepository, IRepository<CurrencyUnit, Guid> currencyUnitRepository) : base(repository)
    {
        _productCategoryRepository = productCategoryRepository;
        _productTagRepository = productTagRepository;
        _fileUnitManager = fileUnitManager;
        _tagRepository = tagRepository;
        _productImageRepository = productImageRepository;
        _currencyUnitRepository = currencyUnitRepository;
    }

    protected override Product MapToEntity(CreateProductDto createInput)
    {
        var defaultCurrencyUnit = _currencyUnitRepository.FirstOrDefault(x => x.IsDefault);
        
        var product = base.MapToEntity(createInput);
        product.Title = createInput.Title.Trim();
        product.NormalizedTitle = product.Title.ToUpper();
        product.Slug = product.Title.GenerateSlug();
        product.ISOCurrencySymbol = defaultCurrencyUnit.CurrencySymbol;
        
        return product;
    }

    public override async Task<ProductDto> CreateAsync([FromForm] CreateProductDto input)
    {
        var product = await base.CreateAsync(input);
        // Create category refs
        await _productCategoryRepository.InsertAsync(new ProductCategory
        {
            CategoryId = input.CategoryId,
            ProductId = product.Id
        });
        
        // Create tag refs
        foreach (var tagTitle in input.TagTitles)
        {
            var trimmed = tagTitle.Trim();
            var tag = await _tagRepository.FirstOrDefaultAsync(x => x.NormalizedTitle == trimmed.ToUpper()) ?? await _tagRepository.InsertAsync(new Tag
            {
                Title = trimmed,
                MetaTitle = trimmed,
                NormalizedTitle = trimmed.ToUpper(),
                Slug = trimmed.GenerateSlug()
            });
            await _productTagRepository.InsertAsync(new ProductTag
            {
                ProductId = product.Id,
                TagId = tag.Id
            });
        }

        await SaveProductImages(product.Id, input.FeatureImageFile, input.Images);
        
        // return result
        return product;
    }

    private async Task SaveProductImages(long productId, IFormFile feature, IFormFileCollection images)
    {
        // Uploaded file cached
        var uploadedFileCached = new List<FileUnit>();
        
        // Upload and create feature image
        var featureImage = await SaveImages(feature);
        if (featureImage == null || featureImage.FullName.IsNullOrWhiteSpace())
            throw new UserFriendlyException(L(LKConstants.ChangeAvatarFail));
        
        uploadedFileCached.Add(featureImage);
        
        await _productImageRepository.InsertAsync(new ProductImage
        {
            ProductId = productId,
            IsFeature = true,
            Uri = featureImage.FullName
        });

        // Upload and create image refs
        foreach (var image in images)
        {
            var uploadedFile = await SaveImages(image);
            if (uploadedFile == null || uploadedFile.FullName.IsNullOrWhiteSpace())
            {
                foreach (var cachedFile in uploadedFileCached)
                {
                    await _fileUnitManager.DeleteAsync(cachedFile.Id);
                }
                
                throw new UserFriendlyException(L(LKConstants.ChangeAvatarFail));
            }
            
            uploadedFileCached.Add(uploadedFile);
            
            await _productImageRepository.InsertAsync(new ProductImage
            {
                ProductId = productId,
                IsFeature = false,
                Uri = uploadedFile.FullName
            });
        }
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

    private async Task<FileUnit> SaveImages(IFormFile imageFile)
    {
        var userDirPictures = await _fileUnitManager.GetUserSelfPictureDir();

        return await _fileUnitManager.UploadFileAsync(new FileUnit
        {
            ParentId = userDirPictures.Id,
            Description = ""
        }, imageFile);
    }
}