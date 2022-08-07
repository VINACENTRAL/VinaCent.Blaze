using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;
using VinaCent.Blaze.Sessions.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates
{
    [AbpAuthorize]
    public class CurrencyExchangeRateAppService : BlazeAppServiceBase, ICurrencyExchangeRateAppService
    {
        private readonly IRepository<CurrencyUnit, Guid> _currencyUnitRepository;
        private readonly IRepository<CurrencyExchangeRate, Guid> _repository;
        private readonly UserManager _userManager;


        public CurrencyExchangeRateAppService(IRepository<CurrencyExchangeRate, Guid> repository, IRepository<CurrencyUnit, Guid> currencyUnitRepository)
        {
            _repository = repository;
            _currencyUnitRepository = currencyUnitRepository;
        }

        private async Task<UserLoginInfoDto> GetCurrentUserAsync(long userId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());
            return ObjectMapper.Map<UserLoginInfoDto>(user);
        }

        public async Task<PagedResultDto<CurrencyExchangeRateDto>> GetAllHistoryAsync(PagedExchangeRateHistoryResultRequestDto input)
        {
            // Get default currency unit
            var defaultCurrencyUnit = ObjectMapper.Map<CurrencyUnitDto>(await _currencyUnitRepository.FirstOrDefaultAsync(x => x.IsDefault));

            // Current currency unit
            var currentCurrencyUnit = ObjectMapper.Map<CurrencyUnitDto>(await _currencyUnitRepository.GetAsync(input.CurrentCurrencyUnitId));

            // Get query base on default currency
            var exchangeRateQuery = _repository.GetAll().Where(x => x.ISOCurrencySymbolFrom == defaultCurrencyUnit.ISOCurrencySymbol || x.ISOCurrencySymbolTo == defaultCurrencyUnit.ISOCurrencySymbol);
            
            // Get query base on current currency
            exchangeRateQuery = _repository.GetAll().Where(x => x.ISOCurrencySymbolFrom == currentCurrencyUnit.ISOCurrencySymbol || x.ISOCurrencySymbolTo == currentCurrencyUnit.ISOCurrencySymbol);
            
            //
            // Apply Sorting process - Order change rate query by creation time
            exchangeRateQuery = exchangeRateQuery.OrderByDescending(x => x.CreationTime);
            var total = exchangeRateQuery.Count();
            //
            // Apply paging process
            exchangeRateQuery = exchangeRateQuery.PageBy(input);
            //
            // Process return result
            var rawResult = await exchangeRateQuery.ToListAsync();

            var dataset = new List<CurrencyExchangeRateDto>();

            foreach (var item in rawResult)
            {
                var data = ObjectMapper.Map<CurrencyExchangeRateDto>(item);
                if (data.ISOCurrencySymbolFrom.Equals(defaultCurrencyUnit.ISOCurrencySymbol, StringComparison.OrdinalIgnoreCase))
                {
                    data.From = defaultCurrencyUnit;
                    data.To = currentCurrencyUnit;
                }
                else
                {
                    data.From = currentCurrencyUnit;
                    data.To = defaultCurrencyUnit;
                }
                if (data.CreatorUserId != null)
                {
                    data.Creator = await GetCurrentUserAsync(data.CreatorUserId.Value);
                }

                dataset.Add(data);
            }

            return new PagedResultDto<CurrencyExchangeRateDto>(total, dataset);
        }

        public async Task<PagedResultDto<CurrencyExchangeRateListDto>> GetAllListAsync(PagedCurrencyExchangeRateResultRequestDto input)
        {
            var query = _currencyUnitRepository.GetAll();
            // Get default currency unit
            var defaultCurrencyUnit = await _currencyUnitRepository.FirstOrDefaultAsync(x => x.IsDefault);
            // Filter process
            query = query.Where(x => x.Id != defaultCurrencyUnit.Id).WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.CurrencyNativeName.Contains(input.Keyword) || x.CurrencyEnglishName.Contains(input.Keyword));
            // Apply Sorting process
            query = query.OrderByDescending(x => x.IsDefault).OrderByDescending(x => x.IsActive).ThenBy(x => x.CurrencyNativeName);
            var total = query.Count();
            // Apply paging process
            query = query.PageBy(input);
            // Process return result
            var rawResult = await query.ToListAsync();

            var dataset = new List<CurrencyExchangeRateListDto>();

            // Get query base on default currency
            var exchangeRateQuery = _repository.GetAll().Where(x => x.ISOCurrencySymbolFrom == defaultCurrencyUnit.ISOCurrencySymbol || x.ISOCurrencySymbolTo == defaultCurrencyUnit.ISOCurrencySymbol);

            foreach (var item in rawResult)
            {
                var data = ObjectMapper.Map<CurrencyExchangeRateListDto>(item);
                data.Default = ObjectMapper.Map<CurrencyUnitDto>(defaultCurrencyUnit);

                // Load lastest exchange rate value
                var crrExchangeRate = await exchangeRateQuery.Where(x => x.ISOCurrencySymbolFrom == item.ISOCurrencySymbol || x.ISOCurrencySymbolTo == item.ISOCurrencySymbol).OrderByDescending(x => x.CreationTime).FirstOrDefaultAsync();
                if (crrExchangeRate != null)
                {
                    if (crrExchangeRate.ISOCurrencySymbolFrom == defaultCurrencyUnit.ISOCurrencySymbol)
                    {
                        // Ex:
                        // Crruent: VND - Default: USD
                        // Current was not Default currency.
                        // Now value will be calculate by: 1 USD = 23.000 VND 
                        data.ValueBaseOnOneDefault = crrExchangeRate.ConvertedValue;
                        data.ValueBaseOnOneCurrent = crrExchangeRate.ConvertedValue == 0 ? 0 : (1 / crrExchangeRate.ConvertedValue);
                    }
                    else
                    {
                        // Ex:
                        // Crruent: VND - Default: USD
                        // Current was not Default currency.
                        // Now value will be calculate by: 1 VND = 0.000043 USD 
                        data.ValueBaseOnOneCurrent = crrExchangeRate.ConvertedValue;
                        data.ValueBaseOnOneDefault = crrExchangeRate.ConvertedValue == 0 ? 0 : (1 / crrExchangeRate.ConvertedValue);
                    }
                }
                data.CreationTime = crrExchangeRate?.CreationTime ?? null;
                dataset.Add(data);
            }

            return new PagedResultDto<CurrencyExchangeRateListDto>(total, dataset);
        }

        public async Task<CurrencyExchangeRate> UpdateExchangeRateAsync(UpdateExchangeRateDto input)
        {
            var query = _currencyUnitRepository.GetAll();
            // Get default currency unit
            var defaultCurrencyUnit = await _currencyUnitRepository.FirstOrDefaultAsync(x => x.IsDefault);
            // Get current currency unit
            var currentCurrencyUnit = await _currencyUnitRepository.GetAsync(input.CurrencyUnitId);

            if (defaultCurrencyUnit.Id == currentCurrencyUnit.Id)
            {
                throw new UserFriendlyException("Can not update self exchange rate!");
            }

            var data = new CurrencyExchangeRate
            {
                ISOCurrencySymbolFrom = defaultCurrencyUnit.ISOCurrencySymbol,
                ISOCurrencySymbolTo = currentCurrencyUnit.ISOCurrencySymbol,
                ConvertedValue = input.ConvertedValue,
                IsActive = true
            };

            return await _repository.InsertAsync(data);
        }
    }
}
