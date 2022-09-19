using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using Microsoft.AspNetCore.Http;
using VinaCent.Blaze.BusinessCore.Shop.Common;
using VinaCent.Blaze.BusinessCore.Shop.ProductImages;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

[AutoMapTo(typeof(Product))]
public class CreateProductDto : EntityDto<long>
{
    /// <summary>
    /// The product title to be displayed on the Shop Page and Product Page.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Title)]
    public string Title { get; set; }

    /// <summary>
    /// The meta title to be used for browser title and SEO.
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MetaTitle)]
    public string MetaTitle { get; set; }

    /// <summary>
    /// The summary to mention the key highlights.
    /// </summary>
    [AppRequired]
    [MinLength(30)]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Summary)]
    public string Summary { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Price)]
    public decimal Price { get; set; }

    /// <summary>
    /// The discount on the product.
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Discount)]
    public decimal Discount { get; set; }

    /// <summary>
    /// It stores the date and time at which the product sale starts.
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.StartSellAt)]
    public DateTime? StartSellAt { get; set; }

    /// <summary>
    /// It stores the date and time at which the product sale ends.
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EndSellAt)]
    public DateTime? EndSellAt { get; set; }

    /// <summary>
    /// The column used to store the additional details of the product.
    /// </summary>
    [AppRequired]
    [MinLength(50)]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
    public string Content { get; set; }

    /// <summary>
    /// The column used to store private content, only visible when product was bought and processed
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.BuyerVisibleContent)]
    public string BuyerVisibleContent { get; set; }

    /// <summary>
    /// The column used to store private content only for Seller/Product Owner
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SellerVisibleContent)]
    public string SellerVisibleContent { get; set; }

    /// <summary>
    /// Column are used to store product visibility
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Visibility)]
    public VisibilityType Visibility { get; set; }

    /// <summary>
    /// Columns are used to store product status.
    /// </summary>
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Status)]
    public SubmitStatus Status { get; set; }

    /// <summary>
    /// Product feature image
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.FeatureImage)]
    public IFormFile FeatureImageFile { get; set; }

    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Product_Images)]
    public IFormFileCollection Images { get; set; }
    
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Product_Categories)]
    public int CategoryId { get; set; }

    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Product_Tags)]
    public string[] TagTitles { get; set; }
}