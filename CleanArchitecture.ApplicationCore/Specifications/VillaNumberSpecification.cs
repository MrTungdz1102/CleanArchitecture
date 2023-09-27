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
            Query.Include(x => x.Villa).Skip(queryParameters.StartIndex).Take(queryParameters.PageSize);
        }
    }
}
