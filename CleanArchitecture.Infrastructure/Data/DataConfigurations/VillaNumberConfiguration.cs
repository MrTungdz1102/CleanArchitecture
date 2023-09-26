using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.DataConfigurations
{
    public class VillaNumberConfiguration : IEntityTypeConfiguration<VillaNumber>
    {
        public void Configure(EntityTypeBuilder<VillaNumber> builder)
        {
            builder.HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 1
                },
                 new VillaNumber
                 {
                     Villa_Number = 102,
                     VillaId = 1
                 },
                  new VillaNumber
                  {
                      Villa_Number = 103,
                      VillaId = 1
                  },
                   new VillaNumber
                   {
                       Villa_Number = 104,
                       VillaId = 2
                   },
                    new VillaNumber
                    {
                        Villa_Number = 105,
                        VillaId = 2
                    },
                     new VillaNumber
                     {
                         Villa_Number = 106,
                         VillaId = 2
                     },
                      new VillaNumber
                      {
                          Villa_Number = 107,
                          VillaId = 3
                      }, new VillaNumber
                      {
                          Villa_Number = 108,
                          VillaId = 3
                      },
                       new VillaNumber
                       {
                           Villa_Number = 109,
                           VillaId = 3
                       },
                        new VillaNumber
                        {
                            Villa_Number = 110,
                            VillaId = 1
                        }
                );
        }
    }
}
