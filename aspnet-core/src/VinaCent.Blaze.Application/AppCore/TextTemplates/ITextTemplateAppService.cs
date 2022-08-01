using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.TextTemplates.Dto;

namespace VinaCent.Blaze.AppCore.TextTemplates
{
    public interface ITextTemplateAppService : IAsyncCrudAppService<TextTemplateDto, Guid, PagedTextTemplateResultRequestDto, CreateTextTemplateDto, UpdateTextTemplateDto>
    {
        Task<PagedResultDto<TextTemplateListDto>> GetAllListAsync(PagedTextTemplateResultRequestDto input);
        Task TestTextTemplateAsync(TestTextTemplateDto input);
    }
}
