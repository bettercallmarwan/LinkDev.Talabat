﻿using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] // Get: /api/products
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")] // GET: /api/products/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            return product is null ? NotFound(new { statusCode = 404, message = "not found" }) : Ok(product);
        }

    }
}
