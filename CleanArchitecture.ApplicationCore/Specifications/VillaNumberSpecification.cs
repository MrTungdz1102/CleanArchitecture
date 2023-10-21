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
    public class VillaNumberSpecification : Specification<VillaNumber>
    {
        public VillaNumberSpecification(QueryParameter queryParameters)
        {
            Query.Include(x => x.Villa).Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).OrderBy(x => x.VillaId);
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
