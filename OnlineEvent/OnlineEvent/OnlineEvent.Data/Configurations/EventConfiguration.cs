using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
             builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(500);

            // İlişkiler
            builder.HasOne(x=>x.City).WithMany(x=>x.Events).HasForeignKey(x=>x.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Category).WithMany(x=>x.Events).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.User).WithMany(x => x.Events).HasForeignKey(x=>x.CreatorUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
