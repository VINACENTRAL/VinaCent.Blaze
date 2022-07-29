using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using System.Linq;
using VinaCent.Blaze.Common.Interfaces;
using static System.DateTime;

namespace VinaCent.Blaze.Common.Extensions
{
    public static class HasFilterDateRangeExtension
    {
        public static IQueryable<T> FilterByCreationTime<T>(this IHasFilterDateRange input, IQueryable<T> query)
            where T : IHasCreationTime
        {
            if (!input.StartTime.IsNullOrWhiteSpace())
            {
                TryParse(input.StartTime, out var startDate);
                startDate = startDate.ToUniversalTime().Date;
                query = query.Where(x => x.CreationTime >= startDate);
            }

            if (!input.EndTime.IsNullOrWhiteSpace())
            {
                TryParse(input.EndTime, out var endDate);
                endDate = endDate.Date.AddDays(1);
                query = query.Where(x => x.CreationTime < endDate);
            }

            return query;
        }
    }
}
