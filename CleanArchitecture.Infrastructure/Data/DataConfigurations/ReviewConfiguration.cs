using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Content = "Rất vui được ở lại ! Các chủ nhà đã chào đón",
                Rating = 5,
                VillaId = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            },
            new Review
            {
                Id = 2,
                UserId = "6af96a07-d096-46a8-aec8-9aa8566617bd",
                Content = "Thanks for serving our!",
                Rating = 4,
                VillaId = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            });
        }
    }
}
