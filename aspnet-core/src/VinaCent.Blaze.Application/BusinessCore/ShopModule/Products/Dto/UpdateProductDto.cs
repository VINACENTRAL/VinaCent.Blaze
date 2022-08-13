using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using VinaCent.Blaze.BusinessCore.Shop.Common;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

public class UpdateProductDto: EntityDto<long>, IPassivable
{
    /// <summary>
    /// The product title to be displayed on the Shop Page and Product Page.
    /// </summary>
    public string Title { get; set; }

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
    /// Columns are used to store product states.
    /// </summary>
    public CensorshipStatus Status { get; set; }
}