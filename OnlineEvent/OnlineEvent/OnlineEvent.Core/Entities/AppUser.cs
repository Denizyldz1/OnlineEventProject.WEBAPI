using Microsoft.AspNetCore.Identity;
using OnlineEvent.Core.EntityConst;

namespace OnlineEvent.Core.Entities
{
    public class AppUser : IdentityUser<int> , IEntity
    {
        // Id, UserName,Email , PhoneNumber vb prop'lar IdentityUser'da mevcut
        // Şifre içinde passwordhash kullanacağımız için bu props'ları yoruma aldım.
        //public string Password { get; set; } = null!;
        //public string ConfirmPassword { get; set; } = null!;
        public AppUser()
        {
            IsActive = true;
            userType = UserType;
            CreatedDate = DateTime.Now;
        }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // User Types verileri için sadece 2 string ifadeyi kabul ettiğimi belirttim
        private string userType;
        public string UserType
        {
            get { return userType; }
            set
            {
                if (value == UserTypes.Person || value == UserTypes.Institution || value == UserTypes.Admin)
                {
                    userType = value;
                }
                else
                {
                    throw new ArgumentException("UserType geçersiz 'Person' veya 'Institution' değerleri girilmeli.");
                }
            }
        }

        public string? InstitutionName { get; set; }
        public string? InstitutionWebSite { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual ICollection<AppUserEvent> EventTables { get; set; } = new List<AppUserEvent>();

    }
}
