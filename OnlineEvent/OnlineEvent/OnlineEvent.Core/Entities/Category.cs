namespace OnlineEvent.Core.Entities
{
    public class Category:BaseEntity
    {
        public Category()
        {
            this.IsActive = true;
        }
        public string CategoryName { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
