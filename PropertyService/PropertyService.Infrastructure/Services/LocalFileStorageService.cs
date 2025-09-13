using Microsoft.AspNetCore.Http;
using PropertyService.Domain.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PropertyService.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;

        public LocalFileStorageService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_webRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/{folderName}/{uniqueFileName}"; // Return relative URL
        }

        public Task DeleteFileAsync(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName))
                return Task.CompletedTask;

            var filePath = Path.Combine(_webRootPath, folderName, Path.GetFileName(fileName));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
    }
}
