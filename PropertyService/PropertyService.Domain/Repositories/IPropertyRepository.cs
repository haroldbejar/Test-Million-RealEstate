using PropertyService.Domain.Entities;
using PropertyService.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyService.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property> GetByIdAsync(string id);
        Task<PagedResult<Property>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Property property);
        Task<bool> UpdateAsync(Property property);
        Task<bool> DeleteAsync(string id);
        Task<PagedResult<Property>> SearchByParam(PropertySearchParams searchParams, int pageNumber, int pageSize);
    }
}

