using OwnerService.Application.DTOs;
using OwnerService.Application.Mappers;
using OwnerService.Application.Services;
using OwnerService.Domain.Entities;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.Services;
using OwnerService.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnerService.Application.Services
{
    public class OwnerApplicationService : IOwnerApplicationService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerCodeGenerator _ownerCodeGenerator;

        public OwnerApplicationService(IOwnerRepository ownerRepository, IOwnerCodeGenerator ownerCodeGenerator)
        {
            _ownerRepository = ownerRepository;
            _ownerCodeGenerator = ownerCodeGenerator;
        }

        public async Task<IEnumerable<OwnerDto>> GetAllAsync()
        {
            var owners = await _ownerRepository.GetAllAsync();
            var ownerDtos = new List<OwnerDto>();
            foreach (var owner in owners)
            {
                ownerDtos.Add(OwnerMapper.ToDto(owner));
            }
            return ownerDtos;
        }

        public async Task<OwnerDto> GetByIdAsync(string id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            return owner != null ? OwnerMapper.ToDto(owner) : null;
        }

        public async Task<OwnerDto> CreateAsync(CreateOwnerDto createOwnerDto)
        {
            var ownerCode = await _ownerCodeGenerator.GenerateAsync();
            var owner = Owner.Create(
                ownerCode,
                createOwnerDto.FullName,
                new Address(createOwnerDto.Address),
                createOwnerDto.Phone,
                new Email(createOwnerDto.Email)
            );

            await _ownerRepository.AddAsync(owner);

            return OwnerMapper.ToDto(owner);
        }

        public async Task UpdateAsync(string id, UpdateOwnerDto updateOwnerDto)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            if (owner != null)
            {
                owner.ChangeAddress(new Address(updateOwnerDto.Address));
                owner.ChangeEmail(new Email(updateOwnerDto.Email));
                owner.ChangePhone(updateOwnerDto.Phone);

                await _ownerRepository.UpdateAsync(owner);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _ownerRepository.DeleteAsync(id);
        }
    }
}
