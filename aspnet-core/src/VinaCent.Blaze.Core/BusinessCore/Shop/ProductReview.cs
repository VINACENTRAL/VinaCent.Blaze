using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using VinaCent.Blaze.BusinessCore.Shop.Common;

namespace VinaCent.Blaze.BusinessCore.Shop
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(ProductReview)}s")]
    public class ProductReview : AuditedEntity<Guid>
    {
        /// <summary>
        /// The product id to identify the parent product.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// The parent id to identify the parent review.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// The review title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The review rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The column used to store the review details.
        /// </summary>
        public string Content { get; set; }

        public CensorshipStatus Status { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual ProductReview Parent { get; set; }
    }
}
