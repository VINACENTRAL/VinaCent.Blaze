using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using VinaCent.Blaze.BusinessCore.Shop.Categories;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto
{
    [AutoMapFrom(typeof(Category), typeof(CategoryTranslation))]
    public class CategoryListItemDto : EntityDto
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public int Level { get; set; }

        public List<CategoryListItemDto> Items { get; set; }
    }
}
