using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VinaCent.Blaze.BusinessCore.Shop.Products;
using VinaCent.Blaze.BusinessCore.Shop.Tags;

namespace VinaCent.Blaze.BusinessCore.Shop.ProductTags
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(ProductTags)}")]
    public class ProductTag : Entity<Guid>
    {
        public long ProductId { get; set; }
        public Guid TagId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; }

    }
}
