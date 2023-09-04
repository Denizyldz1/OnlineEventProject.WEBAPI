using OnlineEvent.Model.AppUserEventModels;
using OnlineEvent.Model.TicketModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.EventModels
{
    public class UpdateEventModel
    {
        // Güncelleme işlemi yapıldıktan sonra tekrardan Admin onayına gitmesi gerekiyor bu yüzden false olarak veri gönderiyorum.
        public UpdateEventModel()
        {
            this.IsActive = false;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Title alanı zorunludur")]
        [MaxLength(500, ErrorMessage = "Max 500 karakter girilebilir")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Description alanı zorunludur")]

        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime ApplicationDeadLine { get; set; }
        public int Quota { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "AreTickets alanı zorunludur")]
        public bool AreTickets { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public int CreatorUserId { get; set; }
        public bool IsActive { get; set; }
        public TicketInfoModel? Ticket { get; set; }
        //public AppUserEventModel? UserEvent { get; set; }
    }
}
