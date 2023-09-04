using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;
using System.Reflection.Emit;

namespace OnlineEvent.Data.Configurations
{
    internal class TicketInfoConfiguration : IEntityTypeConfiguration<TicketInfo>
    {
        public void Configure(EntityTypeBuilder<TicketInfo> builder)
        {
            builder.Property(t => t.TicketPrice).HasColumnType("decimal(18,2)");
        }
    }
}
