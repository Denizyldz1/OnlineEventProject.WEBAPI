using AutoMapper;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserEventModels;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Model.TicketModels;
using OnlineEvent.Service.Mapping;

namespace OnlineEvent.Service.Services
{
    public class EventService : Service<EventModel, Event>, IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IEventRepository repository) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }


        public async Task<CustomResponseModel<EventModel>> AddAsync(CreateEventModel model)
        {
            var entity = new Event()
            {
                Title = model.Title,
                Description = model.Description,
                EventDate = model.EventDate,
                ApplicationDeadLine = model.ApplicationDeadLine,
                Quota = model.Quota,
                ImageUrl = model.ImageUrl,
                CityId = model.CityId,
                CategoryId = model.CategoryId,
                CreatorUserId = model.CreatorUserId,
                AreTickets = model.AreTickets,
            };
            if (model.AreTickets == true)
            {
                TicketInfo ticket = new TicketInfo()
                {
                    TicketPrice = model.Ticket.TicketPrice,
                    WebSiteUrl = model.Ticket.WebSiteUrl
                };
                entity.Ticket = ticket;
            };
            var userEventTable = new AppUserEvent()
            {
                EventId = entity.Id,
                UserId = model.CreatorUserId,
                EventUserType = AppUserEventTypes.Creator
            };
            entity.Users.Add(userEventTable);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            // Tekrardan GetById çağırma sebebim Include'ların tekrardan eklenerek dönmesi
            var value = await GetByIdAsync(entity.Id);
            return CustomResponseModel<EventModel>.Success(201, value.Data);

        }

        public async Task<CustomResponseModel<AppUserEventModel>> JoinEventAsync(AppUserEventModel model)
        {
            var mapping =  _mapper.Map<AppUserEvent>(model);
            await _repository.JoinEventAsync(mapping);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<AppUserEventModel>.Success(201, model);
        }

        public async Task<CustomResponseModel<List<EventModel>>> AttendedEventsAsync(int userId)
        {
            var user = await _repository.AttendedEvents(userId);
            EventMapping eventMapping = new EventMapping();
            var dtoEntities = new List<EventModel>();
            foreach (var item in user)
            {
                dtoEntities.Add(eventMapping.EventMap(item.Event));
            }
            return CustomResponseModel<List<EventModel>>.Success(200, dtoEntities);
        }

        public async Task<CustomResponseModel<NoContentModel>> RemoveAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            _repository.Remove(value);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }
        // Map'leme hatası update'de
        public async Task<CustomResponseModel<NoContentModel>> UpdateAsync(UpdateEventModel model)
        {
            var entity = new Event()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                EventDate = model.EventDate,
                ApplicationDeadLine = model.ApplicationDeadLine,
                Quota = model.Quota,
                ImageUrl = model.ImageUrl,
                AreTickets = model.AreTickets,
                CityId = model.CityId,
                CategoryId = model.CategoryId,
                CreatorUserId = model.CreatorUserId
            };
            if (model.AreTickets && model.Ticket!=null)
            {
                var ticket = new TicketInfo()
                {
                    Id = model.Id,
                    TicketPrice = model.Ticket.TicketPrice,
                    WebSiteUrl = model.Ticket.WebSiteUrl
                };
                entity.Ticket = ticket;
            }
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        public async Task<CustomResponseModel<NoContentModel>> UpdateTicketInfo(UpdateTicketInfoModel ticketModel)
        {
            var mapping = new TicketInfo()
            {
                Id = ticketModel.Id,
                TicketPrice = ticketModel.TicketPrice,
                WebSiteUrl =  ticketModel.WebSiteUrl
            };
            await _repository.UpdateTicketInfo(mapping);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        protected override EventModel MapToDto(Event value)
        {
            EventMapping mapping = new EventMapping();
            var eventDto = mapping.EventMap(value);
            return eventDto;
         }

        public async Task<bool> IsCreatorAsync(int userId, int eventId)
        {
            var value = await _repository.IsCreatorAsync(userId, eventId);
            return value;
        }

        public async Task<CustomResponseModel<List<AppUserEventListModel>>> JoinedEventListAsync()
        {
           var values = await _repository.JoinedEventList();
           var dtoList = new List<AppUserEventListModel>();
            foreach (var item in values)
            {
               var dto =  new AppUserEventListModel()
                {
                    EventId = item.EventId,
                    UserId = item.UserId,
                    EventUserType = item.EventUserType,
                    UserName = item.User.UserName,
                    EventTitle = item.Event.Title,
                    Quota = item.Event.Quota,
                };
                dtoList.Add(dto);
            }
            return CustomResponseModel<List<AppUserEventListModel>>.Success(200, dtoList);

        }

        public async Task<CustomResponseModel<List<AppUserEventListModel>>> JoinedEventListAsync(int eventId)
        {
            var values = await _repository.JoinedEventList(eventId);
            var dtoList = new List<AppUserEventListModel>();
            foreach (var item in values)
            {
                var dto = new AppUserEventListModel()
                {
                    EventId = item.EventId,
                    UserId = item.UserId,
                    EventUserType = item.EventUserType,
                    UserName = item.User.UserName,
                    EventTitle = item.Event.Title,
                    Quota = item.Event.Quota,
                };
                dtoList.Add(dto);
            }
            return CustomResponseModel<List<AppUserEventListModel>>.Success(200, dtoList);
        }
    }
}
