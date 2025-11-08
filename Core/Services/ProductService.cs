using AutoMapper;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var res = mapper.Map<IEnumerable<BrandResultDto>>(brand);
            return res;
        }

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters productSpecParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(productSpecParams);

            var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var specCount = new ProductWithCountSpecifications(productSpecParams);
            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);
            var res = mapper.Map<IEnumerable<ProductResultDto>>(product);

            return new PaginationResponse<ProductResultDto>(productSpecParams.PageIndex,productSpecParams.PageSize, 0, res);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var type = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var res = mapper.Map<IEnumerable<TypeResultDto>>(type);
            return res;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var product = await  unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) throw new ProductNotFoundException(id);
            var res = mapper.Map<ProductResultDto>(product);
            return res;
        }
    }
}
