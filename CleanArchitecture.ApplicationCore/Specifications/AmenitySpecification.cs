using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class AmenitySpecification : Specification<Amenity>
    {
        public AmenitySpecification(QueryParameter query, string? userId = null)
        {
            if (userId != null)
            {
                Query.Include(x => x.Villa).Where(x => x.Villa.UserId == userId).Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).OrderBy(x => x.VillaId);
            }
            else
            {
                Query.Include(x => x.Villa).Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).OrderBy(x => x.VillaId);
            }
        }
        public AmenitySpecification(int? villaId)
        {
            Query.Where(x => x.VillaId == villaId);
        }
    }
}
