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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new City
            {
                Id = 1,
                Name = "Hà Nội",
                CountryName = "Việt Nam",
                PostOffice = "100000"
            },
            new City
            {
                Id = 2,
                Name = "Hải Phòng",
                CountryName = "Việt Nam",
                PostOffice = "01234"
            },
            new City
            {
                Id = 3,
                Name = "Hội An",
                CountryName = "Việt Nam",
                PostOffice = "51000"
            });
        }
    }
}
