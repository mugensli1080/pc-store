using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public interface IPhotoService
    {
        Task<object> UploadPhoto(Product product, IFormFile file, string uploadFolderPath, PhotoType photoType);
        void DeleteProductPhoto(ProductPhoto productPhoto, string uploadFolderPath);
        void DeleteSpecificationPhoto(SpecificationPhoto specification, string uploadFolderPath);
        Task<ProductPhoto> ActivatePhoto(Product product, int productPhotoId);
    }
}