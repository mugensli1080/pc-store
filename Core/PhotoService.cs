using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoStorage _photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this._photoStorage = photoStorage;
            this._unitOfWork = unitOfWork;

        }

        public async Task<object> UploadPhoto(Product product, IFormFile file, string uploadFolderPath, PhotoType photoType)
        {

            var fileNames = await _photoStorage.StorePhoto(file, uploadFolderPath);
            object photo = null;

            switch (photoType)
            {
                case PhotoType.Product:
                    photo = new ProductPhoto { Name = fileNames.photoName, Thumbnail = fileNames.thumbnailName, Created = DateTime.Now, Modified = DateTime.Now };
                    product.ProductPhotos.Add((ProductPhoto)photo);
                    break;
                case PhotoType.Specification:
                    photo = new SpecificationPhoto { Name = fileNames.photoName, Thumbnail = fileNames.thumbnailName, Created = DateTime.Now, Modified = DateTime.Now };
                    product.SpecificationPhotos.Add((SpecificationPhoto)photo);
                    break;
            }

            await _unitOfWork.CompleteAsync();

            return photo;
        }

        public async Task<ProductPhoto> ActivatePhoto(Product product, int productPhotoId)
        {
            var photo = product.ProductPhotos.SingleOrDefault(p => p.Id == productPhotoId);
            if (photo == null) return photo;

            product.ProductPhotos.Select(p => { p.Activated = (p.Id == productPhotoId); p.Modified = DateTime.Now; return p; }).ToList();

            await _unitOfWork.CompleteAsync();

            return photo;
        }

        public void DeleteProductPhoto(ProductPhoto productPhoto, string uploadFolderPath)
        {
            var photoPath = Path.Combine(uploadFolderPath, productPhoto.Name);
            var thumbnailPath = Path.Combine(uploadFolderPath, productPhoto.Thumbnail);
            _photoStorage.DeleteFile(photoPath);
            _photoStorage.DeleteFile(thumbnailPath);
        }

        public void DeleteSpecificationPhoto(SpecificationPhoto specificationPhoto, string uploadFolderPath)
        {
            var specificationPhotoPath = Path.Combine(uploadFolderPath, specificationPhoto.Name);
            var thumbnailPath = Path.Combine(uploadFolderPath, specificationPhoto.Thumbnail);
            _photoStorage.DeleteFile(specificationPhotoPath);
            _photoStorage.DeleteFile(thumbnailPath);
        }
    }
}