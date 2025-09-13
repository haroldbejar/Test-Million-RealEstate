using OwnerService.Domain.Entities;
using OwnerService.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnerService.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<Owner> GetByIdAsync(string id);
        Task<Owner> GetByCodeAsync(OwnerCode code);
        Task AddAsync(Owner owner);
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(string id);
    }
}
