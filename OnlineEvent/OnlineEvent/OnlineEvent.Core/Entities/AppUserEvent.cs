using OnlineEvent.Core.EntityConst;

namespace OnlineEvent.Core.Entities
{
    public class AppUserEvent
    {
        public AppUserEvent()
        {
            this.eventUserType = EventUserType;
        }
        public int UserId { get; set; }
        public int EventId { get; set; }
        // EventUserTypes verileri için sadece 2 string ifadeyi kabul ettiğimi belirttim

        // User Types verileri için sadece 2 string ifadeyi kabul ettiğimi belirttim
        private string eventUserType;

        public string EventUserType
        {
            get { return eventUserType; }
            set
            {
                if (value == AppUserEventTypes.Participant || value == AppUserEventTypes.Creator)
                {
                    eventUserType = value;
                }
                else
                {
                    throw new ArgumentException("EventUserType geçersiz 'Participant' veya 'Creator' değerleri girilmeli");
                }
            }
        }
        public AppUser? User { get; set; }
        public Event? Event { get; set; }
    }
}
