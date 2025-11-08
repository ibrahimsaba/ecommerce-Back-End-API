
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P=> P.Id == id)
        {
            ApplyIncludes();
        }
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParameters productSpecParams) : base(p=> (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search.ToLower())) && (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId) && (!productSpecParams.TypeId.HasValue || p.TypeId == productSpecParams.TypeId))
        {
            ApplyIncludes();
            ApplySorting(productSpecParams.Sort);
            ApplyPagination(productSpecParams.PageIndex, productSpecParams.PageSize);
        }
        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
    }
}
