using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.DataConfigurations
{
    public class VillaConfiguration : IEntityTypeConfiguration<Villa>
    {
        public void Configure(EntityTypeBuilder<Villa> builder)
        {
            builder.HasData(
                  new Villa
                  {
                      Id = 1,
                      Name = "Royal Villa",
                      Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                      ImageUrl = "https://placehold.co/600x400",
                      Occupancy = 4,
                      Price = 200,
                      SquareFeet = 550,
                      CityId = 1,
                      OwnerId = "ed4cdaa3-868e-43d6-b899-c54e5fdd76eb",
                      StartRating = 5
                  },
new Villa
{
    Id = 2,
    Name = "Premium Pool Villa",
    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
    ImageUrl = "https://placehold.co/600x401",
    Occupancy = 4,
    Price = 300,
    SquareFeet = 550,
    CityId = 1,
    OwnerId = "2b4020a4-ba31-4031-bc5b-22461c00e6f1",
    StartRating = 4.5
},
new Villa
{
    Id = 3,
    Name = "Luxury Pool Villa",
    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
    ImageUrl = "https://placehold.co/600x402",
    Occupancy = 4,
    Price = 400,
    SquareFeet = 750,
    CityId = 2,
    OwnerId = "2b4020a4-ba31-4031-bc5b-22461c00e6f1",
    StartRating = 4.9
}
                );
        }
    }
}
