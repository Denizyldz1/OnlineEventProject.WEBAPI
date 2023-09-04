using OnlineEvent.Abstract.Services;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserEventModels;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Model.TicketModels;
using System.Linq.Expressions;

namespace OnlineEvent.Abstract.Services
{
    public interface IEventService : IService<EventModel,Event>
    {
        Task<CustomResponseModel<EventModel>> AddAsync(CreateEventModel Model);
        Task<CustomResponseModel<NoContentModel>> UpdateAsync(UpdateEventModel Model);
        Task<CustomResponseModel<NoContentModel>> RemoveAsync(int id);
        Task<CustomResponseModel<NoContentModel>> UpdateTicketInfo(UpdateTicketInfoModel ticketModel);
        Task<CustomResponseModel<AppUserEventModel>> JoinEventAsync(AppUserEventModel model);
        Task<CustomResponseModel<List<EventModel>>> AttendedEventsAsync(int userId);
        Task<bool> IsCreatorAsync(int userId,int eventId);
        Task<CustomResponseModel<List<AppUserEventListModel>>> JoinedEventListAsync();
        Task<CustomResponseModel<List<AppUserEventListModel>>> JoinedEventListAsync(int eventId);




    }
}
    