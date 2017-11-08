using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using pc_store.Core.Models;
using SixLabors.ImageSharp;

namespace pc_store.Core
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        private readonly PhotoSettings _photoSettings;
        public FileSystemPhotoStorage(IOptionsSnapshot<PhotoSettings> options)
        {
            this._photoSettings = options.Value;
        }
        public async Task<(string photoName, string thumbnailName)> StorePhoto(IFormFile file, string uploadFolderPath)
        {
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileExtension = Path.GetExtension(file.FileName);

            var photoFile = CreateFile(fileExtension, uploadFolderPath);
            using (var stream = new FileStream(photoFile.filePath, FileMode.Create)) { await file.CopyToAsync(stream); }

            var thumbnailName = CreateThumbnail(photoFile.filePath, fileExtension, uploadFolderPath);

            return (photoFile.fileName, thumbnailName);
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        private string CreateThumbnail(string originImagePath, string fileExtension, string uploadFolderPath)
        {
            var thumbnailFile = CreateFile(fileExtension, uploadFolderPath);

            using (var image = Image.Load(originImagePath))
            {
                image.Mutate(x => x.Resize(_photoSettings.ThumbnailSize, _photoSettings.ThumbnailSize));
                image.Save(thumbnailFile.filePath);
            }

            return thumbnailFile.fileName;
        }

        private (string fileName, string filePath) CreateFile(string fileExtension, string uploadFolderPath)
        {
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadFolderPath, fileName);

            return (fileName, filePath);
        }
    }

}