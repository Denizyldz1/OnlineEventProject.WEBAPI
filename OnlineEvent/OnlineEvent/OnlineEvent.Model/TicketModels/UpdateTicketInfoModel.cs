using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.TicketModels
{
    public class UpdateTicketInfoModel
    {
        public int Id { get; set; }
        public decimal TicketPrice { get; set; }
        [Required]
        public string? WebSiteUrl { get; set; }
    }
}
