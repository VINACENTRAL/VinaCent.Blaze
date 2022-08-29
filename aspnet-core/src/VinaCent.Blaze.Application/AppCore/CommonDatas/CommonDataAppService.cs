using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas.Dto;

namespace VinaCent.Blaze.AppCore.CommonDatas
{
    public class CommonDataAppService : 
        AsyncCrudAppService<CommonData, CommonDataDto, Guid, PagedCommonDataResultRequestDto, CreateCommonDataDto, UpdateCommonDataDto>, 
        ICommonDataAppService
    {
        public CommonDataAppService(IRepository<CommonData, Guid> repository) : base(repository)
        {

        }

        [UnitOfWork]
        protected override IQueryable<CommonData> CreateFilteredQuery(PagedCommonDataResultRequestDto input)
        {
            var query = Repository.GetAll();
            query = query.WhereIf(!input.Type.IsNullOrWhiteSpace(), x => x.Type == input.Type);

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

        public async Task<List<T>> GetListType<T>(string type) where T : EntityDto<Guid>
        {
            var items = await Repository.GetAllListAsync(x => x.Type == type);
            return items.Select(x => JsonConvert.DeserializeObject<T>(x.Value)).ToList();
        }

        public async Task<List<T>> SaveListType<T>(string type, params T[] inputs) where T : EntityDto<Guid>
        {
            foreach (var input in inputs)
            {
                if (Repository.GetAll().Any(x => x.Key == input.Id.ToString()))
                {
                    await UpdateType(input);
                }
                else
                {
                    await CreateType(type, input);
                }
            }

            return inputs.ToList();
        }

        public async Task<List<T>> SaveAndRemoveOldListType<T>(string type, params T[] inputs) where T : EntityDto<Guid>
        {
            await Repository.DeleteAsync(x => x.Type == type);
            return await SaveListType(type, inputs);
        }

        public async Task<T> SaveType<T>(string type, T input) where T : EntityDto<Guid>
        {
            if (Repository.GetAll().Any(x => x.Key == input.Id.ToString()))
            {
                await UpdateType(input);
            }
            else
            {
                await CreateType(type, input);
            }

            return input;
        }

        public async Task<T> UpdateType<T>(T input) where T : EntityDto<Guid>
        {
            var item = await Repository.FirstOrDefaultAsync(x => x.Key == input.Id.ToString());
            item.Value = JsonConvert.SerializeObject(input);
            await Repository.UpdateAsync(item);
            return input;
        }

        public async Task<T> CreateType<T>(string type, T input) where T : EntityDto<Guid>
        {
            await Repository.InsertAsync(new CommonData
            {
                Key = input.Id.ToString(),
                Value = JsonConvert.SerializeObject(input),
                Type = type
            });
            return input;
        }

        public async Task<T> GetType<T>(string id) where T : EntityDto<Guid>
        {
            var item = await Repository.FirstOrDefaultAsync(x => x.Key == id);
            return JsonConvert.DeserializeObject<T>(item.Value);
        }

        public async Task DeleteType<T>(string id) where T : EntityDto<Guid>
        {
            await Repository.DeleteAsync(x => x.Key == id);
        }
    }
}
