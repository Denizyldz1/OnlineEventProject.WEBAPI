using OnlineEvent.Core.Entities;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Model;

namespace OnlineEvent.Abstract.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task UpdateTicketInfo(TicketInfo ticket);
        Task JoinEventAsync(AppUserEvent appUserEvent);
        Task<IEnumerable<AppUserEvent>> AttendedEvents(int userId);
        Task<bool> IsCreatorAsync(int userId,int eventId);
        Task<IEnumerable<AppUserEvent>> JoinedEventList();
        Task<IEnumerable<AppUserEvent>> JoinedEventList(int eventId);

    }
}
