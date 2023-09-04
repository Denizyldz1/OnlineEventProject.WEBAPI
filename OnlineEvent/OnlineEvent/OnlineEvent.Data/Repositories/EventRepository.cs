using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;

namespace OnlineEvent.Data.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }
        public async override Task<Event> GetByIdAsync(int id)
        {
            var value = await _context.Events
                       .Include(x => x.City)
                       .Include(x => x.Category)
                       .Include(x => x.User)
                       .Include(x=>x.Ticket)
                       .Where(x => x.Id == id)
                       .SingleOrDefaultAsync();
            return value;
        }
        public override IQueryable<Event> GetAll()
        {
            var values = _context.Events
                 .Include(x => x.City)
                 .Include(x => x.Category)
                 .Include(x => x.User)
                 .Include(x => x.Ticket)
                 .AsNoTracking()
                 .AsQueryable();
            return values;
        }

        public async Task UpdateTicketInfo(TicketInfo ticket)
        {
            var value = await _context.TicketInfos.FindAsync(ticket.Id);
            value.TicketPrice = ticket.TicketPrice;
            value.WebSiteUrl = ticket.WebSiteUrl;
        }

        public async Task JoinEventAsync(AppUserEvent appUserEvent)
        {
            await _context.AppUserEvents.AddAsync(appUserEvent);
        }

        public async Task<IEnumerable<AppUserEvent>> AttendedEvents(int userId)
        {
             return await _context.AppUserEvents
                .Include(x=>x.Event)
                .ThenInclude(x=>x.Category)
                .Include(x=>x.Event)
                .ThenInclude(x=>x.City)
                .Include(x=>x.Event)
                .ThenInclude(x=>x.Ticket)
                .Include (x=>x.Event)
                .ThenInclude(x=>x.User)
                .Where(x=>x.UserId==userId).ToListAsync();
        }

        public async Task<bool> IsCreatorAsync(int userId, int eventId)
        {
            var value = await _context.AppUserEvents.Where(x=>x.UserId == userId && x.EventId==eventId).FirstOrDefaultAsync();
            if(value == null) return false;
            if(value.EventUserType == AppUserEventTypes.Creator) return true;
            return false;
        }

        public async Task<IEnumerable<AppUserEvent>> JoinedEventList()
        {
            return await _context.AppUserEvents.Include(x => x.Event).Include(x => x.User).ToListAsync();

        }

        public async Task<IEnumerable<AppUserEvent>> JoinedEventList(int eventId)
        {
            return await _context.AppUserEvents.Include(x => x.Event).Include(x => x.User).Where(x=>x.EventId==eventId).ToListAsync();
        }
    }
}
