using PropertyService.Application.Dtos;
using PropertyService.Application.Mappers;
using PropertyService.Domain.Models;

//using PropertyService.Application.Services;

using PropertyService.Domain.Repositories;
using PropertyService.Domain.Services;

using System.Threading.Tasks;

namespace PropertyService.Application.Services
{
    public class PropertyAppService : IPropertyAppService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IFileStorageService _fileStorageService;

        public PropertyAppService(IPropertyRepository propertyRepository, IFileStorageService fileStorageService)
        {
            _propertyRepository = propertyRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto)
        {
            var property = createPropertyDto.ToEntity();

            if (createPropertyDto.ImageFile != null)
            {
                property.ImageUrl = await _fileStorageService.SaveFileAsync(createPropertyDto.ImageFile, "images");
            }

            await _propertyRepository.AddAsync(property);
            return property.ToDto();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var propertyToDelete = await _propertyRepository.GetByIdAsync(id);
            if (propertyToDelete == null) return false;

            if (!string.IsNullOrEmpty(propertyToDelete.ImageUrl))
            {
                await _fileStorageService.DeleteFileAsync(propertyToDelete.ImageUrl, "images");
            }

            return await _propertyRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<PropertyDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var pagedResult = await _propertyRepository.GetAllAsync(pageNumber, pageSize);
            return pagedResult.ToDto();
        }

        public async Task<PropertyDto> GetByIdAsync(string id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return property.ToDto();
        }

        public async Task<PagedResultDto<PropertyDto>> SearchByParams(PropertySearchParams searchParams, int pageNumber, int pageSize)
        {
            var pagedResult = await _propertyRepository.SearchByParam(searchParams, pageNumber, pageSize);
            return pagedResult.ToDto();
        }

        public async Task<bool> UpdateAsync(string id, UpdatePropertyDto updatePropertyDto)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(id);
            if (existingProperty == null) return false;

            var propertyToUpdate = updatePropertyDto.ToEntity(id);
            propertyToUpdate.CodeOwner = existingProperty.CodeOwner; // CodeOwner should not be updated via UpdateDto

            if (updatePropertyDto.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl))
                {
                    await _fileStorageService.DeleteFileAsync(existingProperty.ImageUrl, "images");
                }
                propertyToUpdate.ImageUrl = await _fileStorageService.SaveFileAsync(updatePropertyDto.ImageFile, "images");
            }
            else
            {
                propertyToUpdate.ImageUrl = existingProperty.ImageUrl; // Keep existing image if no new one is provided
            }

            return await _propertyRepository.UpdateAsync(propertyToUpdate);
        }
    }
}

