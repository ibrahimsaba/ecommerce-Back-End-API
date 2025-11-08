using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParameters productSpecParams) : base(p => (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId) && (!productSpecParams.TypeId.HasValue || p.TypeId == productSpecParams.TypeId))
        {
            
        }
    }
}
