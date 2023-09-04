using Microsoft.AspNetCore.Identity;

namespace OnlineEvent.Core.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole()
        {
            CreatedDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; set; }
    }
}
