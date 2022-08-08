﻿using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(ProductMeta)}s")]
    public class ProductMeta : Entity<Guid>
    {
        /// <summary>
        /// The product id to identify the parent product.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// The key identifying the meta.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The column used to store the product metadata.
        /// </summary>
        public string Content { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}