using MongoDB.Driver;
using PropertyService.Domain.Entities;
using PropertyService.Domain.Models;
using PropertyService.Domain.Repositories;
using PropertyService.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyService.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDbContext _context;

        public PropertyRepository(PropertyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.InsertOneAsync(property);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await _context.Properties.DeleteOneAsync(p => p.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<PagedResult<Property>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Properties.CountDocumentsAsync(p => true);
            var items = await _context.Properties.Find(p => true)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Limit(pageSize)
                                               .ToListAsync();

            return new PagedResult<Property>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Property> GetByIdAsync(string id)
        {
            return await _context.Properties.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Property>> SearchByParam(PropertySearchParams searchParams, int pageNumber, int pageSize)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (searchParams.Bedrooms.HasValue)
                filters.Add(filterBuilder.Eq(p => p.Details.Bedrooms, searchParams.Bedrooms.Value));

            if (!string.IsNullOrWhiteSpace(searchParams.Title))
                filters.Add(filterBuilder.Regex(p => p.Title, new MongoDB.Bson.BsonRegularExpression(searchParams.Title, "i")));

            if (searchParams.MaxAmount.HasValue)
                filters.Add(filterBuilder.Lte(p => p.Price.Amount, searchParams.MaxAmount));

            if (searchParams.MinAmount.HasValue)
                filters.Add(filterBuilder.Gte(p => p.Price.Amount, searchParams.MinAmount));

            var filter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

            var totalItems = await _context.Properties.CountDocumentsAsync(filter);

            var items = await _context.Properties
                .Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PagedResult<Property>(items, totalItems, pageNumber, pageSize);
        }


        public async Task<bool> UpdateAsync(Property property)
        {
            var updateResult = await _context.Properties.ReplaceOneAsync(p => p.Id == property.Id, property);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}