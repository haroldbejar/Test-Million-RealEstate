using Moq;
using PropertyService.Application.Dtos;
using PropertyService.Application.Services;
using PropertyService.Domain.Entities;
using PropertyService.Domain.Repositories;
using PropertyService.Domain.Models;
using PropertyService.Domain.Services;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using PropertyService.Domain.ValueObjects;
using PropertyService.Domain.Enums;
using PropertyService.Application.Mappers;

namespace PropertyService.Test.UseCases.Application.Services
{
    public class PropertyAppServiceTests
    {
        private readonly Mock<IPropertyRepository> _mockPropertyRepository;
        private readonly Mock<IFileStorageService> _mockFileStorageService;
        private readonly PropertyAppService _propertyAppService;

        public PropertyAppServiceTests()
        {
            _mockPropertyRepository = new Mock<IPropertyRepository>();
            _mockFileStorageService = new Mock<IFileStorageService>();
            _propertyAppService = new PropertyAppService(_mockPropertyRepository.Object, _mockFileStorageService.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_Property_And_Return_Dto()
        {
            // Arrange
            var createDto = new CreatePropertyDto
            {
                CodeOwner = 1,
                Title = "Test Property",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                Amount = 100000,
                Currency = "USD",
                Bedrooms = 3,
                Bathrooms = 2,
                SquareFootage = 1500,
                YearBuilt = 2020,
                // ImageUrl = "http://example.com/image.jpg",
                Status = "ForSale",
                PropertyType = "House"
            };

            _mockPropertyRepository.Setup(repo => repo.AddAsync(It.IsAny<Property>()))
                                   .Returns(Task.CompletedTask);

            // Act
            var result = await _propertyAppService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createDto.Title, result.Title);
            _mockPropertyRepository.Verify(repo => repo.AddAsync(It.IsAny<Property>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_PropertyDto_When_Exists()
        {
            // Arrange
            var propertyId = "someId";
            var property = new Property
            {
                Id = propertyId,
                CodeOwner = 1,
                Title = "Test Property",
                Location = new Location("Test City", "Test State", "Test Country"),
                Price = new Price(100000, "USD"),
                Details = new Details(3, 2, 1500, 2020),
                ImageUrl = "http://example.com/image.jpg",
                Status = PropertyStatus.ForSale,
                PropertyType = PropertyType.House
            };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId))
                                   .ReturnsAsync(property);

            // Act
            var result = await _propertyAppService.GetByIdAsync(propertyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(propertyId, result.Id);
            _mockPropertyRepository.Verify(repo => repo.GetByIdAsync(propertyId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Exists()
        {
            // Arrange
            var propertyId = "nonExistentId";
            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId))
                                   .ReturnsAsync((Property?)null);

            // Act
            var result = await _propertyAppService.GetByIdAsync(propertyId);

            // Assert
            Assert.Null(result);
            _mockPropertyRepository.Verify(repo => repo.GetByIdAsync(propertyId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_PagedResultDto_Of_PropertyDto()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var properties = new List<Property>
            {
                new Property { Id = "1", Title = "Prop1", Location = new Location("C1", "S1", "Co1"), Price = new Price(100, "USD"), Details = new Details(1,1,1,2000), Status = PropertyStatus.ForSale, PropertyType = PropertyType.House },
                new Property { Id = "2", Title = "Prop2", Location = new Location("C2", "S2", "Co2"), Price = new Price(200, "USD"), Details = new Details(2,2,2,2001), Status = PropertyStatus.Rent, PropertyType = PropertyType.Apartment }
            };
            var pagedResult = new PagedResult<Property>(properties, 2, pageNumber, pageSize);

            _mockPropertyRepository.Setup(repo => repo.GetAllAsync(pageNumber, pageSize))
                                   .ReturnsAsync(pagedResult);

            // Act
            var result = await _propertyAppService.GetAllAsync(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(2, result.Items.Count());
            _mockPropertyRepository.Verify(repo => repo.GetAllAsync(pageNumber, pageSize), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_True_When_Update_Succeeds()
        {
            // Arrange
            var propertyId = "someId";
            var updateDto = new UpdatePropertyDto
            {
                Title = "Updated Property",
                City = "Updated City",
                State = "Updated State",
                Country = "Updated Country",
                Amount = 200000,
                Currency = "EUR",
                Bedrooms = 4,
                Bathrooms = 3,
                SquareFootage = 2000,
                YearBuilt = 2021,
                // ImageUrl = "http://example.com/updated.jpg",
                Status = "Rent",
                PropertyType = "Apartment"
            };

            var existingProperty = new Property { Id = propertyId, CodeOwner = 1, Title = "Old Title" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(existingProperty);

            _mockPropertyRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Property>()))
                                   .ReturnsAsync(true);

            // Act
            var result = await _propertyAppService.UpdateAsync(propertyId, updateDto);

            // Assert
            Assert.True(result);
            _mockPropertyRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Property>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_False_When_Update_Fails()
        {
            // Arrange
            var propertyId = "nonExistentId";
            var updateDto = new UpdatePropertyDto
            {
                Title = "Updated Property",
                City = "Updated City",
                State = "Updated State",
                Country = "Updated Country",
                Amount = 200000,
                Currency = "EUR",
                Bedrooms = 4,
                Bathrooms = 3,
                SquareFootage = 2000,
                YearBuilt = 2021,
                // ImageUrl = "http://example.com/updated.jpg",
                Status = "Rent",
                PropertyType = "Apartment"
            };
            var existingProperty = new Property { Id = propertyId, CodeOwner = 1, Title = "Old Title" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(existingProperty);

            _mockPropertyRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Property>()))
                                   .ReturnsAsync(false);

            // Act
            var result = await _propertyAppService.UpdateAsync(propertyId, updateDto);

            // Assert
            Assert.False(result);
            _mockPropertyRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Property>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_True_When_Delete_Succeeds()
        {
            // Arrange
            var propertyId = "someId";
            var propertyToDelete = new Property { Id = propertyId, ImageUrl = "image.jpg" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(propertyToDelete);
            _mockFileStorageService.Setup(s => s.DeleteFileAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockPropertyRepository.Setup(repo => repo.DeleteAsync(propertyId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _propertyAppService.DeleteAsync(propertyId);

            // Assert
            Assert.True(result);
            _mockPropertyRepository.Verify(repo => repo.DeleteAsync(propertyId), Times.Once);
            _mockFileStorageService.Verify(s => s.DeleteFileAsync(propertyToDelete.ImageUrl, "images"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_False_When_Delete_Fails()
        {
            // Arrange
            var propertyId = "nonExistentId";
            var propertyToDelete = new Property { Id = propertyId };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(propertyToDelete);
            _mockPropertyRepository.Setup(repo => repo.DeleteAsync(propertyId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _propertyAppService.DeleteAsync(propertyId);

            // Assert
            Assert.False(result);
            _mockPropertyRepository.Verify(repo => repo.DeleteAsync(propertyId), Times.Once);
        }
    }
}
