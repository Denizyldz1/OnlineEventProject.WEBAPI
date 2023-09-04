using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Configurations
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RefreshToken).IsRequired().HasMaxLength(200);
        }
    }
}
