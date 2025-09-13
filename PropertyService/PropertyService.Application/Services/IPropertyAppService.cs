using PropertyService.Application.Dtos;
using PropertyService.Domain.Models;
using System.Threading.Tasks;

namespace PropertyService.Application.Services
{
    public interface IPropertyAppService
    {
        Task<PagedResultDto<PropertyDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PropertyDto> GetByIdAsync(string id);
        Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto);
        Task<bool> UpdateAsync(string id, UpdatePropertyDto updatePropertyDto);
        Task<bool> DeleteAsync(string id);
        Task<PagedResultDto<PropertyDto>> SearchByParams(
            PropertySearchParams searchParams,
            int pageNumber,
            int pageSize);
    }
}
