using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pc_store.Controllers.Resources;
using pc_store.Core;
using pc_store.Core.Models;
using pc_store.Persistence;

namespace pc_store.Controllers
{
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _repository;
        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repository.GetCategories();
            return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _repository.GetCategory(id);
            return Ok(_mapper.Map<Category, CategoryResource>(category));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var category = _mapper.Map<CategoryResource, Category>(categoryResource);
            category.Created = DateTime.Now;
            category.Modified = DateTime.Now;

            _repository.Add(category);
            await _unitOfWork.CompleteAsync();

            var result = await _repository.GetCategory(category.Id);
            return Ok(_mapper.Map<Category, CategoryResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _repository.GetCategory(id);
            if (category == null) return NotFound();

            _mapper.Map<CategoryResource, Category>(categoryResource, category);
            category.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Category, CategoryResource>(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repository.GetCategory(id);
            if (category == null) return NotFound();
            _repository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }

}