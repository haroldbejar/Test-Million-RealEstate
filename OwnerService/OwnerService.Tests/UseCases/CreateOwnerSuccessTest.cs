
using Moq;
using OwnerService.Application.DTOs;
using OwnerService.Application.Services;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.Services;
using OwnerService.Domain.ValueObjects;
using System.Threading.Tasks;
using Xunit;

namespace OwnerService.Tests.UseCases
{
    public class CreateOwnerSuccessTest
    {
        [Fact]
        public async Task CreateOwner_Successful()
        {
            // Arrange
            var ownerRepositoryMock = new Mock<IOwnerRepository>();
            var ownerCodeGeneratorMock = new Mock<IOwnerCodeGenerator>();
            var ownerApplicationService = new OwnerApplicationService(ownerRepositoryMock.Object, ownerCodeGeneratorMock.Object);

            var createOwnerDto = new CreateOwnerDto
            {
                FullName = "John Doe",
                Address = "123 Main St",
                Phone = "555-1234",
                Email = "john.doe@example.com"
            };

            var ownerCode = new OwnerCode(1341);
            ownerCodeGeneratorMock.Setup(x => x.GenerateAsync()).ReturnsAsync(ownerCode);

            // Act
            var result = await ownerApplicationService.CreateAsync(createOwnerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createOwnerDto.FullName, result.FullName);
            Assert.Equal(createOwnerDto.Address, result.Address);
            Assert.Equal(createOwnerDto.Phone, result.Phone);
            Assert.Equal(createOwnerDto.Email, result.Email);
            Assert.Equal(ownerCode.Value, result.OwnerCode);

            ownerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<OwnerService.Domain.Entities.Owner>()), Times.Once);
        }
    }
}
