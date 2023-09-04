using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;

namespace OnlineEvent.Data.Seeds
{
    internal class AppUserEventSeed : IEntityTypeConfiguration<AppUserEvent>
    {
        public void Configure(EntityTypeBuilder<AppUserEvent> builder)
        {
            builder.HasData(
                new AppUserEvent()
                {
                    EventId = 1,
                    UserId = 1,
                    EventUserType = AppUserEventTypes.Creator
                }
                );
        }
    }
}
