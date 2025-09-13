using PropertyService.Domain.Exceptions;
using PropertyService.Domain.ValueObjects;
using System;
using Xunit;

namespace PropertyService.Test.UseCases.Domain.ValueObjects
{
    public class DetailsTests
    {
        [Fact]
        public void Create_Details_With_Valid_Data_Should_Succeed()
        {
            // Arrange
            var bedrooms = 3;
            var bathrooms = 2;
            var squareFootage = 1500;
            var yearBuilt = 2020;

            // Act
            var details = new Details(bedrooms, bathrooms, squareFootage, yearBuilt);

            // Assert
            Assert.NotNull(details);
            Assert.Equal(bedrooms, details.Bedrooms);
            Assert.Equal(bathrooms, details.Bathrooms);
            Assert.Equal(squareFootage, details.SquareFootage);
            Assert.Equal(yearBuilt, details.YearBuilt);
        }

        [Theory]
        [InlineData(-1, 2, 1500, 2020)]
        [InlineData(3, -1, 1500, 2020)]
        [InlineData(3, 2, -1, 2020)]
        public void Create_Details_With_Negative_Values_Should_Throw_PropertyDomainException(int bedrooms, int bathrooms, int sqft, int year)
        {
            // Act & Assert
            var exception = Assert.Throws<PropertyDomainException>(() => new Details(bedrooms, bathrooms, sqft, year));
            Assert.Equal("INVALID_NEGATIVE_VALUE", exception.Code);
        }

        [Theory]
        [InlineData(3, 2, 1500, 1799)] // Year too old
        [InlineData(3, 2, 1500, 2099)] // Year in the future
        public void Create_Details_With_Invalid_Year_Should_Throw_PropertyDomainException(int bedrooms, int bathrooms, int sqft, int year)
        {
            // Arrange
            // Mocking DateTime.UtcNow is complex, so for this test, we'll assume current year is less than 2099
            // A more robust test could use a time-mocking library

            // Act & Assert
            var exception = Assert.Throws<PropertyDomainException>(() => new Details(bedrooms, bathrooms, sqft, year));
            Assert.Equal("INVALID_YEAR_BUILT", exception.Code);
        }
    }
}
