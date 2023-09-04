namespace OnlineEvent.Model.AppUserEventModels
{
    public class AppUserEventListModel : AppUserEventModel
    {
        public string UserName { get; set; }
        public string EventTitle { get; set; }
        public int Quota { get; set; }
    }
}
