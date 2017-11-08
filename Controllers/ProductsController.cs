using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pc_store.Controllers.Resources;
using pc_store.Core;
using pc_store.Core.Models;

namespace pc_store.Controllers
{
    [Route("/api/products")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null) return NotFound();

            return Ok(Mapper.Map<Product, ProductResource>(product));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] SaveProductResource saveProductResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = _mapper.Map<SaveProductResource, Product>(saveProductResource);
            product.Created = DateTime.Now;
            product.Modified = DateTime.Now;

            _repository.Add(product);
            await _unitOfWork.CompleteAsync();

            var result = await _repository.GetProduct(product.Id);
            return Ok(_mapper.Map<Product, ProductResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] SaveProductResource saveProductResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _repository.GetProduct(id, includeRelated: false);
            if (product == null) return NotFound();

            _mapper.Map<SaveProductResource, Product>(saveProductResource, product);
            product.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Product, ProductResource>(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetProduct(id, includeRelated: false);
            if (product == null) return NotFound();

            _repository.Remove(product);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}