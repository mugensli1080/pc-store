using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace pc_store.Core
{
    public interface IPhotoStorage
    {
        Task<(string photoName, string thumbnailName)> StorePhoto(IFormFile file, string uploadFolderPath);
        void DeleteFile(string photoName);
    }

}