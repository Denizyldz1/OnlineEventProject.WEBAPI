namespace OnlineEvent.Model.AppUserEventModels
{
    public class AppUserEventModel
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string EventUserType { get; set; } = null!;
    }
}
