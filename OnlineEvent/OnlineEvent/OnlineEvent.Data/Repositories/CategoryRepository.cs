using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Category?> GetByIdWithEventsAsync(int categoryId)
        {
            return await _context.Categories
                .Include(x=>x.Events)
                .ThenInclude(x=>x.City)
                 .Include(x => x.Events)
                .ThenInclude(x => x.Ticket)
                .Include(x => x.Events)
                .ThenInclude(x => x.User)
                 .Where(x => x.Id == categoryId)
                 .SingleOrDefaultAsync();
        }
    }
}
