using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PropertyService.Domain.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task DeleteFileAsync(string fileName, string folderName);
    }
}