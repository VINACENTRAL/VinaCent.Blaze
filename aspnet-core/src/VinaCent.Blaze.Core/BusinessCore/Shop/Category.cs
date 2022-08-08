using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.Categories")]
    public class Category : AuditedEntity, IPassivable
    {
        /// <summary>
        /// The parent id to identify the parent category.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The category title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The meta title to be used for browser title and SEO.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// The category slug to form the URL.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// The column used to store the category details.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The column used to notify current category is visible or not
        /// </summary>
        public bool IsActive { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Category ParentCategory { get; set; }
    }
}
