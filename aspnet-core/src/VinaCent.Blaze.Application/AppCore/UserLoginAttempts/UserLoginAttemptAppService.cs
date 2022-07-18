using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.UserLoginAttempts.Dto;
using VinaCent.Blaze.Common.Extensions;

namespace VinaCent.Blaze.AppCore.UserLoginAttempts
{
    [AbpAuthorize]
    public class UserLoginAttemptAppService : BlazeAppServiceBase, IUserLoginAttemptAppService
    {
        private readonly IRepository<UserLoginAttempt, long> _repository;

        public UserLoginAttemptAppService(IRepository<UserLoginAttempt, long> repository)
        {
            _repository = repository;
        }

        private IQueryable<UserLoginAttempt> CreateFilteredQuery(PagedUserLoginAttemptResultRequestDto input)
        {
            var query = _repository.GetAll();
            if (!input.UserNameOrEmailAddress.IsNullOrWhiteSpace())
            {
                input.UserNameOrEmailAddress = input.UserNameOrEmailAddress.ToUpper();
                query = query.Where(x => x.UserNameOrEmailAddress.ToUpper() == input.UserNameOrEmailAddress);
            }

            query = query.WhereIf(input.TenantId is > 0, x => x.TenantId == input.TenantId);

            query = input.FilterByCreationTime(query);

            return query;
        }

        private IQueryable<UserLoginAttempt> ApplyPaging(IQueryable<UserLoginAttempt> query,
            PagedUserLoginAttemptResultRequestDto input)
        {
            return query.Skip(input.SkipCount)
                .Take(input.MaxResultCount);
        }

        private IQueryable<UserLoginAttempt> ApplySorting(IQueryable<UserLoginAttempt> query,
            PagedUserLoginAttemptResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        public async Task<PagedResultDto<UserLoginAttemptDto>> GetAllAsync(PagedUserLoginAttemptResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query, input);
            var total = await query.CountAsync();
            query = ApplyPaging(query, input);
            var result = ObjectMapper.Map<List<UserLoginAttemptDto>>(await query.ToListAsync());
            return new PagedResultDto<UserLoginAttemptDto>(total, result);
        }

        public async Task<UserLoginAttemptDto> GetAsync(long id)
        {
            var log = await _repository.GetAsync(id);
            return ObjectMapper.Map<UserLoginAttemptDto>(log);
        }
        
    }
}
