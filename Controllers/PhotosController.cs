using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using pc_store.Controllers.Resources;
using pc_store.Core;
using pc_store.Core.Models;
using pc_store.Persistence;

namespace pc_store.Controllers
{
    [Route("/api/products/{productId}/photos")]
    public class PhotosController : Controller
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IHostingEnvironment _host;
        private readonly IPhotoService _photoService;

        public PhotosController(
            PcStoreDbContext context,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IHostingEnvironment host,
            IOptionsSnapshot<PhotoSettings> options,
            IPhotoService photoService)
        {
            this._photoService = photoService;
            this._host = host;
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._photoSettings = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product");

            var productPhotoResources = _mapper.Map<IEnumerable<ProductPhoto>, IEnumerable<ProductPhotoResource>>(product.ProductPhotos);
            var specificationPhotoResources = _mapper.Map<IEnumerable<SpecificationPhoto>, IEnumerable<SpecificationPhotoResource>>(product.SpecificationPhotos);

            var result = new Dictionary<string, IEnumerable<Object>> {
                { "product", productPhotoResources },
                { "specification", specificationPhotoResources}
            };

            return Ok(result);
        }

        //============================= PRODUCT PHOTOS
        [Route("product")]
        [HttpGet]
        public async Task<IActionResult> GetProductPhotos(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product.");
            return Ok(_mapper.Map<IEnumerable<ProductPhoto>, IEnumerable<ProductPhotoResource>>(product.ProductPhotos));
        }

        [HttpDelete("product/{id}")]
        public async Task<IActionResult> DeleteProductPhoto(int productId, int id)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product");

            var photo = product.ProductPhotos.SingleOrDefault(p => p.Id == id);
            if (photo == null) return BadRequest("Can't find photo");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.ProductPhotosPath);
            _photoService.DeleteProductPhoto(photo, uploadFolderPath);
            product.ProductPhotos.Remove(photo);

            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [Route("product")]
        [HttpPost]
        public async Task<IActionResult> UploadProductPhoto(int productId, IFormFile file)
        {
            var product = await _productRepository.GetProduct(productId, includeRelated: false);
            if (product == null) return NotFound();

            var validated = _photoSettings.Validate(file);
            if (!validated.IsValid) return BadRequest(validated.Response);

            var uploadFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.ProductPhotosPath);
            var photo = await _photoService.UploadPhoto(product, file, uploadFolderPath, PhotoType.Product);

            return Ok(_mapper.Map<ProductPhoto, ProductPhotoResource>((ProductPhoto)photo));
        }

        [HttpPut("product/{id}")]
        public async Task<IActionResult> ActivatePhoto(int productId, int id)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product");

            var photo = await _photoService.ActivatePhoto(product, id);
            if (photo == null) return BadRequest("Can't find photo.");

            return Ok(_mapper.Map<ProductPhoto, ProductPhotoResource>(photo));
        }

        //============================= SPECIFICATION PHOTOS
        [Route("specification")]
        [HttpGet]
        public async Task<IActionResult> GetSpecificationPhotos(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product");

            return Ok(_mapper.Map<IEnumerable<SpecificationPhoto>, IEnumerable<SpecificationPhotoResource>>(product.SpecificationPhotos));
        }

        [Route("specification")]
        [HttpPost]
        public async Task<IActionResult> UploadSpecificationPhotos(int productId, IFormFile file)
        {
            var product = await _productRepository.GetProduct(productId, includeRelated: false);
            if (product == null) return NotFound();

            var validated = _photoSettings.Validate(file);
            if (!validated.IsValid) return BadRequest(validated.Response);

            var uploadFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.SpecificationPhotosPath);
            var photo = await _photoService.UploadPhoto(product, file, uploadFolderPath, PhotoType.Specification);

            return Ok(_mapper.Map<SpecificationPhoto, SpecificationPhotoResource>((SpecificationPhoto)photo));
        }

        [HttpDelete("specification/{id}")]
        public async Task<IActionResult> DeleteSpecificationPhoto(int productId, int id)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null) return BadRequest("Can't find product");

            var photo = product.SpecificationPhotos.SingleOrDefault(p => p.Id == id);
            if (photo == null) return BadRequest("Can't find photo");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.SpecificationPhotosPath);
            _photoService.DeleteSpecificationPhoto(photo, uploadFolderPath);
            product.SpecificationPhotos.Remove(photo);

            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }

    }
}