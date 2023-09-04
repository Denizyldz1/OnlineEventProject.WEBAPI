using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;

namespace OnlineEvent.Data.Seeds
{
    internal class EventSeed : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Bir Ticket'lı etkinlik ekliyorum
            builder.HasData(new Event()
            {
                Id=1,
                Title = "İlk etkinliğim",
                Description= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                EventDate = new DateTime(2024, 1, 1),
                ApplicationDeadLine = new DateTime(2023 ,12,12),
                Quota = 50,
                ImageUrl = null,
                AreTickets = true,
                CityId = 34,
                CategoryId = 1,
                CreatorUserId = 1
            });
        }
    }
}
