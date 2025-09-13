
using Moq;
using OwnerService.Application.DTOs;
using OwnerService.Application.Services;
using OwnerService.Domain.Exceptions;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.Services;
using System.Threading.Tasks;
using Xunit;

namespace OwnerService.Tests.UseCases
{
    public class CreateOwnerWithInvalidEmailTest
    {
        [Fact]
        public async Task CreateOwner_WithInvalidEmail_ThrowsOwnerDomainException()
        {
            // Arrange
            var ownerRepositoryMock = new Mock<IOwnerRepository>();
            var ownerCodeGeneratorMock = new Mock<IOwnerCodeGenerator>();
            var ownerApplicationService = new OwnerApplicationService(ownerRepositoryMock.Object, ownerCodeGeneratorMock.Object);

            var createOwnerDto = new CreateOwnerDto
            {
                FullName = "Jane Doe",
                Address = "456 Oak Ave",
                Phone = "555-5678",
                Email = "invalid-email"
            };

            // Act & Assert
            await Assert.ThrowsAsync<OwnerDomainException>(() => ownerApplicationService.CreateAsync(createOwnerDto));
        }
    }
}
