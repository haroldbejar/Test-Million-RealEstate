using OwnerService.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnerService.Application.Services
{
    public interface IOwnerApplicationService
    {
        Task<IEnumerable<OwnerDto>> GetAllAsync();
        Task<OwnerDto> GetByIdAsync(string id);
        Task<OwnerDto> CreateAsync(CreateOwnerDto createOwnerDto);
        Task UpdateAsync(string id, UpdateOwnerDto updateOwnerDto);
        Task DeleteAsync(string id);
    }
}
