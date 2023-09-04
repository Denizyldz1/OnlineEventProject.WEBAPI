namespace OnlineEvent.Core.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
            this.IsActive = false;   
        }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime ApplicationDeadLine { get; set; }
        public int Quota { get; set; }
        public string? ImageUrl { get; set; }
        public bool AreTickets { get; set; }

        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public int CreatorUserId { get; set; }

        public virtual City? City { get; set; }
        public virtual Category? Category { get; set; }
        public virtual AppUser? User { get; set; }

        //One to one
        public virtual TicketInfo? Ticket { get; set; }

        //Many to many

        public virtual ICollection<AppUserEvent> Users { get; set; } = new List<AppUserEvent>();
    }
}
