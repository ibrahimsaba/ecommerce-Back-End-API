using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet] //endpoint  /api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetalis))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetalis))]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProduct([FromQuery] ProductSpecificationsParameters productSpecParams)
        {
            var result = await servicesManager.ProductService.GetAllProductsAsync(productSpecParams);
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetalis))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetalis))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetalis))]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result = await servicesManager.ProductService.GetProductByIdAsync(id);  
            return Ok(result);
        }
        [HttpGet("brands")] // endpoint /api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetalis))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetalis))]
        public async Task<ActionResult<BrandResultDto>> GetAllBrands()
        {
            var result = await servicesManager.ProductService.GetAllBrandsAsync();
            if (result is null) 
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet("types")] // endpoint /api/products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetalis))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetalis))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {
            var result = await servicesManager.ProductService.GetAllTypesAsync();
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
