using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Seeds
{
    internal class TicketInfoSeed : IEntityTypeConfiguration<TicketInfo>
    {
        public void Configure(EntityTypeBuilder<TicketInfo> builder)
        {
            builder.HasData(new TicketInfo()
            {
                Id = 1, // Eşleşen Event'e ait bir Id değeri olmalı
                TicketPrice = 50,
                WebSiteUrl = "www.google.com"
            });
        }
    }
}
