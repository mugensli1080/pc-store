using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace pc_store.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
        public int ThumbnailSize { get; set; }
        public string ProductPhotosPath { get; set; }
        public string SpecificationPhotosPath { get; set; }
        public (bool IsValid, string Response) Validate(IFormFile file)
        {
            var response = "";
            var isValid = true;

            if (file == null) response = "Null file.";
            if (file?.Length == 0) response = "Empty file";
            if (file?.Length > MaxBytes) response = "Maximum file size exceeded.";
            if (!IsSupported(file.FileName)) response = "Invalid file type";

            if (!String.IsNullOrWhiteSpace(response)) isValid = false;

            return (isValid, response);
        }

        private bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}