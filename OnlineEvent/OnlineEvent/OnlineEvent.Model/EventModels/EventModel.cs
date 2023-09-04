using OnlineEvent.Core.Entities;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.CategoryModels;
using OnlineEvent.Model.CityModels;

namespace OnlineEvent.Model.EventModels
{
    public class EventModel : BaseModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime ApplicationDeadLine { get; set; }
        public int Quota { get; set; }
        public string? ImageUrl { get; set; }
        public bool AreTickets { get; set; }
        public string? CityName { get; set; }
        public int CityId { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int CreatorUserId { get; set; }
        public string? CreatorUserFullName { get; set; }
        public TicketInfo? TicketInfo { get; set; }

    }
}
