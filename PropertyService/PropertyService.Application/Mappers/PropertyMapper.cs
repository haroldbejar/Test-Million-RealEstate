using PropertyService.Application.Dtos;
using PropertyService.Domain.Entities;
using PropertyService.Domain.Enums;
using PropertyService.Domain.Models;
using PropertyService.Domain.ValueObjects;
using System.Linq;

namespace PropertyService.Application.Mappers
{
    public static class PropertyMapper
    {
        public static Property ToEntity(this CreatePropertyDto dto)
        {
            return new Property
            {
                CodeOwner = dto.CodeOwner,
                Title = dto.Title,
                Location = new Location(dto.City, dto.State, dto.Country),
                Price = new Price(dto.Amount, dto.Currency),
                Details = new Details(dto.Bedrooms, dto.Bathrooms, dto.SquareFootage, dto.YearBuilt),
                // ImageUrl will be set by the AppService
                Status = Enum.Parse<PropertyStatus>(dto.Status, true),
                PropertyType = Enum.Parse<PropertyType>(dto.PropertyType, true)
            };
        }

        public static Property ToEntity(this UpdatePropertyDto dto, string id)
        {
            return new Property
            {
                Id = id,
                Title = dto.Title,
                Location = new Location(dto.City, dto.State, dto.Country),
                Price = new Price(dto.Amount, dto.Currency),
                Details = new Details(dto.Bedrooms, dto.Bathrooms, dto.SquareFootage, dto.YearBuilt),
                // ImageUrl will be set by the AppService
                Status = Enum.Parse<PropertyStatus>(dto.Status, true),
                PropertyType = Enum.Parse<PropertyType>(dto.PropertyType, true)
            };
        }

        public static PropertyDto ToDto(this Property entity)
        {
            if (entity == null) return null;

            return new PropertyDto
            {
                Id = entity.Id,
                CodeOwner = entity.CodeOwner,
                Title = entity.Title,
                City = entity.Location?.City,
                State = entity.Location?.State,
                Country = entity.Location?.Country,
                Amount = entity.Price?.Amount ?? 0,
                Currency = entity.Price?.Currency,
                Bedrooms = entity.Details?.Bedrooms ?? 0,
                Bathrooms = entity.Details?.Bathrooms ?? 0,
                SquareFootage = entity.Details?.SquareFootage ?? 0,
                YearBuilt = entity.Details?.YearBuilt ?? 0,
                ImageUrl = entity.ImageUrl,
                Status = entity.Status.ToString(),
                PropertyType = entity.PropertyType.ToString()
            };
        }

        public static PagedResultDto<PropertyDto> ToDto(this PagedResult<Property> pagedResult)
        {
            return new PagedResultDto<PropertyDto>
            {
                Items = pagedResult.Items.Select(p => p.ToDto()),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages
            };
        }
    }
}
