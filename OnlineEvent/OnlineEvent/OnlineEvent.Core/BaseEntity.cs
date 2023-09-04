namespace OnlineEvent.Core
{
    public abstract class BaseEntity :IEntity
    {
        public BaseEntity()
        {
            this.CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
