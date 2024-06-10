using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class VillaNumberSpecification : Specification<VillaNumber>
    {
        public VillaNumberSpecification(QueryParameter queryParameters, string? userId)
        {
            if (userId != null)
            {
                Query.Include(x => x.Villa).Where(x => x.Villa.UserId == userId).Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).OrderBy(x => x.VillaId);
            }
            else
            {
                Query.Include(x => x.Villa).Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).OrderBy(x => x.VillaId);
            }
        }

        public VillaNumberSpecification(int? villaNumberId = null)
        {
            if (villaNumberId.HasValue)
            {
                Query.Where(x => x.Villa_Number == villaNumberId);
            }
        }
        public VillaNumberSpecification(int villaId)
        {
            Query.Where(x => x.VillaId == villaId);
        }
        public VillaNumberSpecification(int villaId, List<int> availableVillaNumber)
        {
            Query.Where(x => x.VillaId == villaId && availableVillaNumber.Any(u => u == x.Villa_Number));
        }
    }
}
