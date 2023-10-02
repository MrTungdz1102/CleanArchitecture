using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class AmenitySpecification : Specification<Amenity>
    {
        public AmenitySpecification(QueryParameter query)
        {
            Query.Include(x => x.Villa).Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).OrderBy(x => x.VillaId);
        }
    }
}
