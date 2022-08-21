using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using VinaCent.Blaze.BusinessCore.Shop.Products;

namespace VinaCent.Blaze.BusinessCore.Shop.ProductImages;

[Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(ProductImages)}")]
public class ProductImage : Entity<Guid>
{
    /// <summary>
    /// The category id to identify the category.
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Link to resource 
    /// </summary>
    public string Uri { get; set; }
    
    /// <summary>
    /// Is feature image. Use to show in home page
    /// </summary>
    public bool IsFeature { get; set; }
    
    [ForeignKey(nameof(ProductId))] public virtual Product Product { get; set; }
}