using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("/api/categories/{categoryId}/subcategories")]
    public class SubCategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubCategoryRepository _repository;
        public SubCategoriesController(ISubCategoryRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _repository.GetSubCategories(categoryId);
            return Ok(_mapper.Map<IEnumerable<SubCategory>, IEnumerable<SubCategoryResource>>(subCategories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            var subCategory = await _repository.GetSubCategory(id);
            if (subCategory == null) return NotFound();

            return Ok(_mapper.Map<SubCategory, SubCategoryResource>(subCategory));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryResource subCategoryResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var subCategory = _mapper.Map<SubCategoryResource, SubCategory>(subCategoryResource);
            subCategory.Created = DateTime.Now;
            subCategory.Modified = DateTime.Now;

            _repository.Add(subCategory);
            await _unitOfWork.CompleteAsync();

            var result = await _repository.GetSubCategory(subCategory.Id);

            return Ok(_mapper.Map<SubCategory, SubCategoryResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] SubCategoryResource subCategoryResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var subCategory = await _repository.GetSubCategory(id);
            if (subCategory == null) return NotFound();

            _mapper.Map<SubCategoryResource, SubCategory>(subCategoryResource, subCategory);
            subCategory.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<SubCategory, SubCategoryResource>(subCategory);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var subCategory = await _repository.GetSubCategory(id);
            if (subCategory == null) return NotFound();

            _repository.Remove(subCategory);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}