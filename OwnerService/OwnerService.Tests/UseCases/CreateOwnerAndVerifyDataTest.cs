
using Moq;
using OwnerService.Application.DTOs;
using OwnerService.Application.Services;
using OwnerService.Domain.Entities;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.Services;
using OwnerService.Domain.ValueObjects;
using System.Threading.Tasks;
using Xunit;

namespace OwnerService.Tests.UseCases
{
    public class CreateOwnerAndVerifyDataTest
    {
        [Fact]
        public async Task CreateOwner_AndVerifyData()
        {
            // Arrange
            var ownerRepositoryMock = new Mock<IOwnerRepository>();
            var ownerCodeGeneratorMock = new Mock<IOwnerCodeGenerator>();
            var ownerApplicationService = new OwnerApplicationService(ownerRepositoryMock.Object, ownerCodeGeneratorMock.Object);

            var createOwnerDto = new CreateOwnerDto
            {
                FullName = "Peter Jones",
                Address = "789 Pine St",
                Phone = "555-9012",
                Email = "peter.jones@example.com"
            };

            var ownerCode = new OwnerCode(1234);
            ownerCodeGeneratorMock.Setup(x => x.GenerateAsync()).ReturnsAsync(ownerCode);

            Owner? capturedOwner = null;
            ownerRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Owner>()))
                             .Callback<Owner>(owner => capturedOwner = owner)
                             .Returns(Task.CompletedTask);

            // Act
            await ownerApplicationService.CreateAsync(createOwnerDto);

            // Assert
            ownerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Owner>()), Times.Once);

            Assert.NotNull(capturedOwner);
            Assert.Equal(createOwnerDto.FullName, capturedOwner.FullName);
            Assert.Equal(createOwnerDto.Address, capturedOwner.Address.Value);
            Assert.Equal(createOwnerDto.Phone, capturedOwner.Phone);
            Assert.Equal(createOwnerDto.Email, capturedOwner.Email.Value);
            Assert.Equal(ownerCode.Value, capturedOwner.OwnerCode.Value);
        }
    }
}
