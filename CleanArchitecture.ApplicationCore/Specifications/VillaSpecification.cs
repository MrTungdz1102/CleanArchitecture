using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class VillaSpecification : Specification<Villa>
    {
        public VillaSpecification(string userId)
        {
            Query.Where(x => x.UserId == userId);
        }

        public VillaSpecification(string? keyword, int? cityId, double? priceFrom, double? priceTo)
        {
            Query.Where(x => (keyword == null || x.Name.Contains(keyword))
                    && (cityId == null || x.CityId == cityId)
                    && (priceFrom == null || x.Price >= priceFrom)
                    && (priceTo == null || x.Price <= priceTo));
        }
    }
}
