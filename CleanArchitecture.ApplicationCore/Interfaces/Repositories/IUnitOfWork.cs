using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Villa> villaRepo { get; }
        IRepository<VillaNumber> villaNumberRepo { get; }
        IRepository<Amenity> amenityRepo { get; }
        IRepository<Booking> bookingRepo { get; }
        IRepository<City> cityRepo { get; }
        IRepository<Review> reviewRepo { get; }
    }
}
