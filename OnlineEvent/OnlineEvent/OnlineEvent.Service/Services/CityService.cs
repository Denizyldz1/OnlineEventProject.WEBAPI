using AutoMapper;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.CategoryModels;
using OnlineEvent.Model.CityModels;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Service.Mapping;

namespace OnlineEvent.Service.Services
{
    public class CityService : Service<CityModel, City>, ICityService
    {
        private readonly ICityRepository _repository;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, ICityRepository repository) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        public async Task<CustomResponseModel<CityWithEventModel>> GetByIdWithEventAsync(int cityId)
        {
            var city = await _repository.GetByIdWithEventsAsync(cityId);
            var eventList = city.Events.ToList();
            EventMapping eventMapping = new EventMapping();
            var dtoEntities = new List<EventModel>();
            eventList.ForEach(e => { dtoEntities.Add(eventMapping.EventMap(e)); });
            var mapping = new CityWithEventModel()
            {
                Id = cityId,
                CityName = city.CityName,
                IsActive = city.IsActive
            };
            mapping.Events = dtoEntities;
            return CustomResponseModel<CityWithEventModel>.Success(200, mapping);
        }

        protected override CityModel MapToDto(City entity)
        {
            return _mapper.Map<CityModel>(entity);
        }
    }
}
