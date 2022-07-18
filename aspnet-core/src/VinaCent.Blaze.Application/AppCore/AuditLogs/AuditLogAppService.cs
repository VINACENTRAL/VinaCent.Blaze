using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.AuditLogs.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Users.Dto;
using static System.DateTime;

namespace VinaCent.Blaze.AppCore.AuditLogs
{
    [AbpAuthorize(PermissionNames.Pages_AuditLogs)]
    public class AuditLogAppService : BlazeAppServiceBase, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _repository;
        private readonly IRepository<User, long> _userRepository;

        public AuditLogAppService(
            IRepository<AuditLog, long> repository, 
            IRepository<User, long> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        private IQueryable<AuditLog> CreateFilteredQuery(PagedAuditLogResultRequestDto input)
        {
            var query = _repository.GetAll();
            if (!input.UserName.IsNullOrWhiteSpace())
            {
                input.UserName = input.UserName.ToUpper();
                var users = _userRepository.GetAllList(x => x.UserName.ToLower().Contains(input.UserName));
                var userIds = users.Select(x => x.Id).ToArray();
                query = query.WhereIf(!string.IsNullOrEmpty(input.UserName), x => userIds.Contains(x.UserId.Value));
            }

            query = query.WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(),
                x => x.BrowserInfo.Contains(input.BrowserInfo));

            query = query.WhereIf(!input.Parameters.IsNullOrWhiteSpace(),
                x => x.Parameters.Contains(input.Parameters));

            query = query.WhereIf(input.MinExecutionDuration != null,
                x => x.ExecutionDuration >= input.MinExecutionDuration);

            query = query.WhereIf(input.MaxExecutionDuration != null,
                x => x.ExecutionDuration >= input.MaxExecutionDuration);

            query = query.WhereIf(!input.ServiceName.IsNullOrWhiteSpace(),
                x => x.ServiceName.Equals(input.ServiceName));

            query = query.WhereIf(!input.MethodName.IsNullOrWhiteSpace(),
                x => x.MethodName.Equals(input.MethodName));

            query = query.WhereIf(input.HasException != null,
                x => (x.Exception != null) == input.HasException.Value);

            query = query.WhereIf(!string.IsNullOrEmpty(input.ClientIpAddress), x => x.ClientIpAddress == input.ClientIpAddress);

            query = query.WhereIf(input.TenantId is > 0, x => x.TenantId == input.TenantId);

            if (!input.StartTime.IsNullOrWhiteSpace())
            {
                TryParse(input.StartTime, out DateTime startDate);
                query = query.Where(x => x.ExecutionTime >= startDate.Date);
            }

            if (!input.EndTime.IsNullOrWhiteSpace())
            {
                TryParse(input.EndTime, out DateTime endDate);
                endDate = endDate.Date.AddDays(1);
                query = query.Where(x => x.ExecutionTime < endDate);
            }

            return query;
        }

        private IQueryable<AuditLog> ApplyPaging(IQueryable<AuditLog> query,
            PagedAuditLogResultRequestDto input)
        {
            return query.Skip(input.SkipCount)
                .Take(input.MaxResultCount);
        }

        private IQueryable<AuditLog> ApplySorting(IQueryable<AuditLog> query,
            PagedAuditLogResultRequestDto input)
        {
            return query.OrderByDescending(x => x.ExecutionTime);
        }


        public async Task<PagedResultDto<AuditLogListDto>> GetAllAsync(PagedAuditLogResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query, input);
            var total = await query.CountAsync();
            query = ApplyPaging(query, input);
            var auditLogs = await query.ToListAsync();
            var dtoList = ObjectMapper.Map<List<AuditLogListDto>>(auditLogs);

            for (var i = 0; i < dtoList.Count; i++)
            {
                if (dtoList[i].UserId != null)
                {
                    try
                    {
                        var user = await _userRepository.GetAsync(dtoList[i].UserId.Value);
                        dtoList[i].UserName = user?.UserName;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                // ReSharper disable once InvertIf
                if (dtoList[i].ImpersonatorUserId != null)
                {
                    if (dtoList[i].UserId == dtoList[i].ImpersonatorUserId)
                    {
                        dtoList[i].ImpersonatorUserName = dtoList[i].UserName;
                    }
                    else
                    {
                        try
                        {
                            var user = await _userRepository.GetAsync(dtoList[i].ImpersonatorUserId.Value);
                            dtoList[i].ImpersonatorUserName = user.UserName;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }

                dtoList[i].HasException = !auditLogs[i].Exception.IsNullOrEmpty();
            }

            return new PagedResultDto<AuditLogListDto>(total, dtoList);
        }

        public async Task<AuditLogDto> GetAsync(long id)
        {
            var entity = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<AuditLogDto>(entity);
            if (dto.UserId != null)
            {
                var user = await _userRepository.GetAsync(dto.UserId.Value);
                dto.UserDto = ObjectMapper.Map<UserDto>(user);
            }

            // ReSharper disable once InvertIf
            if (dto.ImpersonatorUserId != null)
            {
                if (dto.UserId == dto.ImpersonatorUserId)
                {
                    dto.ImpersonatorUserDto = dto.UserDto;
                }
                else
                {
                    var user = await _userRepository.GetAsync(dto.ImpersonatorUserId.Value);
                    dto.ImpersonatorUserDto = ObjectMapper.Map<UserDto>(user);
                }
            }

            return dto;
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
