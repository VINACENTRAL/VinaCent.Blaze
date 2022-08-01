using System.Linq;
using VinaCent.Blaze.AppCore.TextTemplates;

namespace VinaCent.Blaze.EntityFrameworkCore.Seed.Host
{
    public class DefaultTextTemplatesCreator
    {
        private readonly BlazeDbContext _context;

        public DefaultTextTemplatesCreator(BlazeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            foreach (var tt in TextTemplate.Defaults)
            {
                if (!_context.TextTemplates.Any(x => x.Id == tt.Id))
                {
                    _context.TextTemplates.Add(tt);
                    _context.SaveChanges();
                }
            }
        }
    }
}
