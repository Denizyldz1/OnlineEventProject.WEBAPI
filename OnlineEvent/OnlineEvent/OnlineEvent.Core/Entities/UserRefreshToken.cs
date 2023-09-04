using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Core.Entities
{
    public class UserRefreshToken : IEntity
    {
        public UserRefreshToken()
        {
            this.IsActive = true;
            this.CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
