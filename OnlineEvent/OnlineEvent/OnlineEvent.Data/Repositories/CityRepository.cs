using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<City> GetByIdWithEventsAsync(int cityId)
        {
            return await _context.Cities
                            .Include(x => x.Events)
                            .ThenInclude(x => x.Category)
                             .Include(x => x.Events)
                            .ThenInclude(x => x.Ticket)
                            .Include(x => x.Events)
                            .ThenInclude(x => x.User)
                             .Where(x => x.Id == cityId)
                             .SingleOrDefaultAsync();
        }
    }
}
