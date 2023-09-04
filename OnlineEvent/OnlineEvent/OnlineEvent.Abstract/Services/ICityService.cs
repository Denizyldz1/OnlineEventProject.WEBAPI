using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.CityModels;

namespace OnlineEvent.Abstract.Services
{
    public interface ICityService : IService<CityModel, City>
    {
        Task<CustomResponseModel<CityWithEventModel>> GetByIdWithEventAsync(int cityId);

    }
}
