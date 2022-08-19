﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Localization;
using VinaCent.Blaze.BusinessCore.Shop.Categories;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto
{
    [AutoMap(typeof(CategoryTranslation))]
    public class CategoryTranslationDto
    {
        public CategoryTranslationDto() { }

        public CategoryTranslationDto(string language)
        {
            Language = language;
        }

        /// <summary>
        /// The category title.
        /// </summary>
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Title)]
        public string Title { get; set; }

        /// <summary>
        /// The meta title to be used for browser title and SEO.
        /// </summary>
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MetaTitle)]
        public string MetaTitle { get; set; }

        /// <summary>
        /// The column used to store the category details.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageName)]
        public string Language { get; set; }
    }
}