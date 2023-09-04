using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category()
            {
                Id = 1,
                CategoryName = "Müzikli Festival"
            }, new Category()
            {
                Id = 2,
                CategoryName = "Sahne"
            },
               new Category()
            {
                Id = 3,
                CategoryName = "Spor"
            }
            );
         }
    }
}
