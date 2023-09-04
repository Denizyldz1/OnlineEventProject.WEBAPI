using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;
namespace OnlineEvent.Data.Configurations 
{
    internal class AppUserEventConfiguration : IEntityTypeConfiguration<AppUserEvent>
    {
        public void Configure(EntityTypeBuilder<AppUserEvent> builder)
        {
            builder.HasKey(x => new { x.EventId, x.UserId });
            builder.HasOne(x => x.Event).WithMany(x => x.Users).HasForeignKey(x => x.EventId);
            builder.HasOne(x => x.User).WithMany(x => x.EventTables).HasForeignKey(x => x.UserId);
        }
    }
}
