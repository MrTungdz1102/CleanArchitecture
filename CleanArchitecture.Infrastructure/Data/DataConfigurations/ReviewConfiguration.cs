using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.DataConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(new Review
            {
                Id = 1,
                UserId = "3bb437a2-da65-4b7e-bf85-e24bc6052031",
                ReviewContent = "Rất vui được ở lại ! Các chủ nhà đã chào đón",
                UserName = "Tran My Linh",
                Rating = 5,
                VillaId = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            },
            new Review
            {
                Id = 2,
                UserId = "6af96a07-d096-46a8-aec8-9aa8566617bd",
                ReviewContent = "Thanks for serving our!",
                UserName = "John Wick",
                Rating = 4,
                VillaId = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            });
        }
    }
}
