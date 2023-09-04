namespace OnlineEvent.Core.Entities
{
    public class City:BaseEntity
    {
        public City()
        {
            this.IsActive = false;
        }
        public string CityName { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
