using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas.Dto;

namespace VinaCent.Blaze.AppCore.CommonDatas
{
    public class CommonDataAppService : BlazeAppServiceBase, ICommonDataAppService
    {
        private readonly IRepository<CommonData, Guid> _repository;

        public CommonDataAppService(IRepository<CommonData, Guid> repository)
        {
            _repository = repository;
        }

        [UnitOfWork]
        private IQueryable<CommonData> CreateFilteredQuery(PagedCommonDataResultRequestDto input)
        {
            var query = _repository.GetAll();
            query = query.Where(x => x.Type == input.Type);

            query = query.WhereIf(!input.ParentKey.IsNullOrWhiteSpace(),
                    x => x.ParentKey == input.ParentKey);

            query = query.WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                    x => x.Key.Contains(input.Keyword) ||
                         x.Value.Contains(input.Keyword) ||
                         x.Description.Contains(input.Keyword));
            return query;
        }

        public async Task<ListResultDto<CommonDataDto>> GetAllList(PagedCommonDataResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            var raw = await query.ToListAsync();
            return new ListResultDto<CommonDataDto>(
                ObjectMapper.Map<List<CommonDataDto>>(raw));
        }

    }
}
