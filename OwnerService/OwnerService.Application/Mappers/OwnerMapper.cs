using OwnerService.Application.DTOs;
using OwnerService.Domain.Entities;

namespace OwnerService.Application.Mappers
{
    public static class OwnerMapper
    {
        public static OwnerDto ToDto(Owner owner)
        {
            return new OwnerDto
            {
                Id = owner.Id,
                OwnerCode = owner.OwnerCode.Value,
                FullName = owner.FullName,
                Address = owner.Address.Value,
                Phone = owner.Phone,
                Email = owner.Email.Value
            };
        }
    }
}
