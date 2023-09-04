using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineEvent.Core.Entities
{
    public class TicketInfo
    {
        [ForeignKey("Event")]
        public int Id { get; set; }
        public decimal TicketPrice { get; set; }
        public string? WebSiteUrl { get; set; }
        [JsonIgnore]
        public virtual Event? Event { get; set; }
    }
}
