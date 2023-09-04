using OnlineEvent.Core.Entities;
using OnlineEvent.Model.EventModels;

namespace OnlineEvent.Service.Mapping
{
    public class EventMapping
    {
        public EventModel EventMap(Event value)
        {
            var ticketInfo = new TicketInfo();
            if (value.AreTickets)
            {
                ticketInfo.Id = value.Id;
                ticketInfo.TicketPrice = value.Ticket.TicketPrice;
                ticketInfo.WebSiteUrl = value.Ticket.WebSiteUrl;
            }
            else
            {
                ticketInfo = null;
            }
            var mapping = new EventModel()
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                EventDate = value.EventDate,
                ApplicationDeadLine = value.ApplicationDeadLine,
                Quota = value.Quota,
                ImageUrl = value.ImageUrl,
                AreTickets = value.AreTickets,
                CityName = value.City.CityName,
                CityId = value.CityId,
                CategoryName = value.Category.CategoryName,
                CategoryId = value.CategoryId,
                CreatorUserId = value.CreatorUserId,
                CreatorUserFullName = value.User.Name + " " + value.User.Surname,
                IsActive = value.IsActive,
                TicketInfo = ticketInfo

            };
            return mapping;
        }
    }
}
