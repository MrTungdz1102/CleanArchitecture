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
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            villaRepo = new Repository<Villa>(_db);
            villaNumberRepo = new Repository<VillaNumber>(_db);
        }
    }
}
