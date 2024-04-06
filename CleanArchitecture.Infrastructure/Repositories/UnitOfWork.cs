using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IRepository<Villa> villaRepo { get; private set; }
        public IRepository<VillaNumber> villaNumberRepo { get; private set; }
        public IRepository<Amenity> amenityRepo { get; private set; }
        public IRepository<Booking> bookingRepo { get; private set; }
        public IRepository<City> cityRepo { get; private set; }
        public IRepository<Review> reviewRepo { get; private set; }
        public IRepository<Coupon> couponRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            villaRepo = new Repository<Villa>(_db);
            villaNumberRepo = new Repository<VillaNumber>(_db);
            amenityRepo = new Repository<Amenity>(_db);
            bookingRepo = new Repository<Booking>(_db);
            cityRepo = new Repository<City>(_db);
            reviewRepo = new Repository<Review>(_db);
            couponRepo = new Repository<Coupon>(_db);
        }
    }
}
