using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(ProductTag)}s")]
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
