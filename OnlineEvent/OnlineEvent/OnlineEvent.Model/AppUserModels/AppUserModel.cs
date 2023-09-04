namespace OnlineEvent.Model.AppUserModels
{
    public class AppUserModel : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserType { get; set; } = null!;
        public string? InstitutionName { get; set; }
        public string? InstitutionWebSite { get; set; }
    }
}
