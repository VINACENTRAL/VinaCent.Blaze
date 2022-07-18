using System.Linq;
using VinaCent.Blaze.BusinessCore;

namespace VinaCent.Blaze.EntityFrameworkCore.Seed.Host
{
    public class DefaultCurrencyUnitCreator
    {
        private readonly BlazeDbContext _context;

        public DefaultCurrencyUnitCreator(BlazeDbContext context)
        {
            _context = context;
        }

        public async void Create()
        {
            if (!_context.CurrencyUnits.Any())
            {
                await _context.CurrencyUnits.AddRangeAsync(CurrencyUnit.GetDefaults());
                _context.SaveChanges();
            }
        }
    }
}
