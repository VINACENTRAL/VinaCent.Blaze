using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop.Categories
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(CategoryTranslation)}")]
    public class CategoryTranslation : Entity, IEntityTranslation<Category>
    {
        /// <summary>
        /// The category title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The meta title to be used for browser title and SEO.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// The column used to store the category details.
        /// </summary>
        public string Content { get; set; }

        public Category Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
