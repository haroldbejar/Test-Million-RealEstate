using PropertyService.Domain.Exceptions;
using PropertyService.Domain.ValueObjects;
using Xunit;

namespace PropertyService.Test.UseCases.Domain.ValueObjects
{
    public class LocationTests
    {
        [Fact]
        public void Create_Location_With_Valid_Data_Should_Succeed()
        {
            // Arrange
            var city = "New York";
            var state = "NY";
            var country = "USA";

            // Act
            var location = new Location(city, state, country);

            // Assert
            Assert.NotNull(location);
            Assert.Equal(city, location.City);
            Assert.Equal(state, location.State);
            Assert.Equal(country, location.Country);
        }

        [Theory]
        [InlineData(null, "NY", "USA")]
        [InlineData("", "NY", "USA")]
        [InlineData("   ", "NY", "USA")]
        [InlineData("New York", null, "USA")]
        [InlineData("New York", "", "USA")]
        [InlineData("New York", "   ", "USA")]
        [InlineData("New York", "NY", null)]
        [InlineData("New York", "NY", "")]
        [InlineData("New York", "NY", "   ")]
        public void Create_Location_With_Invalid_Data_Should_Throw_ArgumentException(string city, string state, string country)
        {
            // Act & Assert
            Assert.Throws<System.ArgumentException>(() => new Location(city, state, country));
        }
    }
}
