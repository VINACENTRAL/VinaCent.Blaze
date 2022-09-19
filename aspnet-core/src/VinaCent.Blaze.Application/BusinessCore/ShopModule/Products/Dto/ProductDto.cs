using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using VinaCent.Blaze.BusinessCore.Shop.Common;
using VinaCent.Blaze.BusinessCore.Shop.ProductImages;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;
using VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

[AutoMapFrom(typeof(Product))]
public class ProductDto : AuditedEntityDto<long>, IPassivable
{
    /// <summary>
    /// The product title to be displayed on the Shop Page and Product Page.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Store uppercase title, reduce time standardize string
    /// </summary>
    public string NormalizedTitle { get; set; }

    /// <summary>
    /// The meta title to be used for browser title and SEO.
    /// </summary>
    public string MetaTitle { get; set; }

    /// <summary>
    /// The slug to form the URL.
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// The summary to mention the key highlights.
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The discount on the product.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// It can be used to identify whether the product is publicly available for shopping.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// It stores the date and time at which the product sale starts.
    /// </summary>
    public DateTime? StartSellAt { get; set; }

    /// <summary>
    /// It stores the date and time at which the product sale ends.
    /// </summary>
    public DateTime? EndSellAt { get; set; }

    /// <summary>
    /// The column used to store the additional details of the product.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The column used to store private content, only visible when product was bought and processed
    /// </summary>
    public string BuyerVisibleContent { get; set; }

    /// <summary>
    /// The column used to store private content only for Seller/Product Owner
    /// </summary>
    public string SellerVisibleContent { get; set; }

    /// <summary>
    /// Product currency Symbol
    /// </summary>
    public string ISOCurrencySymbol { get; set; }

    /// <summary>
    /// Columns are used to store product states.
    /// </summary>
    public CensorshipStatus State { get; set; }

    /// <summary>
    /// Column are used to store product visibility
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Visibility)]
    public VisibilityType Visibility { get; set; }

    /// <summary>
    /// Columns are used to store product status.
    /// </summary>
    public SubmitStatus Status { get; set; }

    public virtual ICollection<CategoryDto> Categories { get; set; }
    public virtual ICollection<TagDto> Tags { get; set; }
    public virtual List<ProductImage> Images { get; set; }
}