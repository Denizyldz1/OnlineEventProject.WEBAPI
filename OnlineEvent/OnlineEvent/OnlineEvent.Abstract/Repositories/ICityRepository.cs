using OnlineEvent.Core.Entities;

namespace OnlineEvent.Abstract.Repositories
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<City> GetByIdWithEventsAsync(int cityId);

    }
}
