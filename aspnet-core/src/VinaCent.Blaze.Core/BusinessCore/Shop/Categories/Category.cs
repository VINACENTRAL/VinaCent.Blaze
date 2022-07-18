using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace VinaCent.Blaze.BusinessCore.Shop.Categories
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(Categories)}")]
    public class Category : AuditedEntity, IMultiLingualEntity<CategoryTranslation>, IPassivable
    {
        /// <summary>
        /// The parent id to identify the parent category.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// The category slug to form the URL.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// The column used to notify current category is visible or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Level of this category
        /// </summary>
        public int Level { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Category ParentCategory { get; set; }

        public ICollection<CategoryTranslation> Translations { get; set; }
    }
}
