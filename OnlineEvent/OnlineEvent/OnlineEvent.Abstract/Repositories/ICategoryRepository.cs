using OnlineEvent.Core.Entities;

namespace OnlineEvent.Abstract.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByIdWithEventsAsync(int categoryId);

    }
}
