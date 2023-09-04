namespace OnlineEvent.Core.Configurations
{
    // Bu nesne entity ve Dto olmadığı için Config içerisine yazıldı
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        // Hangi API erişebilmesi için kullanıldı
        public List<String> Audiences { get; set; }
    }
}
