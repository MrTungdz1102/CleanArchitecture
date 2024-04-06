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
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10OFF",
                DiscountAmount = 50,
                MinAmount = 200,
                StartingDate = DateTime.Now,
                EndingDate = DateTime.Now.AddDays(30)
            },
            new Coupon
            {
                CouponId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 100,
                MinAmount = 500,
                StartingDate = DateTime.Now,
                EndingDate = DateTime.Now.AddDays(10)
            } );
        }
    }
}
