using MongoDB.Driver;
using OwnerService.Domain.Entities;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.ValueObjects;
using OwnerService.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnerService.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly OwnerDbContext _context;

        public OwnerRepository(OwnerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.Find(owner => true).ToListAsync();
        }

        public async Task<Owner> GetByIdAsync(string id)
        {
            return await _context.Owners.Find(owner => owner.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Owner> GetByCodeAsync(OwnerCode code)
        {
            return await _context.Owners.Find(owner => owner.OwnerCode == code).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Owner owner)
        {
            await _context.Owners.InsertOneAsync(owner);
        }

        public async Task UpdateAsync(Owner owner)
        {
            await _context.Owners.ReplaceOneAsync(o => o.Id == owner.Id, owner);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Owners.DeleteOneAsync(owner => owner.Id == id);
        }
    }
}
