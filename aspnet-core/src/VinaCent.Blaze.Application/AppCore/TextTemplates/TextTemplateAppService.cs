using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.TextTemplates.Dto;

namespace VinaCent.Blaze.AppCore.TextTemplates
{
    public class TextTemplateAppService : AsyncCrudAppService<TextTemplate, TextTemplateDto, Guid, PagedTextTemplateResultRequestDto, CreateTextTemplateDto, UpdateTextTemplateDto>,
        ITextTemplateAppService
    {
        public TextTemplateAppService(IRepository<TextTemplate, Guid> repository) : base(repository)
        {
        }

        public async Task<PagedResultDto<TextTemplateListDto>> GetAllListAsync(PagedTextTemplateResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query, input);
            var total = await query.CountAsync();
            query = ApplyPaging(query, input);
            var auditLogs = await query.ToListAsync();
            var dtoList = ObjectMapper.Map<List<TextTemplateListDto>>(auditLogs);
            return new PagedResultDto<TextTemplateListDto>(total, dtoList);
        }
    }
}
