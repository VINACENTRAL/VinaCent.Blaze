using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VinaCent.Blaze.BusinessCore.Shop.Products;

namespace VinaCent.Blaze.BusinessCore.Shop.CartItems
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(CartItems)}")]
    public class CartItem : AuditedEntity<Guid>
    {
        /// <summary>
        /// The product id to identify the parent product.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Number of product
        /// </summary>
        public int Quantity { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
