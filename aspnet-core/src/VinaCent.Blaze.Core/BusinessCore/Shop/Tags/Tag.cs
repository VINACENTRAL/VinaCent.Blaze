using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore.Shop.Tags
{
    [Table(nameof(BusinessCore) + $".{nameof(Shop)}.{nameof(Tags)}")]
    public class Tag : Entity<Guid>
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
    }
}
