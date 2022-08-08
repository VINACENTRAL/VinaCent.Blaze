using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.ProductCategories")]
    public class ProductCategory : Entity<Guid>
    {
        /// <summary>
        /// The product id to identify the parent product.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The category id to identify the category.
        /// </summary>
        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
